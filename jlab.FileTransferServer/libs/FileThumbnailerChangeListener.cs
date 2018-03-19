using System;
using System.IO;
using System.Threading;
using System.Security.Permissions;

namespace jlab.FileTransferServer.libs
{
    class FileThumbnailerChangeListener : FileSystemWatcher
    {
        private Config Config { get; set; }
        public Thread lastRenamedThread { get; private set; }
        private string lastRenamedDirPath { get; set; }
        public IPrintMessage OutputMesages { get; set; }

        public FileThumbnailerChangeListener(Config config) : base(config.DirectoryPath)
        {
            SetConfig(config);
            SetEvents();
            IncludeSubdirectories = true;
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
            EnableRaisingEvents = true;
        }

        public void SetConfig(Config config)
        {
            this.Config = config;
            this.Path = config.DirectoryPath;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void SetEvents()
        {
            Created += CreatedListener;
            Renamed += RenamedListener;
            Deleted += DeletedListener;
            Changed += FileThumbnailerChangeListener_Changed;
        }

        private void FileThumbnailerChangeListener_Changed(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private bool isThumbnail(string path)
        {
            return Utils.isThumbnailer(System.IO.Path.GetExtension(path));
        }

        private bool isChildOfThumbnailDirectory(string path)
        {
            string thumbPath = System.IO.Path.Combine(Environment.CurrentDirectory, Config.THUMBNAILS_DIR_PATH);
            return thumbPath.Length <= path.Length && path.Substring(0, thumbPath.Length) == thumbPath;
        }

        private void RenamedListener(object sender, RenamedEventArgs e)
        {
            if (!isChildOfThumbnailDirectory(e.FullPath))
            {
                if (isThumbnail(e.FullPath))
                {
                    try
                    {
                        //CASE: file
                        int time = Environment.TickCount;
                        new Thumbnail(e.OldFullPath, Config).Delete();
                        new Thumbnail(e.FullPath, Config).Save();
                        OutputMesages.Print(Utils.Trace(string.Format(Properties.Settings.Default.RenamedThumb + " \"{0}\"" + Properties.Settings.Default.To + " \"{1}\"", e.OldFullPath, e.FullPath), Environment.TickCount - time), Config.ALL_TAG);
                    }
                    catch { }
                }
                else if (System.IO.Directory.Exists(e.FullPath))
                {
                    if (lastRenamedThread != null && lastRenamedThread.IsAlive && Utils.IsPrefix(lastRenamedDirPath, e.OldFullPath))
                        lastRenamedThread.Abort();

                    lastRenamedDirPath = e.FullPath;
                    lastRenamedThread = new Thread(() =>
                    {
                        Utils.ApplyActionToThumbnailFiles(new DirectoryInfo(e.FullPath), x =>
                        {
                            try
                            {
                                if (isThumbnail(x))
                                {
                                    int time = Environment.TickCount;
                                    string oldFilePath = string.Format("{0}{1}", e.OldFullPath, x.Substring(e.FullPath.Length, x.Length - e.FullPath.Length));
                                    new Thumbnail(oldFilePath, Config).Delete();
                                    new Thumbnail(x, Config).Save();
                                    OutputMesages.Print(Utils.Trace(string.Format(Properties.Settings.Default.UpdateThumb + " \"{0}\""  + Properties.Settings.Default.To + " \"{1}\"", oldFilePath, x), Environment.TickCount - time), Config.ALL_TAG);
                                }
                            }
                            catch { }
                        });
                    });
                    lastRenamedThread.Start();
                }
            }
        }

        //COMPLETE
        private void CreatedListener(object sender, FileSystemEventArgs e)
        {
            if (!isChildOfThumbnailDirectory(e.FullPath))
            {
                if (isThumbnail(e.FullPath))
                {
                    try
                    {
                        //CASE: file
                        int time = Environment.TickCount;
                        new Thumbnail(e.FullPath, Config).Save();
                        OutputMesages.Print(Utils.Trace(string.Format(Properties.Settings.Default.CreateThumb + " \"{0}\"", e.FullPath), Environment.TickCount - time), Config.ALL_TAG);
                    }
                    catch { }
                }
            }
        }

        //COMPLETE
        private void DeletedListener(object sender, FileSystemEventArgs e)
        {
            if (!isChildOfThumbnailDirectory(e.FullPath))
            {
                if (isThumbnail(e.FullPath))
                {
                    try
                    {
                        //CASE: file
                        int time = Environment.TickCount;
                        new Thumbnail(e.FullPath, Config).Delete();
                        OutputMesages.Print(Utils.Trace(string.Format(Properties.Settings.Default.DeleteThumb + " \"{0}\"", e.FullPath), Environment.TickCount - time), Config.ALL_TAG);
                    }
                    catch { }
                }
            }
        }
    }
}
