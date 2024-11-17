using System.Text;
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


        public MainWindow()
        {
            InitializeComponent();

            //Call SetupGrid() method and save the returned array and grid images
            gridImages = SetupGrid();

            //initialize a gameState object
            gameState = new GameState(rows, cols);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //here we will cal Draw() method 
            Draw();
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

            //for now it's a bit redundant but it will do more things soon
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
    }
}