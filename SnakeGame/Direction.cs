
namespace SnakeGame
{
    //the class represents a direction in the grid
    public class Direction
    {
        //we only need 4 directions - left, right, up, down - which we will add as static variables

        //to move left from a given position in the grid => we must leave the row unchanged
        //and substract 1 column => therefore, the row offset should be 0, and the column offset should be -1
        public readonly static Direction Left = new Direction(0,-1);

        //to move right => don't change the row, but add 1 column
        public readonly static Direction Right = new Direction(0, 1);

        //to move up => substract 1 row and don't change the column
        public readonly static Direction Up = new Direction(-1, 0);

        //to move down => add 1 row and don't change the column
        public readonly static Direction Down = new Direction(1, 0);

        //encode the direction as 2 integers: a row offset and a column offset
        public int RowOffset { get; }
        public int ColOffset { get; }

        //private constructor which takes the row and column offsets as parameters
        private Direction(int rowOffset, int colOffset)
        {
            //set property to these values
            RowOffset = rowOffset;
            ColOffset = colOffset;

            //because the constructor is private, no other class can create an instance of the Direction class
            //and that is intentional
        }

        //Method to return direction's opposite
        public Direction Opposite()
        {
            return new Direction(-RowOffset, -ColOffset);
        }

        //override equals and get hashcode so the Direction class can be used as the key in the dictionary
        public override bool Equals(object obj)
        {
            return obj is Direction direction &&
                   RowOffset == direction.RowOffset &&
                   ColOffset == direction.ColOffset;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RowOffset, ColOffset);
        }

        public static bool operator ==(Direction left, Direction right)
        {
            return EqualityComparer<Direction>.Default.Equals(left, right);
        }

        public static bool operator !=(Direction left, Direction right)
        {
            return !(left == right);
        }      
    }
}
