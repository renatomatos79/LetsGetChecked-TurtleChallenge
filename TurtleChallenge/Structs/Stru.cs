namespace TurtleChallenge.Structs
{
    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public override int GetHashCode()
        {
            return $"{X}{Y}".GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj != null && obj is Position && this.GetHashCode() == obj.GetHashCode();
        }
    }
}
