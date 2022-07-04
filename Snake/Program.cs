using Snake.Enums;
using Snake.GameLogic;
using Snake.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Snake
{
    public static class Program
    {
        static void Main(string[] args)
        {
            ConsoleLogic.Welcome();       
            GamePlayLogic.StartGame();
        }
    }
}