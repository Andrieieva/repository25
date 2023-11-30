public class Foods
{
    private List<Food> foods;
    private Snake snake;
    private int screenWidth;
    private int screenHeight;

    public Foods(Snake snake, int screenWidth, int screenHeight)
    {
        this.snake = snake;
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        foods = new List<Food>();
        SpawnFood(3);
    }

    public void DrawFoods()
    {
        foreach (var food in foods)
        {
            food.Draw();
        }
    }

    public void CheckEating()
    {
        Position head = snake.Head;

        for (int i = 0; i < foods.Count; i++)
        {
            if (head.Equals(foods[i].Position))
            {
                foods[i].EatFood(snake);
                foods.RemoveAt(i);
                SpawnFood(1);
                break;
            }
        }
    }

    private void SpawnFood(int count)
    {
        Random random = new Random();
        for (int i = 0; i < count; i++)
        {
            int x, y;
            do
            {
                x = random.Next(1, screenWidth - 1);
                y = random.Next(1, screenHeight - 1);
            } while (snake.Body.Any(segment => segment.X == x && segment.Y == y) || foods.Any(food => food.Position.X == x && food.Position.Y == y));

            int foodType = random.Next(1, 4);
            Food newFood;

            switch (foodType)
            {
                case 1:
                    newFood = new AppleFood(x, y);
                    break;
                case 2:
                    newFood = new BonusFood(x, y);
                    break;
                case 3:
                    newFood = new RottenFood(x, y);
                    break;
                default:
                    newFood = new AppleFood(x, y); // Default to AppleFood
                    break;
            }

            foods.Add(newFood);
        }
    }
}

[Serializable]
public record Position(int X, int Y);

internal class FoodPosition : Food
{
    public FoodPosition(int x, int y) : base(new Position(x, y))
    {
    }

    public override void Draw()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write("o");
    }

    public override void EatFood(Snake snake)
    {
        snake.Length++;
        snake.Score += 5;
    }
}

public abstract class Food
{
    public Position Position { get; }

    protected Food(Position position)
    {
        Position = position;
    }

    public abstract void EatFood(Snake snake);

    public abstract void Draw();
}

public class AppleFood : Food
{
    public AppleFood(int x, int y) : base(new Position(x, y))
    {
    }

    public override void EatFood(Snake snake)
    {
        snake.Length++;
        snake.Score += 5;
        snake.Body.Add(Position);
    }

    public override void Draw()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write("o");
    }
}

public class BonusFood : Food
{
    public BonusFood(int x, int y) : base(new Position(x, y))
    {
    }

    public override void EatFood(Snake snake)
    {
        snake.Lives++;
    }

    public override void Draw()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write("o");
    }
}

public class RottenFood : Food
{
    public RottenFood(int x, int y) : base(new Position(x, y))
    {
    }

    public override void EatFood(Snake snake)
    {
        snake.Lives--;

        if (snake.Lives <= 0)
        {
            Console.WriteLine("Game Over Condition Met!");
        }
    }
    public override void Draw()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write("o");
    }
}
