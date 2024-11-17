using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SnakeGame
{
    //Images class will basically be a container for all our image assets
    //we can get rid of all our imports except for System
    //we also need Windows.System.Media and Windows.System.Media.Imaging
    public static class Images
    {
        //add static variables for each of our image assets
        public readonly static ImageSource Empty = LoadImage("Empty.png");
        //and similarly for the rest of the images
        public readonly static ImageSource Body = LoadImage("Body.png");
        public readonly static ImageSource Head = LoadImage("Head.png");
        public readonly static ImageSource Food = LoadImage("Food.png");
        public readonly static ImageSource DeadBody = LoadImage("DeadBody.png");
        public readonly static ImageSource DeadHead = LoadImage("DeadHead.png");

        //for conveniance let's add a private method LoadImage()
        //Method to load the image with the given file name and returns it as an image source
        private static ImageSource LoadImage(string fileName)
        {
            return new BitmapImage(new Uri($"Assets/{fileName}", UriKind.Relative));
        }      
    }
}
