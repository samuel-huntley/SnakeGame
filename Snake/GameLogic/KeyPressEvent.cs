using Snake.Enums;
using Snake.Models;
using System;

namespace Snake.GameLogic
{
    public class KeyPressEvent
    {
        public static void Press(SnakeModel snake)
        {
            do
            {
                var Key = Console.ReadKey().Key;

                switch (Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        snake.Direction = Directions.Up;
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        snake.Direction = Directions.Left;
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        snake.Direction = Directions.Down;
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        snake.Direction = Directions.Right;
                        break;
                }

            } while (true);
        }
    }
}
