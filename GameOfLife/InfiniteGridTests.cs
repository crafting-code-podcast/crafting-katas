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
    
    [Test]
    public void When_converting_a_grid_to_string()
    {
        var grid = new InfiniteGrid();
        grid.SetLiveCell(0, 0);
        grid.SetLiveCell(1, 2);
        grid.SetLiveCell(1, 0);
        grid.SetLiveCell(2, 1);

        var result = grid.ToString();
        
        Assert.That(result, Is.EqualTo("XX.\n..X\n.X.\n"));
    }

    [Test]
    public void When_creating_a_grid_from_a_string()
    {
        var grid = new InfiniteGrid("XX.\n..X\n.X.\n");
        
        Assert.That(grid.IsAliveAt(0, 0), Is.True);
        Assert.That(grid.IsAliveAt(1, 0), Is.True);
        Assert.That(grid.IsAliveAt(2, 0), Is.False);
        Assert.That(grid.IsAliveAt(0, 1), Is.False);
        Assert.That(grid.IsAliveAt(1, 1), Is.False);
        Assert.That(grid.IsAliveAt(2, 1), Is.True);
        Assert.That(grid.IsAliveAt(0, 2), Is.False);
        Assert.That(grid.IsAliveAt(1, 2), Is.True);
        Assert.That(grid.IsAliveAt(2, 2), Is.False);
    }
    
    [TestCase("...\n.X.\n...\n")]
    [TestCase("X..\n.X.\n...\n")]
    [TestCase("...\nXX.\n...\n")]
    [TestCase("...\n.X.\n..X\n")]
    public void When_a_live_cell_has_less_than_two_neighbors_it_dies(string input)
    {
        var grid = new InfiniteGrid(input);

        var result = grid.NextGeneration();

        Assert.That(result.IsAliveAt(1, 1), Is.False);
    }
    
    [TestCase("X.X\n.X.\n...\n")]
    [TestCase("X..\nXX.\n...\n")]
    [TestCase(".X.\n.X.\n..X\n")]
    [TestCase(".X.\n.XX\n..X\n")]
    public void When_a_live_cell_has_two_or_three_neighbors_it_lives(string input)
    {
        var grid = new InfiniteGrid(input);
        
        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(1, 1), Is.True);
    }
    
    [TestCase("XX.\n.XX\n..X\n")]
    [TestCase("XX.\nXX.\nX..\n")]
    public void When_a_live_cell_has_more_than_three_neighbors_it_dies(string input)
    {
        var grid = new InfiniteGrid(input);
        
        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(1, 1), Is.False);
    }
    
    [TestCase("X..\nX..\nX..\n")]
    [TestCase("..X\nX..\n..X\n")]
    [TestCase(".X.\nX..\n..X\n")]
    public void When_a_dead_cell_has_exactly_3_live_neighbors_it_lives(string input)
    {
        var grid = new InfiniteGrid(input);
        
        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(1, 1), Is.True);
    }
    
    [TestCase("...\n...\n...\n")]
    [TestCase("..X\n...\n...\n")]
    [TestCase("..X\n...\n..X\n")]
    [TestCase("XX.\nX..\n..X\n")]
    [TestCase("...\n...\n...\n")]
    [TestCase("XX.\nX..\nXX.\n")]
    [TestCase("XXX\nX..\nXX.\n")]
    [TestCase("XXX\nX.X\nXX.\n")]
    [TestCase("XXX\nX.X\nXXX\n")]
    public void When_a_dead_cell_has_other_than_3_neighbors_it_stays_dead(string input)
    {
        var grid = new InfiniteGrid(input);
        
        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(1, 1), Is.False);
    }
}