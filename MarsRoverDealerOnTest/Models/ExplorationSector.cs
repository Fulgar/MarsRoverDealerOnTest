using System;

namespace MarsRoverDealerOnTest.Models
{
    // Represents the explorable section of the Mars Plateau
    public class ExplorationSector
    {
        // Complete Constructor
        public ExplorationSector(int easternBound, int northernBound)
        {
            this.EasternBound = easternBound;
            this.NorthernBound = northernBound;
        }
        
        // No-Args Constructor
        public ExplorationSector()
        {
        }

        // Eastern Border
        public int EasternBound { get; set; }
        
        // Northern Border
        public int NorthernBound { get; set; }

        // Will legally move rover within exploration sector bounds
        public void MoveRover (Rover rover, string movementInstructions)
        {
            foreach (char instruction in movementInstructions)
            {
                switch (instruction)
                {
                    case 'L':
                        rover.TurnLeft();
                        break;
                    case 'R':
                        rover.TurnRight();
                        break;
                    case 'M':
                        if (CanRoverMove(rover))
                        {
                            rover.Move();
                        }
                        else
                        {
                            WriteError("ERROR: Rover cannot follow specified path: Out of Bounds!");
                            return;
                        }
                        break;
                }
            }
            Console.WriteLine("New Rover Position: " + rover.X + " " + rover.Y + " " + rover.Direction + "\n");
        }

        // Returns true if no restrictions on rover's movement
        private bool CanRoverMove(Rover rover)
        {
            switch (rover.Direction)
            {
                case 'N':
                    return rover.Y < NorthernBound;
                case 'E':
                    return rover.X < EasternBound;
                case 'S':
                    return rover.Y > 0;
                case 'W':
                    return rover.X > 0;
                default:
                    return false;
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