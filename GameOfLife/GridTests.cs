using NUnit.Framework;

namespace GameOfLife;

public class GridTests
{
    [Test]
    public void When_creating_a_fixed_size_grid_of_cells()
    {
        var columns = 5;
        var rows = 4;
        
        var grid = new Grid(columns, rows);

        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                Assert.That(grid.IsAliveAt(column, row), Is.False);
            }
        }
    }

    [TestCase(0, 0)]
    [TestCase(1, 2)]
    [TestCase(2, 0)]
    public void When_setting_a_live_cell(int column, int row)
    {
        var grid = new Grid(3, 3);
        grid.SetLiveCell(column, row);
        
        Assert.That(grid.IsAliveAt(column, row), Is.True);
    }
    
    [TestCase(-1, 0)]
    [TestCase(0, -1)]
    [TestCase(-1, -1)]
    [TestCase(3, 0)]
    [TestCase(0, 3)]
    [TestCase(3, 3)]
    public void When_checking_is_cell_alive_and_it_is_outside_the_grid_bounds(int column, int row)
    {
        var grid = new Grid(3, 3);
        Assert.That(grid.IsAliveAt(column, row), Is.False);
    }

    [Test]
    public void When_converting_a_grid_to_string()
    {
        var grid = new Grid(3, 3);
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
        var grid = new Grid("XX.\n..X\n.X.\n");
        
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
        var grid = new Grid(input);

        var result = grid.NextGeneration();

        Assert.That(result.IsAliveAt(1, 1), Is.False);
    }

    [TestCase("X.X\n.X.\n...\n")]
    [TestCase("X..\nXX.\n...\n")]
    [TestCase(".X.\n.X.\n..X\n")]
    [TestCase(".X.\n.XX\n..X\n")]
    public void When_a_live_cell_has_two_or_three_neighbors_it_lives(string input)
    {
        var grid = new Grid(input);
        
        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(1, 1), Is.True);
    }
    
    [TestCase("XX.\n.XX\n..X\n")]
    [TestCase("XX.\nXX.\nX..\n")]
    public void When_a_live_cell_has_more_than_three_neighbors_it_dies(string input)
    {
        var grid = new Grid(input);
        
        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(1, 1), Is.False);
    }

    [TestCase("X..\nX..\nX..\n")]
    [TestCase("..X\nX..\n..X\n")]
    [TestCase(".X.\nX..\n..X\n")]
    public void When_a_dead_cell_has_exactly_3_live_neighbors_it_lives(string input)
    {
        var grid = new Grid(input);
        
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
        var grid = new Grid(input);
        
        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(1, 1), Is.False);
    }

    [Test]
    public void When_testing_a_block()
    {
        var blockInput = "....\n.XX.\n.XX.\n....\n";

        var result = new Grid(blockInput).NextGeneration().ToString();
        
        Assert.That(result, Is.EqualTo(blockInput));
    }

    [Test]
    public void When_testing_a_beehive()
    {
        var beehiveInput = "......\n..XX..\n.X..X.\n..XX..\n......\n";

        var result = new Grid(beehiveInput).NextGeneration().ToString();
        
        Assert.That(result, Is.EqualTo(beehiveInput));
    }
    
    [Test]
    public void When_testing_a_boat()
    {
        var boatInput = ".....\n.XX..\n.X.X.\n..X..\n.....\n";

        var result = new Grid(boatInput).NextGeneration().ToString();
        
        Assert.That(result, Is.EqualTo(boatInput));
    }
    
    [Test]
    public void When_testing_a_blinker()
    {
        var blinkerInput = ".....\n.....\n.XXX.\n.....\n.....\n";
        var grid = new Grid(blinkerInput);

        var result1 = grid.NextGeneration().ToString();
        var result2 = grid.NextGeneration().NextGeneration().ToString();
        
        Assert.That(result1, Is.EqualTo(".....\n..X..\n..X..\n..X..\n.....\n"));
        Assert.That(result2, Is.EqualTo(blinkerInput));
    }
    
    [Test]
    public void When_testing_a_toad()
    {
        var toadInput = "......\n......\n..XXX.\n.XXX..\n......\n......\n";
        var grid = new Grid(toadInput);

        var result1 = grid.NextGeneration().ToString();
        var result2 = grid.NextGeneration().NextGeneration().ToString();
        
        Assert.That(result1, Is.EqualTo("......\n...X..\n.X..X.\n.X..X.\n..X...\n......\n"));
        Assert.That(result2, Is.EqualTo(toadInput));
    }
    
    [Test]
    public void When_testing_a_glider()
    {
        var grid = new Grid(".X...\n..X..\nXXX..\n.....\n.....\n.....\n");

        grid = grid.NextGeneration();
        Assert.That(grid.ToString(), Is.EqualTo(".....\nX.X..\n.XX..\n.X...\n.....\n.....\n"));

        grid = grid.NextGeneration();
        Assert.That(grid.ToString(), Is.EqualTo(".....\n..X..\nX.X..\n.XX..\n.....\n.....\n"));
        
        grid = grid.NextGeneration();
        Assert.That(grid.ToString(), Is.EqualTo(".....\n.X...\n..XX.\n.XX..\n.....\n.....\n"));
        
        grid = grid.NextGeneration();
        Assert.That(grid.ToString(), Is.EqualTo(".....\n..X..\n...X.\n.XXX.\n.....\n.....\n"));
        
        grid = grid.NextGeneration();
        Assert.That(grid.ToString(), Is.EqualTo(".....\n.....\n.X.X.\n..XX.\n..X..\n.....\n"));
    }
}