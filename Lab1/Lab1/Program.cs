
var obj = new object();
var rnd = new Random();

const int numPhil = 5;
const int numFork = 5;

var Phils = new List<Philosopher>();
var Forks = new List<Fork>();

for (int i = 0; i < numFork; i++)
    Forks.Add(new Fork(i));
for (int i = 0; i < numPhil; i++)
    Phils.Add(new Philosopher(i, Forks.Where(x => x.Id == i || x.Id == (i + 1) % numFork).ToList(), rnd));

// Начало трапезы
foreach (var phil in Phils)
{
    var thread = new Thread(() => phil.Eating(obj));
    thread.Start();
}

public class Philosopher
{
    private int Id;
    private int TimeForEating;
    private bool WellFed;
    private List<Fork> Forks = new List<Fork>();

    // rnd необходим для случайной генерации
    public Philosopher(int id, List<Fork> forks, Random rnd)
    {
        this.Id = id;
        this.TimeForEating = rnd.Next(2000, 5000);
        this.WellFed = false;
        this.Forks = forks;
    }

    public void Eating(object obj)
    {
        while (!WellFed)
        {
            lock (obj)
            {
                if (Forks.Any(x => x.InUsage))
                    continue;
            }

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
    private int _id;
    private bool _inUsage;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    public bool InUsage
    {
        get { return _inUsage; }
        set { _inUsage = value; }
    }

    public Fork(int id)
    {
        this.Id = id;
        this.InUsage = false;
    }
}