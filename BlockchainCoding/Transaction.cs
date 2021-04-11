using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainCoding
{
    public class Transaction
    {
        public Transaction(string sender, string receiver, int amount)
        {
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
        }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int Amount { get; set; }
    }
}
