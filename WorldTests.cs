using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace GameOfLife;

public class WorldTests
{
    [Test]
    public void Tick_returns_a_new_World()
    {
        var world = new World();
        var newWorld = world.Tick();

        Assert.That(newWorld, Is.Not.SameAs(world));
    }

    [Test]
    public void Adding_a_living_cell_increases_population()
    {
        var world = new World();
        Assert.That(world.Population, Is.EqualTo(0));

        world.SetCellStatus(0, 0, Status.Alive);

        Assert.That(world.Population, Is.EqualTo(1));
    }

    [Test]
    public void Adding_a_dead_cell_does_not_increase_population()
    {
        var world = new World();
        Assert.That(world.Population, Is.EqualTo(0));

        world.SetCellStatus(0, 0, Status.Dead);

        Assert.That(world.Population, Is.EqualTo(0));
    }

    [Test]
    public void Cell_default_state_is_Dead()
    {
        var world = new World();
        Assert.That(world.GetCellStatus(0, 0), Is.EqualTo(Status.Dead));
    }

    [Test]
    public void Cell_can_live()
    {
        var world = new World();

        world.SetCellStatus(0, 0, Status.Alive);

        Assert.That(world.GetCellStatus(0, 0), Is.EqualTo(Status.Alive));
    }
}

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