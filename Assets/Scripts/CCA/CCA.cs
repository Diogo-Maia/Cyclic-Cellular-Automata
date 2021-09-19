public class CCA
{
    private System.Random rdn;
    private int[,] nextGrid;
    private int width;
    private int height;
    private int states;
    private int threshold;
    private int range;
    private bool moore;
    private bool toroidal;

    public CCA(int width, int height, int states, int threshold, int range, bool moore, bool toriodal)
    {
        this.width = width;
        this.height = height;
        this.states = states;
        this.threshold = threshold;
        this.moore = moore;
        this.range = range;
        this.toroidal = toriodal;
    }

    public int[,] Next(int[,] grid)
    {
        int myState = 0;
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++)
            {
                if(CheckNeighboors(x, y, grid) >= threshold)
                {
                    nextGrid = grid;
                    myState = nextGrid[x, y];
                    nextGrid[x, y] = myState + 1 > states ? 0 : myState + 1;

                }                                
            }
        }

        return nextGrid;
    }

    public int[,] Populate(int[,] grid)
    { 
        rdn = new System.Random();
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++)
            {
                grid[x, y] = rdn.Next(0, states);
            }
        }
        return grid;
    }

    private int CheckNeighboors(int x, int y, int[,] grid)
    {
        int myState = (int)grid[x, y];
        int nextS = myState + 1 > 2 ? 0 : myState + 1;
        
        int numOfNext = 0;
        for(int i = 1; i < range + 1; i++)
        {
            if(!toroidal)
            {
                if(x - 1 - i >= 0) if(grid[x - 1 - i, y] == nextS) numOfNext++;
                if(x + 1 + i < width - 1) if(grid[x + 1 + i, y] == nextS) numOfNext++;
                if(y - 1 - i >= 0) if(grid[x, y - 1 - i] == nextS) numOfNext++;
                if(y + 1 + i < height - 1) if(grid[x, y + 1 + i] == nextS) numOfNext++;

                if(moore)
                {
                    if(x - 1 - i >= 0 && y - 1 - i >= 0) if(grid[x - 1 - i, y - 1 - i] == nextS) numOfNext++;
                    if(x - 1 - i >= 0 && y + 1 + i < height - 1) if(grid[x - 1 - i, y + 1 + i] == nextS) numOfNext++;
                    if(x + 1 + i < width - 1 && y - 1 - i >= 0) if(grid[x + 1 + i, y - 1 - i] == nextS) numOfNext++;
                    if(x + 1 + i < width - 1 && y + 1 + i < height - 1) if(grid[x + 1 + i, y + 1 + i] == nextS) numOfNext++;
                } 
                if(i >= 2 && !moore)
                {                
                    int q = i - 1;

                    if(x - 1 - q >= 0 && y - 1 - q >= 0) if(grid[x - 1 - q, y - 1 - q] == nextS) numOfNext++;
                    if(x - 1 - q >= 0 && y + 1 + q < height - 1) if(grid[x - 1 - q, y + 1 + q] == nextS) numOfNext++;
                    if(x + 1 + q < width - 1 && y - 1 - q >= 0) if(grid[x + 1 + q, y - 1 - q] == nextS) numOfNext++;
                    if(x + 1 + q < width - 1 && y + 1 + q < height - 1) if(grid[x + 1 + q, y + 1 + q] == nextS) numOfNext++;
                }
            }else
            {
                if(grid[x + 1 + i >= width ? 0 + (i - 1): x + 1 + i, y] == nextS) numOfNext++;
                if(grid[x - 1 - i < 0 ? width - 1 - (i - 1): x - 1 - i, y] == nextS) numOfNext++;
                if(grid[x, y - 1 - i < 0 ? height - 1 - (i - 1): y - 1 - i] == nextS) numOfNext++;
                if(grid[x, y + 1 + i >= height ? 0 + (i - 1): y + 1 + i] == nextS) numOfNext++;

                if(moore)
                {
                    if(grid[x + 1 + i >= width ? 0 + (i - 1): x + 1 + i,
                        y + 1 + i >= height ? 0 + (i - 1): y + 1 + i] == nextS) numOfNext++;
                    if(grid[x + 1 + i >= width ? 0 + (i - 1): x + 1 + i, 
                        y - 1 - i < 0 ? height - 1 - (i - 1): y - 1 - i] == nextS) numOfNext++;
                    if(grid[x - 1 - i < 0 ? width - 1 - (i - 1): x - 1 - i, 
                        y + 1 + i >= height ? 0 + (i - 1): y + 1 + i] == nextS) numOfNext++;
                    if(grid[x - 1 - i < 0 ? width - 1 - (i - 1): x - 1 - i, 
                        y - 1 - i < 0 ? height - 1 - (i - 1): y - 1 - i] == nextS) numOfNext++;
                }
                if(i >= 2 && !moore)
                {
                    int q = i - 1;

                    if(grid[x + 1 + q >= width ? 0 : x + 1 + q, 
                        y + 1 + q >= height ? 0 : y + 1 + q] == nextS) numOfNext++;
                    if(grid[x + 1 + q >= width ? 0 : x + 1 + q, 
                        y - 1 - q < 0 ? height - 1 : y - 1 - q] == nextS) numOfNext++;
                    if(grid[x - 1 - q < 0 ? width - 1 : x - 1 - q, 
                        y + 1 + q >= height ? 0 : y + 1 + q] == nextS) numOfNext++;
                    if(grid[x - 1 - q < 0 ? width - 1 : x - 1 - q, 
                        y - 1 - q < 0 ? height - 1 : y - 1 - q] == nextS) numOfNext++;
                }
            }
        }

        return numOfNext;
    }
}
