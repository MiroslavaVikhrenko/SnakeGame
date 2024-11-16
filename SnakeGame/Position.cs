
namespace SnakeGame
{
    //Position class represents a position in the grid
    public class Position
    {
        //stores a row and a column
        public int Row { get; }
        public int Col { get; }

        //constructor which takes a row and a column as parameters
        public Position(int row, int col)
        {
            //save these values in properties
            Row = row;
            Col = col;
        }

        //Method to return the position we get by moving one step in the given direction
        public Position Translate(Direction dir)
        {
            //we return a new position
            //the row should be this position's row + the direction's row offset
            //the column should be this position's column + the direction's column offset
            return new Position(Row + dir.RowOffset, Col + dir.ColOffset);
        }

        //override equals and get hashcode + operators as well
        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Col == position.Col;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col);
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        

    }
}
