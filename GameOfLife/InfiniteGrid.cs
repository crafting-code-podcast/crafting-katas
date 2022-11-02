using System.Collections.Generic;
using System.Linq;

namespace GameOfLife;

public class InfiniteGrid
{
    private List<(int column, int row)> liveCells = new();
    
    public bool IsAliveAt(int column, int row)
    {
        return liveCells.Any(c => c.column == column && c.row == row);
    }

    public void SetLiveCell(int column, int row)
    {
        if (!IsAliveAt(column, row))
        {
            liveCells.Add((column, row));            
        }
    }

    public int LiveCellCount => liveCells.Count;
}