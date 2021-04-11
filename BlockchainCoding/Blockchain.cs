using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainCoding
{
    public class Blockchain
    {
        public Blockchain()
        {
            Chain = new List<Block>();
            PendingTransaction = new List<Transaction>();
            AddGenesisBlock();
        }

        public IList<Block> Chain { get; set; }
        public IList<Transaction> PendingTransaction { get; set; }

        readonly int difficult = 3;
        readonly int reward = 1;

        public Block CreateGenesisBlock()
        {
            Block block = new Block(DateTime.Now, null, PendingTransaction);
            block.Mine(difficult);//bloğa yazma hakkı elde etmesi gerekiyor.
            PendingTransaction = new List<Transaction>();
            return block;
        }
        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }
        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }
        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Hash = block.CalculateHash();
            block.Mine(difficult); // Kanıtlanmadığı sürece blok eklemeyecek.
            Chain.Add(block);
        }

        //bütün transactionları bekletip bir bloğun içinde gönderecez
        public void CreateTransaction(Transaction transaction)
        {
            PendingTransaction.Add(transaction);
        }

        //ödül verme
        public void ProcessPendingTransaction(string minerAddress)
        {
            CreateTransaction(new Transaction(null, minerAddress, reward));
            Block block = new Block(DateTime.Now, GetLatestBlock().Hash, PendingTransaction);
            AddBlock(block);
            PendingTransaction = new List<Transaction>();//bloğa yazdıktan sonra bütün trasları boşalttım.
            
        }
        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }

            return true;
        }

        public int GetBalance(string address)
        {
            int balance = 0;
            for (int i = 0; i < Chain.Count; i++)
            {
                for (int j = 0; j < Chain[i].Transactions.Count; j++)
                {
                    var transaction = Chain[i].Transactions[j];
                    if (transaction.Sender==address)
                    {
                        balance -= transaction.Amount;
                    }
                    if (transaction.Receiver==address)
                    {
                        balance += transaction.Amount;
                    }
                }
            }
            return balance;
        }
    }
}
