using System.Linq;
using NUnit.Framework;

namespace GameOfLife;

public class Tests
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
    public void When_setting_live_cells_from_coordinates()
    {
        var grid = new Grid(3, 3);
        var coordinates = "0,0;2,2;0,2;1,2";
        
        SetLiveCellsFromStringCoordinates(grid, coordinates);
        
        Assert.That(grid.IsAliveAt(0, 0), Is.True);
        Assert.That(grid.IsAliveAt(0, 1), Is.False);
        Assert.That(grid.IsAliveAt(0, 2), Is.True);
        Assert.That(grid.IsAliveAt(1, 0), Is.False);
        Assert.That(grid.IsAliveAt(1, 1), Is.False);
        Assert.That(grid.IsAliveAt(1, 2), Is.True);
        Assert.That(grid.IsAliveAt(2, 0), Is.False);
        Assert.That(grid.IsAliveAt(2, 1), Is.False);
        Assert.That(grid.IsAliveAt(2, 2), Is.True);
    }

    private void SetLiveCellsFromStringCoordinates(Grid grid, string coordinates)
    {
        if (string.IsNullOrEmpty(coordinates))
        {
            return;
        }
        
        coordinates.Split(";")
            .Select(x => x.Split(",").Select(x => int.Parse(x)).ToList())
            .ToList()
            .ForEach(cell => grid.SetLiveCell(cell[0], cell[1]));
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

    [TestCase("")]
    [TestCase("0,0")]
    [TestCase("0,1")]
    [TestCase("2,2")]
    public void When_a_live_cell_has_less_than_two_neighbors_it_dies(string neighbors)
    {
        var grid = new Grid(3, 3);
        grid.SetLiveCell(1, 1);
        SetLiveCellsFromStringCoordinates(grid, neighbors);

        var result = grid.NextGeneration();

        Assert.That(result.IsAliveAt(1, 1), Is.False);
    }

    [TestCase("0,0;0,2")]
    [TestCase("0,0;0,1")]
    [TestCase("1,0;2,2")]
    [TestCase("1,0;2,2;2,1")]
    public void When_a_live_cell_has_two_or_three_neighbors_it_lives(string neighbors)
    {
        var grid = new Grid(3, 3);
        grid.SetLiveCell(1, 1);
        SetLiveCellsFromStringCoordinates(grid, neighbors);

        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(1, 1), Is.True);
    }
    
    [TestCase("1,0;2,2;2,1;0,0")]
    [TestCase("0,0;0,1;0,2;1,0")]
    public void When_a_live_cell_has_more_than_three_neighbors_it_dies(string neighbors)
    {
        var grid = new Grid(3, 3);
        grid.SetLiveCell(1, 1);
        SetLiveCellsFromStringCoordinates(grid, neighbors);

        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(1, 1), Is.False);
    }

    [TestCase("0,0;0,1;0,2")]
    [TestCase("2,0;0,1;2,2")]
    [TestCase("1,0;0,1;2,2")]
    public void When_a_dead_cell_has_exactly_3_live_neighbors_it_lives(string neighbors)
    {
        var grid = new Grid(3, 3);
        SetLiveCellsFromStringCoordinates(grid, neighbors);

        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(1, 1), Is.True);
    }
    
    [TestCase("")]
    [TestCase("0,2")]
    [TestCase("2,0;2,2")]
    [TestCase("1,0;0,1;2,2;0,0")]
    [TestCase("0,0;0,1;0,2;1,0;1,2")]
    [TestCase("0,0;0,1;0,2;1,0;1,2;2,0")]
    [TestCase("0,0;0,1;0,2;1,0;1,2;2,0;2,1")]
    [TestCase("0,0;0,1;0,2;1,0;1,2;2,0;2,1;2,2")]
    public void When_a_dead_cell_has_other_than_3_neighbors_it_stays_dead(string neighbors)
    {
        var grid = new Grid(3, 3);
        SetLiveCellsFromStringCoordinates(grid, neighbors);

        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(1, 1), Is.False);
    }

    [Test]
    public void When_testing_a_block()
    {
        var grid = new Grid(4, 4);
        SetLiveCellsFromStringCoordinates(grid, "1,1;1,2;2,1;2,2");

        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(0,0), Is.False);
        Assert.That(result.IsAliveAt(0,1), Is.False);
        Assert.That(result.IsAliveAt(0,2), Is.False);
        Assert.That(result.IsAliveAt(0,3), Is.False);
        Assert.That(result.IsAliveAt(1,0), Is.False);
        Assert.That(result.IsAliveAt(1,1), Is.True);
        Assert.That(result.IsAliveAt(1,2), Is.True);
        Assert.That(result.IsAliveAt(1,3), Is.False);
        Assert.That(result.IsAliveAt(2,0), Is.False);
        Assert.That(result.IsAliveAt(2,1), Is.True);
        Assert.That(result.IsAliveAt(2,2), Is.True);
        Assert.That(result.IsAliveAt(2,3), Is.False);
        Assert.That(result.IsAliveAt(3,0), Is.False);
        Assert.That(result.IsAliveAt(3,1), Is.False);
        Assert.That(result.IsAliveAt(3,2), Is.False);
        Assert.That(result.IsAliveAt(3,3), Is.False);
    }

    [Test]
    public void When_testing_a_beehive()
    {
        var grid = new Grid(6, 5);
        SetLiveCellsFromStringCoordinates(grid, "2,1;3,1;1,2;4,2;2,3;3,3");
        
        var result = grid.NextGeneration();
        
        Assert.That(result.IsAliveAt(0,0), Is.False);
        Assert.That(result.IsAliveAt(1,0), Is.False);
        Assert.That(result.IsAliveAt(2,0), Is.False);
        Assert.That(result.IsAliveAt(3,0), Is.False);
        Assert.That(result.IsAliveAt(4,0), Is.False);
        Assert.That(result.IsAliveAt(5,0), Is.False);
        
        Assert.That(result.IsAliveAt(0,1), Is.False);
        Assert.That(result.IsAliveAt(1,1), Is.False);
        Assert.That(result.IsAliveAt(2,1), Is.True);
        Assert.That(result.IsAliveAt(3,1), Is.True);
        Assert.That(result.IsAliveAt(4,1), Is.False);
        Assert.That(result.IsAliveAt(5,1), Is.False);
        
        Assert.That(result.IsAliveAt(0,2), Is.False);
        Assert.That(result.IsAliveAt(1,2), Is.True);
        Assert.That(result.IsAliveAt(2,2), Is.False);
        Assert.That(result.IsAliveAt(3,2), Is.False);
        Assert.That(result.IsAliveAt(4,2), Is.True);
        Assert.That(result.IsAliveAt(5,2), Is.False);
        
        Assert.That(result.IsAliveAt(0,3), Is.False);
        Assert.That(result.IsAliveAt(1,3), Is.False);
        Assert.That(result.IsAliveAt(2,3), Is.True);
        Assert.That(result.IsAliveAt(3,3), Is.True);
        Assert.That(result.IsAliveAt(4,3), Is.False);
        Assert.That(result.IsAliveAt(5,3), Is.False);
        
        Assert.That(result.IsAliveAt(0,4), Is.False);
        Assert.That(result.IsAliveAt(1,4), Is.False);
        Assert.That(result.IsAliveAt(2,4), Is.False);
        Assert.That(result.IsAliveAt(3,4), Is.False);
        Assert.That(result.IsAliveAt(4,4), Is.False);
        Assert.That(result.IsAliveAt(5,4), Is.False);
    }
}