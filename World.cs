using System.Collections.Generic;
using System.Linq;

namespace GameOfLife;

public class World
{
    private readonly IRules rules;
    private readonly List<(int row, int column)> livingCells = new();

    public World() : this(new Rules())
    {
    }

    public World(IRules rules)
    {
        this.rules = rules;
    }

    public World Tick()
    {
        var world = new World();
        if (livingCells.Count == 0)
        {
            return world;
        }

        for (var row = livingCells.Min(x => x.row) - 1; row <= livingCells.Max(x => x.row) + 1; row++)
        {
            for (var column = livingCells.Min(x => x.column) - 1; column <= livingCells.Max(x => x.column) + 1; column++)
            {
                var cellStatus = GetCellStatus(row, column);
                var livingNeighbors = CountLivingNeighbors(row, column);
                world.SetCellStatus(row, column, rules.NextState(cellStatus, livingNeighbors));
            }
        }

        return world;
    }

    private int CountLivingNeighbors(int row, int column)
    {
        return new[]
        {
            GetCellStatus(row - 1, column - 1),
            GetCellStatus(row - 1, column),
            GetCellStatus(row - 1, column + 1),
            GetCellStatus(row, column - 1),
            GetCellStatus(row, column + 1),
            GetCellStatus(row + 1, column - 1),
            GetCellStatus(row + 1, column),
            GetCellStatus(row + 1, column + 1),
        }.Count(x => x == Status.Alive);
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