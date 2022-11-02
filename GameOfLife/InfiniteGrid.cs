using System;
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
        var next = new InfiniteGrid();
        liveCells.ForEach(cell =>
        {
            var liveNeighbors = LiveNeighborCount(cell);
            if (liveNeighbors == 2 || liveNeighbors == 3)
            {
                next.SetLiveCell(cell.column, cell.row);
            }
        });
        var deadNeighbors = liveCells.SelectMany(GetAllNeighbors)
            .Distinct()
            .Where(cell => !liveCells.Contains(cell))
            .ToList();
        deadNeighbors.ForEach(cell =>
        {
            var liveNeighbors = LiveNeighborCount(cell);
            if (liveNeighbors == 3)
            {
                next.SetLiveCell(cell.column, cell.row);
            }
        });
        return next;
    }

    private int LiveNeighborCount((int column, int row) cell) =>
        liveCells.Count(otherCell => IsNeighbor(cell, otherCell));

    private bool IsNeighbor((int column, int row) cell, (int column, int row) otherCell)
    {
        var columnDistance = Math.Abs(cell.column - otherCell.column);
        var rowDistance = Math.Abs(cell.row - otherCell.row);

        return columnDistance == 1 && rowDistance < 2 ||
               columnDistance < 2 && rowDistance == 1;
    }

    private List<(int column, int row)> GetAllNeighbors((int column, int row) cell)
    {
        return new List<(int, int)>
        {
            (cell.column - 1, cell.row - 1),
            (cell.column - 1, cell.row),
            (cell.column - 1, cell.row + 1),
            (cell.column, cell.row - 1),
            (cell.column, cell.row + 1),
            (cell.column + 1, cell.row - 1),
            (cell.column + 1, cell.row),
            (cell.column + 1, cell.row + 1),
        };
    }
}