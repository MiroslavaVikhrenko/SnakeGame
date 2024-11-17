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
        //we need 2 variables for the number of rows and columns, we use 15 rows and 15 columns
        private readonly int rows = 15, cols = 15;

        //add a 2d image array for the image controls
        //this array will make it easy to access the image for a given position in the grid
        private readonly Image[,] gridImages;

        public MainWindow()
        {
            InitializeComponent();

            //Call SetupGrid() method and save the returned array and grid images
            gridImages = SetupGrid();

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
    }
}