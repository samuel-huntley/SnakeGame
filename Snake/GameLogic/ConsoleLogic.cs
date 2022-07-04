using Snake.Models;
using Snake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Enums;

namespace Snake.GameLogic
{
    public class ConsoleLogic
    {
        public static void UpdateGrid(GameGridModel model, SnakeModel snake)
        {
            model.gridMatrix = new string[model.GridWidth, model.GridHeight];

            foreach (var coordinate in model.GridSpots)
            {
                if (coordinate.Type == CoordinateTypeEnum.Normal)
                {
                    model.gridMatrix[coordinate.X, coordinate.Y] = " ";
                }
                else if (coordinate.Type == CoordinateTypeEnum.Border)
                {
                    model.gridMatrix[coordinate.X, coordinate.Y] = "*";
                }
                else if (coordinate.Type == CoordinateTypeEnum.Food)
                {
                    model.gridMatrix[coordinate.X, coordinate.Y] = "§";
                }
                foreach (var node in snake.SnakeNodes)
                {
                    model.gridMatrix[node.X, node.Y] = "@";
                }
            }
            DrawGrid(model, snake);
        }

        internal static void Welcome()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("Welcome to Snake 1.0!");
            Console.WriteLine("created by Samuel Huntley");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.WriteLine("****************************");
            Console.ReadLine();
            Console.Clear();
        }

        private static void DrawGrid(GameGridModel model, SnakeModel snake)
        {
            for (int y = 0; y < model.GridHeight; y++)
            {
                for (int x = 0; x < model.GridWidth; x++)
                {
                    Console.Write(model.gridMatrix[x, y]);
                    if (x == model.gridMatrix.GetLength(0) - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
            ShowScore(snake);
        }
        private static void ShowScore(SnakeModel snake)
        {
            Console.WriteLine($"Score: {snake.Score}");
        }
        public static void UpdateSnakeNodeLocations(SnakeModel snake)
        {
            for (int i = snake.SnakeNodes.Count - 1; i >= 0; i--)
            {
                if (snake.SnakeNodes[i].Head == null)
                {
                    if (snake.Direction == Directions.Left)
                    {
                        snake.SnakeNodes[i].X -= 1;
                    }
                    else if (snake.Direction == Directions.Right)
                    {
                        snake.SnakeNodes[i].X += 1;
                    }
                    else if (snake.Direction == Directions.Up)
                    {
                        snake.SnakeNodes[i].Y -= 1;
                    }
                    else if (snake.Direction == Directions.Down)
                    {
                        snake.SnakeNodes[i].Y += 1;
                    }
                }
                if (snake.SnakeNodes[i].FirstNode != true)
                {
                    snake.SnakeNodes[i].Y = snake.SnakeNodes[i].Head.Y;
                    snake.SnakeNodes[i].X = snake.SnakeNodes[i].Head.X;
                }
            }
        }
        public static void GameOver(int score)
        {
            Console.Clear();
            Console.WriteLine("****************************");
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Player score was: {score}");
            Console.WriteLine("****************************");
            Console.ReadLine();
        }
    }
}
