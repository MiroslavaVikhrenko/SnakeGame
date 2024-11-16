using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    //GameState class stores the current state of the game
    public class GameState
    {
        //properties: number of rows and columns in the grid
        public int Rows { get; }
        public int Cols { get; }
        //the grid itself which is a 2-dimensional rectangular array of grid values
        public GridValue[,] Grid { get; }

        //the snake also has a direction which dictates where it will move next
        public Direction Dir { get; private set; }

        //score property
        public int Score { get; private set; }

        //game over boolean
        public bool GameOver { get; private set; }

        //it will be convenient to keep a list containing the positions currently occupied by the snake
        //we use a linked list because it allows us to add and delete from both ends of the list
        //we use the convention that the first element is the head of the snake and
        //the last element is the tail
        private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();

        //we also need a random object variable
        //it will be used to figure out where the food should spawn
        private readonly Random random = new Random();

        //constructor to take number of rows and columns in the grid as parameters
        public GameState(int rows, int cols)
        {
            //first we store these numbers in the properties
            Rows = rows;
            Cols = cols;
            //then initialize our 2-d array with the correct size
            Grid = new GridValue[rows, cols];
            //at this point every position in the array will contain GridValue.Empty becuase it's the first enum value

            //when the game starts I want this next direction to be right
            Dir = Direction.Right;
        }

        //Method to add a snake to the grid
        private void AddSnake()
        {

        }

    }
}
