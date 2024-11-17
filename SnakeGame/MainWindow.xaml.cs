﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeGame
{
    ///code behind
    public partial class MainWindow : Window
    {
        //add a dictionary which maps the grid values to image sources 
        private readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            //if a grid position is empty => we want to display an empty image asset
            {GridValue.Empty, Images.Empty },
            //if the position contains parts of the snake => we want to show the body image
            {GridValue.Snake, Images.Body },
            //if the position contains food => then we show the food image
            {GridValue.Food, Images.Food }

            //note that we omitted the type after new() keyword => you can only do this in newer versions of C#
            //so if you are having problems the just write the type again
        };       

        //we need 2 variables for the number of rows and columns, we use 15 rows and 15 columns
        private readonly int rows = 15, cols = 15;

        //add a 2d image array for the image controls
        //this array will make it easy to access the image for a given position in the grid
        private readonly Image[,] gridImages;

        //we also need a GameState object which we will initialize in the constructor
        private GameState gameState;

        //add a boolean called gameRunning, it is false by default which is what we need
        private bool gameRunning;


        public MainWindow()
        {
            InitializeComponent();

            //Call SetupGrid() method and save the returned array and grid images
            gridImages = SetupGrid();

            //initialize a gameState object
            gameState = new GameState(rows, cols);

        }

        //turn Window_Loaded(object sender, RoutedEventArgs e) into RunGame() method
        //when a user presses the key for the first time, we should call RunGame() method
        private async Task RunGame()
        {
            //here we will call Draw() method 
            Draw();

            //call ShowCountDown() method
            await ShowCountDown();

            //hide overlay
            Overlay.Visibility = Visibility.Hidden;           

            //start the game loop
            await GameLoop();

            //when the game loop ends => it means that the game is over 
            //so, we will call ShowGameOver() after that
            await ShowGameOver();

            //amd we also create a fresh game state for the next game
            gameState = new GameState(rows, cols);
        }

        //when a user presses a key, then Window_PreviewKeyDown() is called
        //and after that Window_KeyDown() is also called
        //but we can do smth clever
        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if the overlay is visible, we set the event's Handled property to true
            //this will prevent Window_KeyDown() from being called
            //so, while overlay is visible, a key press will only cause the Window_PreviewKeyDown() to be called
            if (Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }

            //if the game is not already running, we set gameRunning to true
            if (!gameRunning)
            {
                gameRunning = true;

                //await RunGame()
                await RunGame();

                //and when that method is complete, we set gameRunning back to false
                gameRunning = false;
            }
        }

        //handle some keyboards inputs
        //Method to be called when a user enters a key
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if the game is over, then pressing a key should not do anything => so we simply return
            if (gameState.GameOver)
            {
                return;
            }

            //otherwise we check which key was pressed 
            switch (e.Key)
            {
                //we will use the arrow keys but we can use any keys we like
                //if the user presses the left arrow key => we change the snake's direction to left
                case Key.Left:
                    gameState.ChangeDirection(Direction.Left); 
                    break;
                //similarly for the other arrow keys
                case Key.Right:
                    gameState.ChangeDirection(Direction.Right);
                    break;
                case Key.Up:
                    gameState.ChangeDirection(Direction.Up);
                    break;
                case Key.Down:
                    gameState.ChangeDirection(Direction.Down);
                    break;
                //now we can change the snake's direction, but we need to do it at regular intervals
                //for that we add an async game loop method

            }

        }
        //Async Method to change the snake's direction at regular intervals
        private async Task GameLoop()
        {
            //the loop will run until the game is over 
            while (!gameState.GameOver)
            {
                //in the body we add a small delay - I am using 500 milliseconds
                //but you can change this to make the game slower or faster
                await Task.Delay(500);

                //after the delay we call the Move() method
                gameState.Move();

                //and then draw the new game state
                Draw();
            }
        }

        //Method to set up grid
        //it will add the required image controls to the game grid and return them in a 2d array for easy access
        private Image[,] SetupGrid()
        {
            //we start by creating a 2d array 
            Image[,] images = new Image[rows, cols];

            //next we have to set up the number of rows and columns on the game grid
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;

            //then we loop over all grid positions 
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    //for each of them we create a new image 
                    Image image = new Image
                    {
                        //initially I want its source to be empty image asset
                        Source = Images.Empty
                    };

                    //we store this image in the 2d array
                    images[r, c] = image;

                    //and add it as a child of the game grid
                    GameGrid.Children.Add(image);
                }
            }

            //outside the loops we return the images array
            return images;

            //in the constructor we call this method
        }

        //Method to draw (general method)
        private void Draw()
        {
            DrawGrid();

            //set the score text to score followed by the actual score stored in the gameState
            ScoreText.Text = $"SCORE: {gameState.Score}";
        }


        //Method to draw a game grid
        //this method will look at the grid array in the gameState and update the images to reflect it
        private void DrawGrid()
        {
            //it loops through every grid position 
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0;c < cols; c++)
                {
                    //inside the loop we get the grid value at the current position 
                    GridValue gridVal = gameState.Grid[r, c];
                    //and set the source for the corresponding image using our dictionary
                    gridImages[r, c].Source = gridValToImage[gridVal];
                }
            }
            //we will call DrawGrid() from a more general Draw() method
        }

        //Method to show a countdown
        private async Task ShowCountDown()
        {
            //we loop from 3 down to 1
            for (int i = 3; i >= 1; i--)
            {
                //for each iteration we make the overlay text display the value of i,
                //followe by a small delay
                OverlayText.Text = i.ToString();
                await Task.Delay(500);
            }
            //we can call this method from RunGame()
        }

        //Method to show Game Over
        private async Task ShowGameOver()
        {
            //it starts with a 1 sec delay
            await Task.Delay(1000);

            //and then makes the overlay visible again
            Overlay.Visibility = Visibility.Visible;

            //in ShowCountDown() method we changes the overlay text, so we must change it back to 'Press any key to start'
            OverlayText.Text = "PRESS ANY KEY TO START";
        }
    }
}