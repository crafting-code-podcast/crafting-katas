using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife;

public class InfiniteGrid
{
    private List<(int column, int row)> liveCells = new();

    public InfiniteGrid() { }

    public InfiniteGrid(string input)
    {
        var rowData = input.Trim().Split("\n");
        var rows = rowData.Length;
        var columns = rowData[0].Length;
        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                if (rowData[row][column] == 'X')
                {
                    SetLiveCell(column, row);
                }
            }
        }
    }
    
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

    public override string ToString()
    {
        var minColumn = liveCells.Min(x => x.column);
        var maxColumn = liveCells.Max(x => x.column);
        var minRow = liveCells.Min(x => x.row);
        var maxRow = liveCells.Max(x => x.row);
        var builder = new StringBuilder();

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var column = minColumn; column <= maxColumn; column++)
            {
                builder.Append(IsAliveAt(column, row) ? "X" : ".");
            }

            builder.Append("\n");
        }

        return builder.ToString();
    }

    public InfiniteGrid NextGeneration()
    {
        return new InfiniteGrid();
    }
}