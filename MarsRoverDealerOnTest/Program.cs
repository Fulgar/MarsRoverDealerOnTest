using System;
using System.Linq;
using MarsRoverDealerOnTest.Models;

namespace MarsRoverDealerOnTest
{
    internal static class Program
    {
        // Driver Method
        public static void Main(string[] args)
        {
            // External loop to allow for user-error correction during map creation
            while (true)
            {
                // Will obtain exploration bounds
                ExplorationSector explorationSector = GetExplorationSector();

                if (explorationSector != null)
                {
                    // Internal loop to allowed for continuous rover control
                    while (true)
                    {
                        RoverCommand(explorationSector);
                    }
                }
            }
        }

        // Creates ExplorationSector via user-command input
        private static ExplorationSector GetExplorationSector()
        {
            // Prompt user for Eastern and Northern constraints
            Console.WriteLine("Enter the eastern and northern grid bounds, separated by a space.");
            string explorationGridBoundsInput = Console.ReadLine();
                            
            if (explorationGridBoundsInput != null)
            {
                // Divide Input result into separate strings by space char
                string[] explorationGridBoundsArr = explorationGridBoundsInput.Split(' ');
                                
                // Validate that number of input entries is correct
                if (explorationGridBoundsArr.Length != 2)
                {
                    WriteError("ERROR: Unexpected number of tokens: expected two numbers");
                    return null;
                }
                
                else
                {
                    // Represents how far east and north a rover can venture
                    int easternBound = 0;
                    int northernBound = 0;
                    
                    // Validate that input entries are integers and output them into the easternBound and northernBound int variables
                    bool isEasternBoundInteger = int.TryParse(explorationGridBoundsArr[0], out easternBound);
                    bool isNorthernBoundInteger = int.TryParse(explorationGridBoundsArr[1], out northernBound);
                
                    if (!isEasternBoundInteger || !isNorthernBoundInteger)
                    {
                        WriteError("ERROR: Non-Integer Input detected!");
                        return null;
                    }
                    else
                    {
                        if (easternBound < 0 || northernBound < 0)
                        {
                            WriteError("ERROR: Negative Bound detected, must be positive bound");
                            return null;
                        }
                        ExplorationSector explorationSector = new ExplorationSector(easternBound, northernBound);
                        return explorationSector;
                    }
                }
            }
            else
            {
                WriteError("ERROR: Null Input Received!");
                return null;
            }
        }

        // Method takes user commands to move rover
        private static void RoverCommand(ExplorationSector explorationSector)
        {
            // Prompts User for starting position of a drone
            Console.WriteLine("\nEnter Rover Position via X-coord, Y-coord, and cardinal direction symbol, separated by spaces");
            string originalPosInput = Console.ReadLine();
            
            // Prompts User for movement instructions
            Console.WriteLine("Enter movement instructions (NO SPACES)");
            Console.WriteLine("M = Move forward, L = Turn 90 degrees left, R = Turn 90 degrees right");
            string movementInstructionsInput = Console.ReadLine();
            
            if (originalPosInput != null && movementInstructionsInput != null)
            {
                // Validate that movement instructions didn't have any invalid characters in it
                if (movementInstructionsInput.Any(letter => !new[] {'M', 'L', 'R'}.Contains(letter)))
                {
                    WriteError("ERROR: Movement Instructions contained unknown character; Expected M, L, or R");
                    return;
                }
                
                // Divide Input result into separate strings by space char
                string[] originalPosArr = originalPosInput.Split(' ');
                                
                // Validate that number of input entries is correct
                if (originalPosArr.Length != 3)
                {
                    WriteError("ERROR: Unexpected number of tokens: expected two numbers, followed by one cardinal letter");
                }
                else
                {
                    // Validate that input entries are correct data types, and output them to respective variables
                    bool isXInteger = int.TryParse(originalPosArr[0], out int x);
                    bool isYInteger = int.TryParse(originalPosArr[1], out int y);
                    if (!isXInteger || !isYInteger)
                    {
                        WriteError("ERROR: Non-numeric character detected in coordinates");
                        return;
                    }
                    
                    // Validate cardinal direction input is only one character long
                    if (originalPosArr[2].Length != 1)
                    {
                        WriteError("ERROR: Cardinal Direction should only be one character long");
                    }
                    else
                    {
                        // Validate whether or not the cardinal direction contains N, E, S, or W
                        if (!new[] {'N', 'E', 'S', 'W'}.Contains(originalPosArr[2][0]))
                        {
                            WriteError("ERROR: Cardinal Direction should only contain either: N, E, S, or W ");
                        }
                        else
                        {
                            // Sets dir value
                            char dir = originalPosArr[2][0];

                            if (!(0 <= x && x <= explorationSector.EasternBound) ||
                                !(0 <= y && y <= explorationSector.NorthernBound))
                            {
                                WriteError("ERROR: X or Y coordinate is out of bounds");
                            }
                            else
                            {
                                // Creates rover
                                Rover rover = new Rover(x, y, dir);
                                
                                // Attempts to execute movement commands
                                explorationSector.MoveRover(rover, movementInstructionsInput);
                            }
                        }
                    }
                }
            }
            else
            {
                WriteError("ERROR: Null Input Received!");
            }
        }

        // Custom Error Logging Method for more readability
        private static void WriteError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg + "\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        
    }
}