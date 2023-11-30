public class Snake
{
    public List<Position> Body { get; private set; }

    public Snake(int initialX, int initialY)
    {
        Body = new List<Position> { new Position(initialX, initialY) };
    }

    public void Move(Position newHead)
    {
        Body.Add(newHead);
    }

    public Position CalculateNewHeadPosition(Direction direction)
    {
        Position head = Head;
        switch (direction)
        {
            case Direction.Up:
                return new Position(head.X, head.Y - 1);
            case Direction.Down:
                return new Position(head.X, head.Y + 1);
            case Direction.Left:
                return new Position(head.X - 1, head.Y);
            case Direction.Right:
                return new Position(head.X + 1, head.Y);
            default:
                return head;
        }
    }

    public Position Head => Body.Last();

    public int Lives { get; internal set; }
    public int Length { get; internal set; }
    public int Score { get; internal set; }

    public bool EatFood(List<Food> foods)
    {
        Position head = Body.Last();

        for (int i = 0; i < foods.Count; i++)
        {
            if (head.Equals(foods[i].Position))
            {
                foods[i].EatFood(this);
                foods.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public bool IsCollisionWithSelf(Position newHead)
    {
        return Body.GetRange(0, Body.Count - 1).Contains(newHead);
    }
}

