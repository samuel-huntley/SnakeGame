using System.Collections.Generic;

namespace Snake.Models
{
    public class GameGridModel
    {
        public List<CoordinateModel> GridSpots { get; set; } = new List<CoordinateModel>();
        public int GridHeight { get; set; } = 25;
        public int GridWidth { get; set; } = 40;
        public List<CoordinateModel> FoodLocation { get; set; } = new List<CoordinateModel>();
        public string[,] gridMatrix = null;
    }
}
