using System;
using System.Net;

namespace jlab.FileTransferServer.libs
{
    public class SearchDirectory : Directory
    {
        private string _query { get; set; }
        public SearchDirectory(string dirToQuery, string query) : base(dirToQuery)
        {
            this._query = query;
        }

        public HttpListenerContext Get(HttpListenerContext context, int indexStart, int count)
        {
            throw new NotImplementedException();
        }
    }
}