using Snake.Enums;
using System.Collections.Generic;

namespace Snake.Models
{
    public class SnakeModel
    {
        public int Speed { get; set; } = 200;
        public List<SnakeNodeModel> SnakeNodes { get; set; } = new List<SnakeNodeModel>();
        public Directions Direction { get; set; } = Directions.None;
        public int Score { get; set; }

    }
}
