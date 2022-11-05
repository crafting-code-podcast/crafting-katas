using Moq;
using NUnit.Framework;

namespace GameOfLife;

public class WorldTests
{
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

    [Test]
    public void Tick_returns_a_new_World()
    {
        var world = new World();
        var newWorld = world.Tick();

        Assert.That(newWorld, Is.Not.SameAs(world));
    }

    [Test]
    public void Tick_evaluates_rules_for_living_cells()
    {
        var fakeRules = new Mock<IRules>();
        var world = new World(fakeRules.Object);
        world.SetCellStatus(0, 0, Status.Alive);
        fakeRules.Setup(x => x.NextState(It.IsAny<Status>(), It.IsAny<int>())).Returns(Status.Alive);

        world.Tick();

        fakeRules.Verify(x => x.NextState(Status.Alive, 0));
        Assert.That(world.GetCellStatus(0, 0), Is.EqualTo(Status.Alive));
    }

    [Test]
    public void Tick_evaluates_rules_for_cells_around_living_cells()
    {
        var fakeRules = new Mock<IRules>();
        var world = new World(fakeRules.Object);
        world.SetCellStatus(0, 0, Status.Alive);
        fakeRules.Setup(x => x.NextState(It.IsAny<Status>(), It.IsAny<int>())).Returns(Status.Alive);

        world.Tick();

        fakeRules.Verify(x => x.NextState(Status.Dead, 1), Times.Exactly(8));
        fakeRules.Verify(x => x.NextState(Status.Alive, 0), Times.Exactly(1));
    }
}