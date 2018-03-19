using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;

namespace jlab.FileTransferServer.libs
{
    class HttpServer
    {
        private string _rootDirectory { get; set; }
        private HttpListener _listener { get; set; }
        public string Host { get; private set; }
        private string DirPath { get; set; }
        public int Port { get; private set; }
        private Semaphore sMaxFileDownloadsConcurrent;
        private Semaphore sMaxDirDownloadsConcurrent;
        private Semaphore sMaxThumbDownloadsConcurrent;
        private Config Config { get; set; }

        public long DownloadDirLength { get; set; }
        public long DownloadDirSpeed { get; set; }
        public long DownloadFileLength { get; set; }
        public long DownloadFileSpeed { get; set; }
        public long DownloadThumbLength { get; set; }
        public long DownloadThumbSpeed { get; set; }
        public IPrintMessage OutputMesages { get; set; }

        public void Create(string host, Config config)
        {
            Config = config;
            DirPath = Config.DirectoryPath;
            InitializeSemaphore(Config.MaxFileDownConcurrent, Config.MaxDirectoryDownConcurrent, Config.MaxThumbnailDownConcurrent);
            this.Host = host;
            this.Port = Config.Port;
            if (!System.IO.Directory.Exists(DirPath))
                throw new MException(Properties.Settings.Default.TheDirectory + (DirPath != null ? " " : "") + DirPath + " " + Properties.Settings.Default.ShareNotExist);
        }

        public void Start()
        {
            this.Initialize(DirPath[DirPath.Length - 1] == Path.DirectorySeparatorChar ? DirPath : DirPath + Path.DirectorySeparatorChar);
        }
        
        private void InitializeSemaphore(int maxFileDownConc, int maxDirDownConc, int maxThumbDownConc)
        {
            this.sMaxFileDownloadsConcurrent = new Semaphore(maxFileDownConc, maxFileDownConc);
            this.sMaxDirDownloadsConcurrent = new Semaphore(maxDirDownConc, maxDirDownConc);
            this.sMaxThumbDownloadsConcurrent = new Semaphore(maxThumbDownConc, maxThumbDownConc);
        }

        public static int GetAnyPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }

        public void Stop()
        {
            if (_listener.IsListening)
            {
                _listener.Stop();
                _listener.Abort();
            }
        }

        private void Listen()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(string.Format("http://{0}:{1}/d/", this.Host, this.Port));
            _listener.Prefixes.Add(string.Format("http://{0}:{1}/f/", this.Host, this.Port));
            _listener.Prefixes.Add(string.Format("http://{0}:{1}/t/", this.Host, this.Port));
            _listener.Prefixes.Add(string.Format("http://{0}:{1}/p/", this.Host, this.Port));
            _listener.Prefixes.Add(string.Format("http://{0}:{1}/s/", this.Host, this.Port));
            try
            {
                _listener.Start();
            }
            catch(Exception exp)
            {
                throw new MException(Properties.Settings.Default.NotCanStartServer);
            }
            while (true)
            {
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    new Thread(() => Process(context, Environment.TickCount)).Start();
                }
                catch
                {
                }
            }
        }

        private string SubStringAndReplace(string s, char oldc, char newc, int index)
        {
            int l = _rootDirectory.Length;
            char[] result = new char[l + s.Length - index];
            for (int i = 0; i < l; i++)
                result[i] = _rootDirectory[i];

            for (int i = index; i < s.Length; i++)
                if (s[i] == oldc)
                    result[l + i - index] = newc;
                else
                    result[l + i - index] = s[i];
            return new string(result);
        }

        private void Process(HttpListenerContext context, long starticks)
        {
            try
            {
                string uri = Uri.UnescapeDataString(context.Request.RawUrl),
                method = context.Request.HttpMethod;
                IPAddress address = context.Request.RemoteEndPoint.Address;
                Version protVer = context.Request.ProtocolVersion;
                HttpMethod methodTag = HttpMethodParse(method);
                char resId = Config.ALL_TAG;
                if (methodTag == HttpMethod.GET || methodTag == HttpMethod.HEAD)
                {
                    resId = uri[1];
                    string resName;
                    Dictionary<string, string> varsQuery;
                    try
                    {
                        resName = SubStringAndReplace(uri, '/', Path.DirectorySeparatorChar, 3);
                        varsQuery = Utils.GetQueryVars(Uri.UnescapeDataString(context.Request.Url.Query));
                    }
                    catch
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return;
                    }
                    if (varsQuery != null)
                    {
                        if (resId != Config.SEARCH_TAG)
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        else
                        {
                        }
                    }
                    else
                    {
                        if (resId == Config.THUMB_PRESENTATION_TAG)
                        {
                            if (Config.ThumbnailPresentationPath == null || !System.IO.File.Exists(Config.ThumbnailPresentationPath))
                            {
                                context.Response.ContentLength64 = 0;
                                context.Response.StatusCode = (int)HttpStatusCode.OK;
                                context.Response.OutputStream.Close();
                            }
                            else
                            {
                                this.sMaxFileDownloadsConcurrent.WaitOne();
                                new File(Config.ThumbnailPresentationPath, Config).Get(context, x => Refresh(ResourceType.Thumbnail, x));
                                this.sMaxFileDownloadsConcurrent.Release();
                            }
                        }
                        else if (resId == Config.FILE_TAG && System.IO.File.Exists(resName))
                        {
                            if (methodTag == HttpMethod.GET)
                            {
                                this.sMaxFileDownloadsConcurrent.WaitOne();
                                new File(resName, Config).Get(context, x => Refresh(ResourceType.File, x));
                                this.sMaxFileDownloadsConcurrent.Release();
                            }
                            else
                                new File(resName, Config).Head(context);
                        }
                        else if (resId == Config.DIR_TAG && System.IO.Directory.Exists(resName))
                        {
                            this.sMaxDirDownloadsConcurrent.WaitOne();
                            var dir = new Directory(resName);
                            dir.Get(context, x => Refresh(ResourceType.Directory, x));
                            DownloadDirLength += dir.Length;
                            DownloadDirSpeed += dir.Length;
                            this.sMaxDirDownloadsConcurrent.Release();
                        }
                        else if (resId == Config.THUMB_TAG && System.IO.File.Exists(resName))
                        {
                            this.sMaxThumbDownloadsConcurrent.WaitOne();
                            new Thumbnail(resName, Config).Get(context, x => Refresh(ResourceType.Thumbnail, x));
                            this.sMaxThumbDownloadsConcurrent.Release();
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            context.Response.OutputStream.Close();
                        }
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    context.Response.OutputStream.Close();
                }
                OutputMesages.Print(Utils.Trace(address, method, uri, protVer, context.Response.StatusCode, Environment.TickCount - starticks), resId);
            }
            catch { }
        }

        public void Refresh(ResourceType resType, int y)
        {
            switch (resType)
            {
                case ResourceType.Directory:
                    DownloadDirSpeed += y;
                    DownloadDirLength += y;
                    break;
                case ResourceType.File:
                    DownloadFileSpeed += y;
                    DownloadFileLength += y;
                    break;
                case ResourceType.Thumbnail:
                    DownloadThumbSpeed += y;
                    DownloadThumbLength += y;
                    break;
                default:
                    break;
            }
        }

        public enum ResourceType
        {
            Directory,
            File,
            Thumbnail
        }

        private HttpMethod HttpMethodParse(string method)
        {
            HttpMethod result = HttpMethod.TRACE;
            switch (method)
            {
                case "GET":
                    result = HttpMethod.GET;
                    break;
                case "HEAD":
                    result = HttpMethod.HEAD;
                    break;
                case "POST":
                    result = HttpMethod.POST;
                    break;
                case "PUT":
                    result = HttpMethod.PUT;
                    break;
                case "DELETE":
                    result = HttpMethod.DELETE;
                    break;
                case "OPTIONS":
                    result = HttpMethod.OPTIONS;
                    break;
                case "CONNECT":
                    result = HttpMethod.CONNECT;
                    break;
            }
            return result;
        }

        private void Initialize(string path)
        {
            this._rootDirectory = path;
            Listen();
        }

        private enum HttpMethod
        {
            HEAD,
            GET,
            POST,
            PUT,
            DELETE,
            TRACE,
            OPTIONS,
            CONNECT
        }
    }
}