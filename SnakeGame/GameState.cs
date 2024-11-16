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

            //add a snake
            AddSnake();

            //add food
            AddFood();
        }

        //Method to add a snake to the grid
        private void AddSnake()
        {
            //I want it to appear in the middle row in column 1, 2, 3
            //first we create a variable for the middle row
            //if you use even number of rows for your game then this row will be slightly closer to the top
            int r = Rows / 2;

            //next we loop over the columns from 1 to 3 
            for (int c = 1; c <= 3; c++)
            {
                //inside the loop we set the grid entry at r, c to GridValue.Snake
                Grid[r, c] = GridValue.Snake;
                //we must also remember to add this position to the snakePositions list
                snakePositions.AddFirst(new Position(r, c));
            }
            //now we can call AddSnake() from the constructor
        }

        //now we need to add food, but before we need to get empty grid positions

        //Method to return all empty grid positions
        private IEnumerable<Position> EmptyPositions()
        {
            //here we loop through all rows and columns 
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c <= Cols; c++)
                {
                    //inside the loop we check if the grid at r, c is Empty 
                    if (Grid[r,c] == GridValue.Empty)
                    {
                        //if so we yield return that position
                        yield return new Position(r, c);
                    }
                }
            }
        }

        //NOW WE CAN ADD FOOD

        //Method to add food
        private void AddFood()
        {
            //first we create a list of empty positions
            List<Position> empty = new List<Position>(EmptyPositions());

            //it is theoretically possible to beat snake in which case there wouldn't be any empty positions 
            //if someone actually does that it would be a bomber if the game crashed
            //so, if there are no empty positions we simply return

            if (empty.Count == 0)
            {
                return;
            }

            //in the general case we pick an empty position at random 
            Position pos = empty[random.Next(empty.Count)];
            //and set the corresponding array entry to GridValue.Food
            Grid[pos.Row,pos.Col] = GridValue.Food;

            //another way to do it would be repeatedly generate random positions and check if they are empty
            //if you choose this approach make sure you have a way of detecting when there are none of them.

            //AddFood() should be called in the constructor just like AddSnake()
        }

        //ADD A FEW SNAKE-RELATED METHODS

        //Method to return the position of the snake's head
        public Position HeadPosition()
        {
            //we can easily get this position from the linked list
            return snakePositions.First.Value;
        }

        //Similar method for the tail position
        public Position TailPosition()
        {
            //we can easily get this position from the other end of the linked list
            return snakePositions.Last.Value;
        }

        //Method to return all snake's positions as an IEnumerable
        //thos method will be handy when the snake dies and we turn it dark green
        public IEnumerable<Position> SnakePositions()
        {
            return snakePositions;
        }

        //note that these methods are PUBLIC
        //later we will use head position to add eyes to the snake and you could also add a special tail using tail position

        //ADD TWO METHODS FOR MODIFYING THE SNAKE

        //Method to add the given position to the front of the snake making it the new head
        private void AddHead(Position pos)
        {
            //so, we must add this position to the front of our list
            snakePositions.AddFirst(pos);
            //and set the corresponding entry of the grid array to GridValue.Snake
            Grid[pos.Row, pos.Col] = GridValue.Snake;
        }

        //Method to remove the tail
        private void RemoveTail()
        {
            //we start by getting the current tail position
            Position tail = snakePositions.Last.Value;
            //then we make that position empty in the grid
            Grid[tail.Row, tail.Col] = GridValue.Empty;
            //and remove it from the linked list
            snakePositions.RemoveLast();
        }

        //these two methods will be useful when we have to move the snake

        //NOW WE NEED TO ADD SOME PUBLIC METHODS FOR MODIFYING THE GAME STATE

        //Method to change the snake's direction
        public void ChangeDirection(Direction dir)
        {
            //for now it will simply set the direction property to the given direction parameter
            Dir = dir;
            //it's a bit too simplistic, but we will come back and change it once the problem becomes apparent
        }
    }
}
