using NUnit.Framework;

namespace GameOfLife;

public class AcceptanceTests
{
    [Test]
    public void Square_still_life()
    {
        var world = new World();
        world.SetCellStatus(0,0,Status.Alive);
        world.SetCellStatus(0,1,Status.Alive);
        world.SetCellStatus(1,0,Status.Alive);
        world.SetCellStatus(1,1,Status.Alive);

        var tick1 = world.Tick();
        Assert.That(tick1.Population, Is.EqualTo(4));
        Assert.That(tick1.GetCellStatus(0,0), Is.EqualTo(Status.Alive));
        Assert.That(tick1.GetCellStatus(0,1), Is.EqualTo(Status.Alive));
        Assert.That(tick1.GetCellStatus(1,0), Is.EqualTo(Status.Alive));
        Assert.That(tick1.GetCellStatus(1,1), Is.EqualTo(Status.Alive));

        var tick2 = tick1.Tick();
        Assert.That(tick2.Population, Is.EqualTo(4));
        Assert.That(tick2.GetCellStatus(0,0), Is.EqualTo(Status.Alive));
        Assert.That(tick2.GetCellStatus(0,1), Is.EqualTo(Status.Alive));
        Assert.That(tick2.GetCellStatus(1,0), Is.EqualTo(Status.Alive));
        Assert.That(tick2.GetCellStatus(1,1), Is.EqualTo(Status.Alive));
    }
}