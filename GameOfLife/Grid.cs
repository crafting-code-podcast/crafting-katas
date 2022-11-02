using System.Collections.Generic;
using System.Linq;
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
        if (column < 0 || column >= cells.GetLength(0))
        {
            return false;
        }

        if (row < 0 || row >= cells.GetLength(1))
        {
            return false;
        }
        
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
        var next = new Grid(columns, rows);
        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                next.cells[column, row] = WillLiveAt(column, row);
            }
        }

        return next;
    }

    private bool WillLiveAt(int column, int row)
    {
        var liveNeighbors = CountLiveNeighbors(column, row);
        if (IsAliveAt(column, row) && (liveNeighbors == 2 || liveNeighbors == 3))
        {
            return true;
        }
        return false;
    }

    private int CountLiveNeighbors(int column, int row)
    {
        return new List<(int column, int row)>
        {
            (column - 1, row - 1),
            (column - 1, row),
            (column - 1, row + 1),
            (column, row - 1),
            (column, row + 1),
            (column + 1, row - 1),
            (column + 1, row),
            (column + 1, row + 1),
        }
        .Select(x => IsAliveAt(x.column, x.row) ? 1 : 0)
        .Sum();
    }
}