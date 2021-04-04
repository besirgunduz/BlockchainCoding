using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainCoding
{
    public class Block
    {
        public Block(DateTime timeStamp, string previousHash, string data)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Data = data;
            Hash = CalculateHash();
        }

        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public string Data { get; set; }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inbytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Data}");
            byte[] outbytes = sha256.ComputeHash(inbytes);

            return Convert.ToBase64String(outbytes);
        }
    }
}
