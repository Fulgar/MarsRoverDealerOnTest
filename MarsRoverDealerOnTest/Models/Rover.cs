using System.Collections.Generic;

namespace MarsRoverDealerOnTest.Models
{
    public class Rover
    {
        // Complete Constructor
        public Rover(int x, int y, char direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
        
        // No-Args Constructor
        public Rover()
        {
        }

        // Longitude
        public int X { get; set; }
        
        // Latitude
        public int Y { get; set; }
        
        // Rotation
        public char Direction { get; set; }

        // Rotates rover left
        public void TurnLeft()
        {
            LinkedList<char> possibleDirections = new LinkedList<char>() {};
            possibleDirections.AddLast('N');
            possibleDirections.AddLast('E');
            possibleDirections.AddLast('S');
            possibleDirections.AddLast('W');
            
            // If there is a previous value in LinkedList, then set Direction to that value
            var linkedListNode = possibleDirections.Find(Direction)?.Previous;
            if (linkedListNode != null)
            {
                Direction = linkedListNode.Value;
            }
            else
            {
                // If no previous node exists then set Direction to last node
                Direction = possibleDirections.Last.Value;
            }
        }
        
        // Rotates rover right
        public void TurnRight()
        {
            LinkedList<char> possibleDirections = new LinkedList<char>() {};
            possibleDirections.AddLast('N');
            possibleDirections.AddLast('E');
            possibleDirections.AddLast('S');
            possibleDirections.AddLast('W');
            
            // If there is a next value in LinkedList, then set Direction to that value
            var linkedListNode = possibleDirections.Find(Direction)?.Next;
            if (linkedListNode != null)
            {
                Direction = linkedListNode.Value;
            }
            else
            {
                // If no next node exists then set Direction to first node
                Direction = possibleDirections.First.Value;
            }
        }
        
        // Moves Rover in the direction it is facing
        public void Move()
        {
            switch (Direction)
            {
                case 'N':
                    Y++;
                    break;
                case 'E':
                    X++;
                    break;
                case 'S':
                    Y--;
                    break;
                case 'W':
                    X--;
                    break;
            }
        }
    }
}