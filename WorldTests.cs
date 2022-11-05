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
    
    
}

public class World
{
    public World Tick()
    {
        return new World();
    }
}