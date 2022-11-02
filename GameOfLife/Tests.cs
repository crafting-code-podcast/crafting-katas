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
}