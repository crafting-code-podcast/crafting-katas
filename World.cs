using System.Collections.Generic;
using System.Linq;

namespace GameOfLife;

public class World
{
    private readonly List<(int row, int column)> livingCells = new();

    public World Tick()
    {
        return new World();
    }

    public int Population => livingCells.Count;

    public void SetCellStatus(int row, int column, Status status)
    {
        livingCells.Remove((row, column));
        if (status == Status.Alive) livingCells.Add((row, column));
    }

    public Status GetCellStatus(int row, int column)
    {
        return livingCells.Any(x => x.row == row && x.column == column) ? Status.Alive : Status.Dead;
    }
}