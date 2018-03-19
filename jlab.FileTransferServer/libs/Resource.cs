using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace jlab.FileTransferServer.libs
{
    public class Resource : IEnumerable<char>
    {
        public string Name { get; private set; }
        public string Path { get; set; }
        public long Length { get; protected set; }
        private bool IsDir { get; set; }

        public Resource(string path, long size, bool isDir)
        {
            this.Name = System.IO.Path.GetDirectoryName(path);
            this.IsDir = isDir;
            this.Length = size;
            this.Path = path;
        }

        public Resource(string name, string path, long size, bool isDir)
        {
            this.Name = name;
            this.IsDir = isDir;
            this.Length = size;
            this.Path = path;
        }

        public void Delete()
        {
            try
            {
                if (IsDir)
                    System.IO.Directory.Delete(Path);
                else System.IO.File.Delete(Path);
            }
            catch { }
        }

        public IEnumerator<char> GetEnumerator()
        {
            string sCurr = "{\"n\": \"";
            foreach (var c in sCurr)
                yield return c;
            foreach (var c in Name)
                yield return c;
            sCurr = "\", \"d\": ";
            foreach (var c in sCurr)
                yield return c;
            sCurr = IsDir ? "true" : "false";
            foreach (var c in sCurr)
                yield return c;
            if (!IsDir)
            {
                sCurr = ", \"s\": ";
                foreach (var c in sCurr)
                    yield return c;
                sCurr = Length.ToString();
                foreach (var c in sCurr)
                    yield return c;
            }
            yield return '}';
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}