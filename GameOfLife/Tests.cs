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
}