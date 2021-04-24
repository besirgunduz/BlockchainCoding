using Newtonsoft.Json;
using System;

namespace BlockchainCoding
{
    class Program
    {
        public static Blockchain ourblockchain = new Blockchain();
        public static int Port = 0;
        public static P2PClient Client = new P2PClient();
        public static P2PServer Server = null;
        public static string name = "Unknown";
        static void Main(string[] args)
        {
            ourblockchain.InitializeChain();
            if (args.Length >= 1) Port = int.Parse(args[0]);
            if (args.Length >= 2) name = args[1];

            if (Port > 0)
            {
                Server = new P2PServer();
                Server.Start();
            }
            if (name != "Unknown") Console.WriteLine($"Su anki Kullanıcı:{name}");

            Console.WriteLine("=========================");
            Console.WriteLine("1. Server a Baglan");
            Console.WriteLine("2. Transaction Ekle");
            Console.WriteLine("3. Blockchain i Goster");
            Console.WriteLine("4. Cikis");
            Console.WriteLine("=========================");

            int selection = 0;
            while (selection != 4)
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("Lütfen Server URL ini Girin:");
                        string serverURL = Console.ReadLine();
                        Client.Connect($"{serverURL}/Blockchain");
                        break;
                    case 2:
                        Console.WriteLine("Lütfen Alici adini Girin");
                        string receiverName = Console.ReadLine();
                        Console.WriteLine("Miktari girin");
                        string amount = Console.ReadLine();
                        ourblockchain.CreateTransaction(new Transaction(name, receiverName, int.Parse(amount)));
                        ourblockchain.ProcessPendingTransactions(name);
                        Client.Broadcast(JsonConvert.SerializeObject(ourblockchain));
                        break;
                    case 3:
                        Console.WriteLine("Blockchain");
                        Console.WriteLine(JsonConvert.SerializeObject(ourblockchain, Formatting.Indented));
                        break;

                }

                Console.WriteLine("Lütfen bir seçenek seçin");
                string action = Console.ReadLine();
                selection = int.Parse(action);
            }

            Client.Close();
        }
    }
}
