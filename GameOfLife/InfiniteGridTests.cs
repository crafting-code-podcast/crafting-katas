using NUnit.Framework;

namespace GameOfLife;

public class InfiniteGridTests
{
    [Test]
    public void When_checking_if_cell_is_alive_and_it_is_not()
    {
        var grid = new InfiniteGrid();
        
        Assert.That(grid.IsAliveAt(0, 0), Is.False);
    }

    [Test]
    public void When_checking_if_cell_is_alive_and_it_is()
    {
        var grid = new InfiniteGrid();
        grid.SetLiveCell(0, 0);
        
        Assert.That(grid.IsAliveAt(0, 0), Is.True);
    }
    
    [Test]
    public void When_setting_the_same_cell_to_be_alive_twice()
    {
        var grid = new InfiniteGrid();
        grid.SetLiveCell(0, 0);
        grid.SetLiveCell(0, 0);
        
        Assert.That(grid.IsAliveAt(0, 0), Is.True);
        Assert.That(grid.LiveCellCount, Is.EqualTo(1));
    }
}