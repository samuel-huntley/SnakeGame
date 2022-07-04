using Snake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.GameLogic
{
    internal class GamePlayLogic
    {
        public static GameGridModel CreateGrid()
        {
            GameGridModel model = new GameGridModel();

            for (int x = 0; x < model.GridWidth; x++)
            {
                for (int y = 0; y < model.GridHeight; y++)
                {
                    CoordinateModel coordinate = new CoordinateModel();
                    coordinate.X = x;
                    coordinate.Y = y;
                    if (x == 0 || x == model.GridWidth - 1 || y == 0 || y == model.GridHeight - 1)
                    {
                        coordinate.Type = CoordinateTypeEnum.Border;
                    }
                    else
                    {
                        coordinate.Type = CoordinateTypeEnum.Normal;
                    }
                    model.GridSpots.Add(coordinate);
                }
            }
            return model;
        }

        public static void StartGame()
        {
            SnakeModel snake = new SnakeModel();
            GameGridModel grid = CreateGrid();
            Random rand = new Random();
            SnakeNodeModel head = new SnakeNodeModel
            {
                Head = null,
                X = rand.Next(1, grid.GridWidth - 2),
                Y = rand.Next(1, grid.GridHeight - 2),
                FirstNode = true
            };
            snake.SnakeNodes.Add(head);
            SpawnFood(grid);
            GameLoop(grid, snake);
        }

        private static void GameLoop(GameGridModel model, SnakeModel snake)
        {
            bool gameOver = false;
            Task.Factory.StartNew(() => KeyPressEvent.Press(snake));

            while (!gameOver)
            {
                ConsoleLogic.UpdateGrid(model, snake);
                gameOver = CheckLocations(model, snake);
                ConsoleLogic.UpdateSnakeNodeLocations(snake);

                Array.Clear(model.gridMatrix, 0, model.gridMatrix.Length);
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                System.Threading.Thread.Sleep(snake.Speed);
            }
            int score = snake.Score;
            ConsoleLogic.GameOver(score);
        }

        private static void SpawnFood(GameGridModel model)
        {
            Random rand = new Random();
            int x = rand.Next(1, model.GridWidth - 2);
            int y = rand.Next(1, model.GridHeight - 2);

            foreach (var coordinate in model.GridSpots)
            {
                if (coordinate.X == x && coordinate.Y == y)
                {
                    coordinate.Type = CoordinateTypeEnum.Food;
                    model.FoodLocation.Add(coordinate);
                }
            }
        }
        private static bool CheckLocations(GameGridModel model, SnakeModel snake)
        {
            bool isGameOver = false;

            //Check if crashes into tail
            for (int i = 0; i < snake.SnakeNodes.Count; i++)
            {
                int checkX = snake.SnakeNodes[i].X;
                int checkY = snake.SnakeNodes[i].Y;
                for (int k = 0; k < snake.SnakeNodes.Count; k++)
                {
                    if ((checkX == snake.SnakeNodes[k].X && checkY == snake.SnakeNodes[k].Y) &&
                        !snake.SnakeNodes[i].Equals(snake.SnakeNodes[k]))
                    {
                        isGameOver = true;
                        break;
                    }
                }
            }

            SnakeNodeModel firstNode = snake.SnakeNodes[0];
            SnakeNodeModel lastNode = snake.SnakeNodes.Last();

            //Check border and food
            foreach (var gridSpot in model.GridSpots)
            {
                if (gridSpot.Type == CoordinateTypeEnum.Border)
                {
                    if (firstNode.X == gridSpot.X && firstNode.Y == gridSpot.Y)
                    {
                        isGameOver = true;
                        break;
                    }
                }
                if (gridSpot.Type == CoordinateTypeEnum.Food)
                {
                    if (firstNode.X == gridSpot.X && firstNode.Y == gridSpot.Y)
                    {
                        snake.Score += 1;
                        SnakeNodeModel newNode = new SnakeNodeModel
                        { X = lastNode.X, Y = lastNode.Y, Head = lastNode };
                        snake.SnakeNodes.Add(newNode);
                        gridSpot.Type = CoordinateTypeEnum.Normal;
                        model.FoodLocation.Remove(gridSpot);
                        SpawnFood(model);
                    }

                }
            }
            return isGameOver;
        }
    }
}
