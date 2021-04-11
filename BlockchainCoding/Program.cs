using Newtonsoft.Json;
using System;

namespace BlockchainCoding
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain blockchain = new Blockchain();

            DateTime startTime = DateTime.Now;

            blockchain.CreateTransaction(new Transaction("Beşir", "Esat",117));
            blockchain.ProcessPendingTransaction("Zeyni");

            blockchain.CreateTransaction(new Transaction("Esat", "Beşir", 17));
            blockchain.CreateTransaction(new Transaction("Beşir", "Esat", 10));
            blockchain.ProcessPendingTransaction("Rıdvan");

            Console.WriteLine($"Beşir balance : {blockchain.GetBalance("Beşir")}");
            Console.WriteLine($"Esat balance : {blockchain.GetBalance("Esat")}");
            Console.WriteLine($"Zeyni balance : {blockchain.GetBalance("Zeyni")}");
            Console.WriteLine($"Rıdvan balance : {blockchain.GetBalance("Zeyni")}");


            DateTime finishTime = DateTime.Now;

            Console.WriteLine($"Geçen süre =  {(finishTime-startTime).ToString()}");

            Console.WriteLine(JsonConvert.SerializeObject(blockchain,Formatting.Indented));

            Console.WriteLine($"Geçerli mi => {blockchain.IsValid() }");

            Console.ReadKey();

        }
    }
}
