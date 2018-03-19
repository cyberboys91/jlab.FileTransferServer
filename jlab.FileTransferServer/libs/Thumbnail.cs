using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace jlab.FileTransferServer.libs
{
    public class Thumbnail : File
    {
        private static int THUMB_SIZE = 100;
        private string _filePath { get; set; }
        public Thumbnail(string filePath, Config config, int thumbSize = 100) :
            base(System.IO.Path.Combine(Config.THUMBNAILS_DIR_PATH, Utils.HashStr(filePath)), config, File.GenerateClassMimeType(filePath, true))
        {
            _filePath = filePath;
            THUMB_SIZE = thumbSize;
        }

        ~Thumbnail()
        {
            Dispose();
        }

        public void Save()
        {
            if (!System.IO.Directory.Exists(Config.THUMBNAILS_DIR_PATH))
                System.IO.Directory.CreateDirectory(Config.THUMBNAILS_DIR_PATH);
            if (_classMimeType == ClassMimeType.APK)
                ExtractAndSaveFromApk();
            else if (_classMimeType == ClassMimeType.AUDIO)
                ExtractAndSaveFromApk();
            else if (_classMimeType == ClassMimeType.ICON)
            {
                Icon icon = new Icon(_filePath);
                int div = GetSize(icon.Width, icon.Height);
                icon.ToBitmap().GetThumbnailImage(icon.Width / div, icon.Height / div, () => false, IntPtr.Zero).Save(Path);
            }
            else if (_classMimeType == ClassMimeType.IMAGE)
            {
                Image image = Image.FromFile(_filePath);
                ToThumbAndSaveImage(image);
                image.Dispose();
            }
        }

        public void ExtractAndSaveFromAudio()
        {
            var file = TagLib.File.Create(this._filePath);
            if (file.Tag.Pictures.Length > 0)
            {
                var bin = file.Tag.Pictures[0].Data.Data;
                using (MemoryStream memoryStream = new MemoryStream(bin))
                {
                    Image thumbnail = Image.FromStream(memoryStream);
                    int div = GetSize(thumbnail.Width, thumbnail.Height);
                    thumbnail = thumbnail.GetThumbnailImage(thumbnail.Width / div, thumbnail.Height / div, () => false, IntPtr.Zero);
                    thumbnail.Save(Path);
                }
            }
        }

        private void ToThumbAndSaveImage(Image image)
        {
            if (_classMimeType != ClassMimeType.OTHER && _classMimeType != ClassMimeType.APK)
            {
                int div = GetSize(image.Width, image.Height);
                image = image.GetThumbnailImage(image.Width / div, image.Height / div, () => false, IntPtr.Zero);
            }
            else
                image = image.GetThumbnailImage(THUMB_SIZE, THUMB_SIZE, () => false, IntPtr.Zero);
            image.Save(Path);
        }

        private void ExtractAndSaveFromApk()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = System.IO.Path.Combine(Environment.CurrentDirectory, "data\\aapt.exe");
                process.StartInfo.Arguments = string.Format("dump badging \"{0}\"", _filePath);
                process.Start();

                string apkPathIcon = getApkPathIcon(process.StandardOutput.ReadToEnd());
                process.Close();

                using (ZipArchive archive = ZipFile.OpenRead(_filePath))
                    foreach (ZipArchiveEntry entry in archive.Entries)
                        if (entry.FullName == apkPathIcon)
                        {
                            Stream stream = entry.Open();
                            Image icon = Image.FromStream(stream);
                            ToThumbAndSaveImage(icon);
                            stream.Close();
                            break;
                        }
            }
            catch { }
        }

        private int GetSize(double width, double height)
        {
            double size = Math.Min(width, height);
            return (int)Math.Ceiling(size / THUMB_SIZE);
        }

        private static string getApkPathIcon(string aaptOutput)
        {
            string[] lines = aaptOutput.Split(new char[] { '\r', '\n' });
            var re = new Regex("application-icon(.*):(.*)");
            foreach (var line in lines)
            {
                if (re.IsMatch(line))
                    return line.Split(':')[1].Replace("'", "");
            }
            return null;
        }
    }

    public enum ClassMimeType
    {
        APK,
        AUDIO,
        VIDEO,
        IMAGE,
        ICON,
        OTHER
    }
}
