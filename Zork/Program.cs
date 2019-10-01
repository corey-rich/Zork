﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Zork
{
    class Program
    {
        private static Room CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            string roomsFilename = args.Length > 0 ? args[0] : "Rooms.txt";
            InitializeRoomDescriptions(roomsFilename);

            Room previousRoom = null;
            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);

                if (previousRoom != CurrentRoom)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                }

                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(CurrentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }
        private static bool Move(Commands command)
        {
            Assert.IsTrue(Directions.Contains(command), "Invalid direction.");

            bool movedSuccessfully = true;
            switch (command)
            {
                case Commands.SOUTH when Location.Row > 0:
                    Location.Row--;
                    break;

                case Commands.NORTH when Location.Row < Rooms.GetLength(0) - 1:
                    Location.Row++;
                    break;

                case Commands.EAST when Location.Column < Rooms.GetLength(1) - 1:
                    Location.Column++;
                    break;

                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    break;

                default:
                    movedSuccessfully = false;
                    break;
            }

            return movedSuccessfully;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        private static void InitializeRoomDescriptions(string roomsFilename)
        {
            Dictionary<string, Room> roomMap = new Dictionary<string, Room>();
            foreach(Room room in Rooms)
            {
                roomMap[room.Name] = room;
            }

            string[] lines = File.ReadAllLines(roomsFilename);
            foreach (string line in lines)
            {
                const string fieldDelimiter = "##";
                const int expectedFieldCount = 2;

                string[] fields = line.Split(fieldDelimiter);
                if(fields.Length != expectedFieldCount)
                {
                    throw new InvalidDataException("Invalid record.");
                }

                string name = fields[(int)Fields.Name];
                string description = fields[(int)Fields.Description];

                roomMap[name].Description = description;
            }
        }

        private static readonly Room[,] Rooms = {
            {new Room ("Rocky Trail"),  new Room ("South of House"),    new Room ("Canyon View")    },
            {new Room ("Forest"),       new Room ("West of House"),     new Room ("Behind House")   },
            {new Room ("Dense Woods"),  new Room ("North of House"),    new Room ("Clearing")       }
        };                              

        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private enum Fields
        {
            Name = 0,
            Description = 1
        }

        private static (int Row, int Column) Location = (1, 1);
    }
}
