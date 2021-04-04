using Newtonsoft.Json;
using System;

namespace BlockchainCoding
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain blockchain = new Blockchain();

            blockchain.AddBlock(new Block(DateTime.Now, null, "{sender:Besir,receiver:Zeyni,amount:5}"));
            blockchain.AddBlock(new Block(DateTime.Now, null, "{sender:Rıdvan,receiver:Raddat,amount:55}"));
            blockchain.AddBlock(new Block(DateTime.Now, null, "{sender:Raddat,receiver:Ömer,amount:15}"));

            Console.WriteLine(JsonConvert.SerializeObject(blockchain, Formatting.Indented));
            Console.WriteLine($"Geçerli mi => {blockchain.IsValid() }");

            Console.WriteLine("Veriyi değiştiriyoruz...");
            blockchain.Chain[1].Data = "{sender:Ömer,receiver:Beşir,amount:39}";
            Console.WriteLine($"Geçerli mi => {blockchain.IsValid() }");

            Console.WriteLine("Hash güncellendi...");
            blockchain.Chain[1].Hash = blockchain.Chain[1].CalculateHash();
            Console.WriteLine($"Geçerli mi => {blockchain.IsValid() }");

            Console.WriteLine("%51 ele geçirildiğinde...");
            blockchain.Chain[2].PreviousHash = blockchain.Chain[1].Hash;
            blockchain.Chain[2].Hash = blockchain.Chain[2].CalculateHash();
            blockchain.Chain[3].PreviousHash = blockchain.Chain[2].Hash;
            blockchain.Chain[3].Hash = blockchain.Chain[3].CalculateHash();
            Console.WriteLine($"Geçerli mi => {blockchain.IsValid() }");

            Console.ReadKey();

        }
    }
}
