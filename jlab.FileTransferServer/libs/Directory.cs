using System.IO;
using System.Net;
using System.Linq;
using System;

namespace jlab.FileTransferServer.libs
{
    public class Directory : Resource, IDisposable
    {
        protected DirectoryInfo _info { get; set; }
        public Directory(string path) : base(path, 0, true)
        {
            _info = new DirectoryInfo(path);
        }

        ~Directory()
        {
            Dispose();
        }

        public void Get(HttpListenerContext context, Action<int> refresh)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(context.Response.OutputStream);
                
                context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
                context.Response.AddHeader("Last-Modified", System.IO.File.GetLastWriteTime(Path).ToString("r"));
                context.Response.AddHeader("Connection", "keep-alive");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                foreach (var dirChild in _info.GetDirectories().Where(x => Utils.IsShowed(x)))
                {
                    var res = new Resource(dirChild.Name, dirChild.FullName, -1, true).ToArray();
                    int len = System.Text.Encoding.UTF8.GetByteCount(res);
                    Length += len;
                    refresh(len);
                    writer.WriteLine(res);
                }
                foreach (var fileChild in _info.GetFiles().Where(x => Utils.IsShowed(x)))
                {
                    var res = new Resource(fileChild.Name, fileChild.FullName, fileChild.Length, false).ToArray();
                    int len = System.Text.Encoding.UTF8.GetByteCount(res);
                    Length += len;
                    refresh(len);
                    writer.WriteLine(res);
                }
            }
            catch
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    GC.SuppressFinalize(writer);
                    Dispose();
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}