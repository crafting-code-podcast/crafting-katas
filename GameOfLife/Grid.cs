using NUnit.Framework.Constraints;

namespace GameOfLife;

public class Grid
{
    private bool[,] cells;
    
    public Grid(int columns, int rows)
    {
        cells = new bool[columns, rows];
    }

    public bool IsAliveAt(int column, int row)
    {
        return cells[column, row];
    }

    public void SetLiveCell(int column, int row)
    {
        cells[column, row] = true;
    }

    public Grid NextGeneration()
    {
        var columns = cells.GetLength(0);
        var rows = cells.GetLength(1);
        return new Grid(columns, rows);
    }
}