class Program
{
    static void Main(string[] args)
    {
        Console.WindowHeight = 20;
        Console.WindowWidth = 44;
        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;

        var game = new SnakeGame(screenwidth, screenheight);
        game.Run();
    }
}
