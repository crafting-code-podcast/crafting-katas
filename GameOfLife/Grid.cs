using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife;

public class Grid
{
    private readonly int columns;
    private readonly int rows;
    private readonly bool[,] cells;
    
    public Grid(int columns, int rows)
    {
        this.columns = columns;
        this.rows = rows;
        cells = new bool[columns, rows];
    }

    public Grid(string input)
    {
        var rowData = input.Trim().Split("\n");
        rows = rowData.Length;
        columns = rowData[0].Length;
        cells = new bool[columns, rows];
        ForEveryCell((column, row) => cells[column, row] = rowData[row][column] == 'X');
    }

    private void ForEveryCellAndRow(Action<int, int> cellAction, Action endOfRowAction)
    {
        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                cellAction(column, row);
            }

            endOfRowAction();
        }
    }

    private void ForEveryCell(Action<int, int> action) => ForEveryCellAndRow(action, () => { });

    public bool IsAliveAt(int column, int row)
    {
        if (column < 0 || column >= columns || row < 0 || row >= rows)
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
        var next = new Grid(columns, rows);
        ForEveryCell((column, row) => next.cells[column, row] = WillLiveAt(column, row));
        return next;
    }

    private bool WillLiveAt(int column, int row)
    {
        var liveNeighbors = CountLiveNeighbors(column, row);
        if (liveNeighbors == 3)
        {
            return true;
        }
        
        if (IsAliveAt(column, row) && liveNeighbors == 2)
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

    public override string ToString()
    {
        var builder = new StringBuilder((columns + 1) * rows);
        ForEveryCellAndRow((column, row) => builder.Append(IsAliveAt(column, row) ? "X" : "."), () => builder.Append("\n"));
        return builder.ToString();
    }
}