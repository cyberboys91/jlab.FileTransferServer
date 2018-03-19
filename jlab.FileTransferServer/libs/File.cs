using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace jlab.FileTransferServer.libs
{
    public class File : Resource, IDisposable
    {
        private FileStream _stream { get; set; } 
        protected string _mimeType { get; set; }
        public string _ext { get; protected set; }
        protected Config Config { get; set; }
        protected ClassMimeType _classMimeType { get; set; }
        public File(string path, Config config, Tuple<string, string, ClassMimeType> properties = null)
            : base(path, 0, true)
        {
            setProperties(path, config, properties);
        }

        protected void setProperties(string path, Config config, Tuple<string, string, ClassMimeType> properties = null)
        {
            bool propIsNull = properties == null;
            if (propIsNull)
                properties = GenerateClassMimeType(path, false);
            _ext = properties.Item1;
            Path += propIsNull ? "" : _ext;
            _mimeType = properties.Item2;
            _classMimeType = properties.Item3;
            Config = config;
        }

        ~File()
        {
            Dispose();
        }

        public static Tuple<string, string, ClassMimeType> GenerateClassMimeType(string filePath, bool updateExt)
        {
            var ext = System.IO.Path.GetExtension(filePath);
            var mimeType = Utils.GetMimeType(ext);
            var classMimeType = Utils.GetClassMimeType(mimeType, ext);
            if (updateExt)
                switch (classMimeType)
                {
                    case ClassMimeType.APK:
                        ext = ".png";
                        break;
                    case ClassMimeType.AUDIO:
                        ext = ".jpg";
                        break;
                    case ClassMimeType.VIDEO:
                        ext = ".png";
                        break;
                }
            return new Tuple<string, string, ClassMimeType>(ext, mimeType, classMimeType);
        }

        public void Get(HttpListenerContext context, Action<int> refresh)
        {
            try
            {
                try
                {
                    _stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read, Config.BUFFER_SIZE, true);
                }
                catch
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    Close(context.Response.OutputStream);
                    Close(_stream);
                    return;
                }
                context.Response.ContentType = _mimeType;
                context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(Path).ToString("r"));
                context.Response.AddHeader("Connection", "keep-alive");
                context.Response.AddHeader("Accept-Encoding", "identity");
                string range = context.Request.Headers.Get("Range");
                var re = new Regex("bytes=(.*)-(.*)");
                if (range != null && re.IsMatch(range))
                {
                    long streamLength = _stream.Length, byteStart = 0, byteEnd = 0;
                    bool badReq = false;
                    try
                    {
                        var split = range.Split('=')[1].Split('-');
                        byteStart = split[0] != "" ? long.Parse(split[0]) : 0;
                        byteEnd = split[1] != ""
                            ? Math.Min(long.Parse(split[1]), _stream.Length - 1)
                            : streamLength - 1;
                    }
                    catch
                    {
                        badReq = true;
                    }

                    long len = (int)(byteEnd - byteStart + 1);
                    if (badReq || len < 0)
                    {
                        context.Response.ContentLength64 = 0;
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                    else
                    {
                        context.Response.ContentLength64 = len;
                        context.Response.AddHeader("Content-Range", string.Format("bytes {0}-{1}/{2}", byteStart, byteEnd, _stream.Length));
                        context.Response.StatusCode = (int)HttpStatusCode.PartialContent;

                        Length = len;
                        _stream.CopyTo(context.Response.OutputStream, byteStart, len, Config.BUFFER_SIZE, refresh);
                    }
                }
                else if (range != null)
                {
                    context.Response.ContentLength64 = 0;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    Length = _stream.Length;
                    context.Response.ContentLength64 = Length;
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    _stream.CopyTo(context.Response.OutputStream, 0, Length, Config.BUFFER_SIZE, refresh);
                }
            }
            catch (Exception exp)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                Close(context.Response.OutputStream);
                Close(_stream);
            }
        }

        private void Close(Stream stream)
        {
            if (stream != null)
            {
                stream.Close();
                stream.Dispose();
                Dispose();
            }
        }

        public void Head(HttpListenerContext context)
        {
            _stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read, Config.BUFFER_SIZE, true);
            context.Response.ContentType = _mimeType;
            context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
            context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(Path).ToString("r"));
            context.Response.ContentLength64 = _stream.Length;
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            Close(_stream);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}