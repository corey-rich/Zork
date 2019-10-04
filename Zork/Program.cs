using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultRoomsFilename = "Zork.json";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFilename);

            Game game = Game.Load(roomsFilename);
            Console.WriteLine("Welcome to Zork!");
            game.Run();
            Console.WriteLine("Thank you for playing!");
        }

        private enum CommandLineArguments
        {
            RoomsFilename = 0
        }
    }
}

