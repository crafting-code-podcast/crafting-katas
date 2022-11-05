using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace GameOfLife;

public class RulesTests
{
    [TestCase(0, Status.Dead)]
    [TestCase(1, Status.Dead)]
    [TestCase(2, Status.Alive)]
    [TestCase(3, Status.Alive)]
    [TestCase(4, Status.Dead)]
    [TestCase(5, Status.Dead)]
    [TestCase(6, Status.Dead)]
    [TestCase(7, Status.Dead)]
    [TestCase(8, Status.Dead)]
    public void Living_cell_next_status_tests(int livingNeighbors, Status expected)
    {
        Assert.That(new Rules().NextState(Status.Alive, livingNeighbors), Is.EqualTo(expected));
    }
}

public class Rules
{
    public Status NextState(Status currentStatus, int livingNeighbors)
    {
        if (livingNeighbors == 2 || livingNeighbors == 3)
        {
            return Status.Alive;
        }
        return Status.Dead;
    }
}

public enum Status
{
    Alive,
    Dead
}