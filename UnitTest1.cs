using Microsoft.Playwright;
using NUnit.Framework;
using System.Reflection;
using System.Xml.Linq;

[TestFixture]
public class SnakeGameTests
{
    [Test]
    public void SnakeGame_GameOverIsTrueWhenSnakeHitsWall()
    {
        var snakeGame = new SnakeGame(20, 10);

        var isGameOver = (bool)GetNonPublicMethod(snakeGame, "IsGameOver").Invoke(snakeGame, new object[] { new Position(0, 5) });

        Assert.IsTrue(isGameOver);
    }

    [Test]
    public void SnakeGame_GameOverIsTrueWhenSnakeCollidesWithItself()
    {
        var snakeGame = new SnakeGame(20, 10);
        var snake = (Snake)GetNonPublicField(snakeGame, "snake").GetValue(snakeGame);
        snake.Body.Add(new Position(10, 5));
        snake.Body.Add(new Position(9, 5));
        snake.Body.Add(new Position(8, 5));

        var isGameOver = (bool)GetNonPublicMethod(snakeGame, "IsGameOver").Invoke(snakeGame, new object[] { new Position(10, 5) });

        Assert.IsTrue(isGameOver);
    }

    [Test]
    public void SnakeGame_GameOverIsFalseWhenSnakeDoesNotHitWallOrCollideWithItself()
    {
        var snakeGame = new SnakeGame(20, 10);

        var isGameOver = (bool)GetNonPublicMethod(snakeGame, "IsGameOver").Invoke(snakeGame, new object[] { new Position(5, 5) });

        Assert.IsFalse(isGameOver);
    }


    private MethodInfo GetNonPublicMethod(object obj, string methodName)
    {
        return obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
    }

    private FieldInfo GetNonPublicField(object obj, string fieldName)
    {
        return obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
    }
}
