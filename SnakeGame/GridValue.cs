
namespace SnakeGame
{
    //enum for positions in the grid
    public enum GridValue
    {
        Empty,
        Snake,
        Food,
        Outside
    }
    //Outside => this value won't be stored in the grid array but it will be convenient in the code
    //when the snakes tries to move outside the grid.
}
