using Newtonsoft.Json;
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
        public Block(DateTime timeStamp, string previousHash, IList<Transaction> transactions)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Hash = CalculateHash();
            Transactions = transactions;
            Nonce = 0;
        }

        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public int Nonce { get; set; }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inbytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{JsonConvert.SerializeObject(Transactions)}-{Nonce}");
            byte[] outbytes = sha256.ComputeHash(inbytes);

            return Convert.ToBase64String(outbytes);
        }

        public void Mine(int difficulty)
        {
            var leadingZeros = new String('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }
}
