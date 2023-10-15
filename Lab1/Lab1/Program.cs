public class Philosopher
{
    public int Id;
    public int TimeForEating;
    public bool WellFed;
    public List<Fork> Forks = new List<Fork>();

    // rnd необходим для случайной генерации
    public Philosopher(int id, List<Fork> forks, Random rnd)
    {
        this.Id = id;
        this.TimeForEating = rnd.Next(2000, 5000);
        this.WellFed = false;
        this.Forks = forks;
    }

    public void Eating()
    {
        while (!WellFed)
        {
            if (Forks.Any(x => x.InUsage))
                continue;

            foreach(var fork in Forks)
                fork.InUsage = true;

            Thread.Sleep(TimeForEating);
            WellFed = true;
            Console.WriteLine($"Философ {Id} - поел");
            foreach(var fork in Forks)
                fork.InUsage = false;
        }
    }
}

public class Fork
{
    public int Id;
    public bool InUsage;

    public Fork(int id)
    {
        this.Id = id;
        this.InUsage = false;
    }
}