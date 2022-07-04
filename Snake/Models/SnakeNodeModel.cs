namespace Snake.Models
{
    public class SnakeNodeModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public SnakeNodeModel Head { get; set; }
        public bool FirstNode { get; set; } = false;

    }
}
