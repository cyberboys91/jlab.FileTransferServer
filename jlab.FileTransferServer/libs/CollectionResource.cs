using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace jlab.FileTransferServer.libs
{
    class FsCollectionResource : List<Resource>
    {
        public IEnumerable<char> GetChars()
        {
            foreach (var fsRes in this)
            {
                foreach (var cRes in fsRes.ToArray())
                    yield return cRes;
                yield return '\n';
            }
        }

        public void Write(StreamWriter stream) { 
            Clear();
        }
    }
}
