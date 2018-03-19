using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace jlab.FileTransferServer.libs
{
    public static class Utils
    {
        private const string IP_LOCALHOST = "127.0.0.1";
        public static Dictionary<string, string> GetQueryVars(string query)
        {
            Dictionary<string, string> result = null;
            query = query != "" ? query.Substring(1) : "";
            string[] varsAndValues = query.Split(new char[] { '&', '=' });
            if (varsAndValues.Length % 2 != 0)
            {
                //Invalid Query
            }
            else
            {
                result = new Dictionary<string, string>();
                for (int i = 0; i < varsAndValues.Length; i += 2)
                    result.Add(varsAndValues[i], varsAndValues[i + 1]);
            }
            return result;
        }

        public static bool IsPrefix(string s1, string prefix)
        {
            return s1 != null && s1.Length >= prefix.Length && s1.Substring(0, prefix.Length) == prefix;
        }

        public static bool isThumbnailer(string ext)
        {
            string mimeType = GetMimeType(ext);
            var classMimeType = Utils.GetClassMimeType(mimeType, ext);
            return classMimeType == ClassMimeType.APK || classMimeType == ClassMimeType.IMAGE || classMimeType == ClassMimeType.ICON;
        }

        public static void ApplyActionToThumbnailFiles(DirectoryInfo dirInfo, Action<string> action)
        {
            try
            {
                foreach (var file in dirInfo.GetFiles().Where(x => isThumbnailer(x.Extension) && Utils.IsShowed(x)))
                    action(file.FullName);

                foreach (var dir in dirInfo.GetDirectories().Where(x => Utils.IsShowed(x)
                && x.FullName != Path.Combine(Environment.CurrentDirectory, Config.THUMBNAILS_DIR_PATH)))
                    ApplyActionToThumbnailFiles(dir, action);
            }
            catch { }
        }

        public static string GetMimeType(string ext)
        {
            if (ext == ".apk")
                return "application/vnd.android.package-archive";
            if (ext == ".vob")
                return "video/x-ms-vob";
            string mimeType = "application/unknown";
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            object elem = regKey != null ? regKey.GetValue("Content Type") : null;
            if (regKey != null && elem != null)
                mimeType = elem.ToString();
            return mimeType;
        }

        public static ClassMimeType GetClassMimeType(string mimetype, string ext = null)
        {
            if (ext == ".ico")
                return ClassMimeType.ICON;
            if (ext == ".apk")
                return ClassMimeType.APK;
            switch (mimetype.Split('/')[0])
            {
                case "audio":
                    return ClassMimeType.AUDIO;
                case "video":
                    return ClassMimeType.VIDEO;
                case "image":
                    return ClassMimeType.IMAGE;
                default:
                    return ClassMimeType.OTHER;
            }
        }

        public static string Trace(IPAddress address, string method, string absPath, Version protVer, int code, double millisecs)
        {
            return string.Format("{0} - - [{1}] \"{2} {3} HTTP/{4}\" {5} - [{6}]", address, DateTime.Now, method, absPath, protVer, code, GetTime(millisecs));
        }

        public static string Trace(string description, double millisecs)
        {
            return string.Format("[{0}] {1} - [{2}]", DateTime.Now, description, GetTime(millisecs));
        }

        private static string GetTime(double millisecs)
        {
            string result = "";
            if (millisecs < 1000)
                result = string.Format("{0} millisecond{1}", millisecs, millisecs > 1 ? "s" : "");
            else
            {
                if (millisecs >= 1000)
                {
                    int secs = (int)millisecs / 1000;
                    result = string.Format("{0} second{1}", secs, secs > 1 ? "s" : "");
                    millisecs /= 1000d;
                }
                if (millisecs >= 60)
                {
                    int mins = (int)millisecs / 60;
                    result = string.Format("{0} min{1}, {2} sec", mins, mins > 1 ? "s" : "", (long)millisecs % 60);
                    millisecs /= 60d;
                }
                if (millisecs >= 60)
                {
                    int hours = (int)millisecs / 60;
                    result = string.Format("{0} hour{1}, {2} min{3}", hours, hours > 1 ? "s" : "", (long)millisecs % 60, millisecs % 60 > 1 ? "s" : "");
                }
            }
            return result;
        }

        public static string HashStr(string tohash)
        {
            UInt64 hashedValue = 3074457345618258791ul;
            for (int i = 0; i < tohash.Length; i++)
            {
                hashedValue += tohash[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue.ToString();
        }

        public static bool IsShowed(FileSystemInfo fi)
        {
            var att = fi.Attributes;
            return ((att | FileAttributes.Hidden) != att)
                    && ((att | FileAttributes.System) != att);
        }

        public static void CopyTo(this Stream source, Stream destination, long byteStartSource, long count, int bufferSize, Action<int> refresh)
        {
            if (byteStartSource > source.Length)
                return;
            if (byteStartSource + count > source.Length)
                count = source.Length - byteStartSource;
            source.Position = byteStartSource;
            long countWriter = 0;
            int lenBuff = bufferSize;
            byte[] buffer = new byte[lenBuff];
            while (countWriter < count)
            {
                if (countWriter + bufferSize > count)
                    lenBuff = (int)(count - countWriter);
                int countReaded = source.Read(buffer, 0, lenBuff);
                destination.Write(buffer, 0, countReaded);
                countWriter += countReaded;
                refresh(countReaded);
            }
        }

        public static IEnumerable<Tuple<string, string>> GetAllIpAddress()
        {
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces().Where(x => x.OperationalStatus == OperationalStatus.Up))
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork && ip.Address.ToString() != IP_LOCALHOST)
                    {
                        yield return new Tuple<string, string>(item.Name, ip.Address.ToString());
                        break;
                    }
                }
            }
        }

        public static string GetSize(long bytes)
        {
            string med = "B";
            double bytesDec = bytes;
            if (bytesDec >= 1024)
            {
                bytesDec /= 1024d;
                med = "KB";
            }
            if (bytesDec >= 1024)
            {
                bytesDec /= 1024d;
                med = "MB";
            }
            if (bytesDec >= 1024)
            {
                bytesDec /= 1024d;
                med = "GB";
            }
            if (bytesDec >= 1024)
            {
                bytesDec /= 1024d;
                med = "TB";
            }
            if (bytesDec >= 1024)
            {
                bytesDec /= 1024;
                med = "PB";
            }
            if (bytesDec >= 1024)
            {
                bytesDec /= 1024;
                med = "ZB";
            }
            if (bytesDec >= 1024)
            {
                bytesDec /= 1024;
                med = "YB";
            }
            return Math.Round(bytesDec, 1) + " " + med;
        }
    }

    [Serializable]
    public class Config
    {
        internal int Port = 9101;
        internal int MaxCountTrace = 1000;
        internal int MaxDirectoryDownConcurrent = 1000;
        internal int MaxFileDownConcurrent = 50;
        internal int MaxThumbnailDownConcurrent = 100;
        internal int BUFFER_SIZE = 1048576; //1MB
        internal const int COUNT_STORAGE = 1000;
        internal string ThumbnailPresentationPath;
        private List<string> DirectoriesIndexes { get; set; }
        internal const string DATA_DIR_PATH = "data";
        internal const string THUMBNAILS_DIR_PATH = "data\\.thumbnails";
        internal const char ALL_TAG = 'a';
        internal const char DIR_TAG = 'd';
        internal const char FILE_TAG = 'f';
        internal const char THUMB_TAG = 't';
        internal const char THUMB_PRESENTATION_TAG = 'p';
        internal const char SEARCH_TAG = 's';
        internal const char ERROR_TAG = 'e';
        internal const char INDEX_TAG = 'i';
        internal string DirectoryPath { get; set; }

        public Config()
        {
            this.DirectoriesIndexes = new List<string>();
        }

        public void Save()
        {
            try
            {
                using (FileStream saveFileStream = new FileStream("data\\config.ser", FileMode.OpenOrCreate))
                    new BinaryFormatter().Serialize(saveFileStream, this);
            }
            catch
            {
            }
        }

        public Config Load()
        {
            if (System.IO.File.Exists("data\\config.ser"))
            {
                try
                {
                    using (FileStream saveFileStream = new FileStream("data\\config.ser", FileMode.Open))
                    {
                        var config = (Config)new BinaryFormatter().Deserialize(saveFileStream);
                        return config;
                    }
                }
                catch { }
            }
            return this;
        }

        public void AddDirIndixed(string dirPath)
        {
            if (!IndexAlreadyCreated(dirPath))
                this.DirectoriesIndexes.Add(dirPath);
            DeleteAllDirWithPrefix(dirPath);
        }

        public void DeleteAllDirWithPrefix(string dirPath)
        {
            this.DirectoriesIndexes.RemoveAll(x => Utils.IsPrefix(x, dirPath) && x != dirPath);
        }

        public bool IndexAlreadyCreated(string dirPath)
        {
            return this.DirectoriesIndexes.Exists(x => Utils.IsPrefix(dirPath, x));
        }

        public bool ExistsIndex()
        {
            return this.DirectoriesIndexes.Count != 0;
        }

        public void EmptyDirPathIndexes()
        {
            this.DirectoriesIndexes.Clear();
        }

        public IEnumerable<string> DirectoriesIndixed()
        {
            return this.DirectoriesIndexes;
        }
    }
}