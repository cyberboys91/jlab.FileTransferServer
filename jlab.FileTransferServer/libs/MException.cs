using System;

namespace jlab.FileTransferServer.libs
{
    class MException : ArgumentException
    {
        public MException(string message) : base(message)
        {

        }
    }
}
