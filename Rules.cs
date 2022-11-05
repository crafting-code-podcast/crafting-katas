namespace GameOfLife;

public class Rules
{
    public Status NextState(Status currentStatus, int livingNeighbors)
    {
        if (livingNeighbors == 3)
        {
            return Status.Alive;
        }
        if (currentStatus == Status.Alive && livingNeighbors == 2)
        {
            return Status.Alive;
        }
        return Status.Dead;
    }
}