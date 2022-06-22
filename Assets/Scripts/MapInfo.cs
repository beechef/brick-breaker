public class MapInfo
{
    private static readonly int MAXRow = 10;
    private static readonly int MAXColumn = 17;
    public int level;
    public float ballSpeed;
    public int lives;
    public byte[,] blocks = new byte[MAXRow,MAXColumn]; 
}