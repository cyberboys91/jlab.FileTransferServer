using System;
using System.IO;
using System.Threading;

namespace jlab.FileTransferServer.libs
{
    public class Index
    {
        public Config config { get; set; }
        public Thread lastRenamedThread { get; private set; }
        public string lastRenamedDirPath { get; private set; }
        public Action onStartingCreating;
        public Action onFinishCreating;
        public bool Creating { get; private set; }
        public IPrintMessage OutputMesages { get; set; }

        public Index(Config config)
        {
            this.config = config;
        }

        public void Create(string dirPath, Action refresh, Func<bool> continua)
        {
            if (Creating)
            {
                onFinishCreating();
                return;
            }
            if (lastRenamedThread != null && lastRenamedThread.IsAlive)
            {
                if (Utils.IsPrefix(lastRenamedDirPath, dirPath))
                    try
                    {
                        lastRenamedThread.Abort();
                    }
                    catch { }
                else if (Utils.IsPrefix(dirPath, lastRenamedDirPath))
                    return;
            }
            onStartingCreating();
            lastRenamedThread = new Thread(() =>
            {
                long time = Environment.TickCount;
                Creating = true;
                try
                {
                    if (!System.IO.Directory.Exists(dirPath))
                        OutputMesages.Print(string.Format("[{0}] - {1}", DateTime.Now, Properties.Settings.Default.InvalidDir), Config.INDEX_TAG);
                    else
                    {
                        OutputMesages.Print(string.Format("[{0}] - {1}", DateTime.Now, Properties.Settings.Default.CreatingThumbIndex), Config.INDEX_TAG);
                        Create(new DirectoryInfo(dirPath), refresh, config, continua);
                        OutputMesages.Print(Utils.Trace(Properties.Settings.Default.CreatingFinish, Environment.TickCount - time), Config.INDEX_TAG);
                    }
                    config.AddDirIndixed(dirPath);
                }
                catch
                {
                    OutputMesages.Print(Utils.Trace(Properties.Settings.Default.CreatingInterrupted, Environment.TickCount - time), Config.INDEX_TAG);
                }
                finally
                {
                    Creating = false;
                    onFinishCreating();
                }
            });
            lastRenamedThread.Start();
        }

        public void Delete(string dirPath = null)
        {
            var time = Environment.TickCount;
            OutputMesages.Print(string.Format("[{0}] - {1} {2} {3} {4}...", Properties.Settings.Default.Deleting, dirPath == null ? Properties.Settings.Default.All : "", dirPath != null ? string.Format("{0} \"{1}\" ", Properties.Settings.Default.ForDirectory, dirPath) : "", DateTime.Now), Config.INDEX_TAG);
            if (dirPath == null)
            {
                System.IO.Directory.Delete(Config.THUMBNAILS_DIR_PATH, true);
                config.EmptyDirPathIndexes();
            }
            else
            {
                if (System.IO.Directory.Exists(dirPath))
                    Utils.ApplyActionToThumbnailFiles(new DirectoryInfo(dirPath), x =>
                    {
                        new Thumbnail(x, config).Delete();
                    });
                config.DeleteAllDirWithPrefix(dirPath);
            }
            OutputMesages.Print(Utils.Trace(Properties.Settings.Default.DeletingFinished, Environment.TickCount - time), Config.INDEX_TAG);
        }

        private void Create(DirectoryInfo dirInfo, Action refresh, Config config, Func<bool> continua)
        {
            Utils.ApplyActionToThumbnailFiles(dirInfo, x =>
            {
                while (!continua())
                    Thread.Sleep(1000);
                try
                {
                    int time = Environment.TickCount;
                    new Thumbnail(x, config).Save();
                    OutputMesages.Print(Utils.Trace(string.Format("{0} \"{1}\"", Properties.Settings.Default.AddedThumb, x), Environment.TickCount - time), Config.INDEX_TAG);
                }
                catch { }
                refresh();
            });
        }
    }
}