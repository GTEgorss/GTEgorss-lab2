using System;
using System.Collections.Generic;

namespace CSharpClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            int command;
            Console.WriteLine("1 - Get");
            Console.WriteLine("2 - Post");
            Console.WriteLine("0 - Exit");

            Client.Client client = new();

            bool exit = false;
            while (!exit)
            {
                command = Convert.ToInt32(Console.ReadLine());

                switch (command)
                {
                    case 1:
                        Console.WriteLine("Type id");
                        int id = Convert.ToInt32(Console.ReadLine());
                        client.getZxc(id);
                        break;
                    case 2:
                        Console.WriteLine("Type id, name and hoursInDota2");
                        int playerId = Convert.ToInt32(Console.ReadLine());
                        string name = Console.ReadLine() ?? "";
                        
                        int hoursInDota2 = Convert.ToInt32(Console.ReadLine());
                        List<string> achievements = new List<string>() {"The worst", "The worst of the worst"};
                        client.newZxc(playerId, name, hoursInDota2, achievements);
                        break;
                    case 0:
                        exit = true;
                        break;
                }
            }
        }
    }
}