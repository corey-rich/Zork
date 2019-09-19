using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(Rooms[PlayerPosition]);
                Console.WriteLine("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                            break;

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork! lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        bool movedSuccessfully = Move(command);
                        if (movedSuccessfully)
                        {
                            outputString = $"You moved {command}.";
                        }
                        else
                        {
                            outputString = "The way is shut!";
                        }
                        break;

                    default:
                        outputString = "Unknown command";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        private static bool Move(Commands command)
        {
            bool movedSuccessfully = false;

            switch (command)
            {
                
                case Commands.NORTH:
                case Commands.SOUTH:
                    movedSuccessfully = false;
                    break;

                case Commands.EAST:
                    if (PlayerPosition < Rooms.Length - 1)
                    {
                        PlayerPosition++;
                        movedSuccessfully = true;
                    }
                    break;

                case Commands.WEST:
                    if(PlayerPosition > 0)
                    {
                        PlayerPosition--;
                        movedSuccessfully = true;
                    }
                    break;

                default:
                    throw new ArgumentException();
            }

            return movedSuccessfully;
        }

        private static string[] Rooms = { "Forest", "West of House", "Behind House", "Behind House", "Clearing", "Canyon View"};
        private static int PlayerPosition = 1;
    }
}