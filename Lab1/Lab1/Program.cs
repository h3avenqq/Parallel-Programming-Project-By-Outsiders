
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
    private int _id;
    private int _timeForEating;
    private bool _wellFed;
    private List<Fork> _forks = new List<Fork>();

    public int Id { 
        get { return _id; }
        set { _id = value; }
    }
    public int TimeForEating
    {
        get { return _timeForEating; }
        set { _timeForEating = value; }
    }
    public bool WellFed
    {
        get { return _wellFed; }
        set { _wellFed = value; }
    }
    public List<Fork> Forks
    {
        get { return _forks; }
        set { _forks = value; }
    }

    // rnd необходим для случайной генерации
    public Philosopher(int id, List<Fork> forks, Random rnd)
    {
        if (forks == null || !forks.Any())
            throw new ArgumentException("Error: массив вилок пуст либо количество не равно 0");

        this.Id = id;
        this.TimeForEating = rnd.Next(2000, 5000);
        this.WellFed = false;
        this.Forks = forks;
    }

    public void Eating(object obj)
    {
        while (!_wellFed)
        {
            lock (obj)
            {
                if (Forks.Any(x => x.InUsage))
                    continue;

                foreach (var fork in _forks)
                    fork.InUsage = true;
            }

            Thread.Sleep(_timeForEating);
            _wellFed = true;
            Console.WriteLine($"Философ {_id} - поел");
            lock (obj)
            {
                foreach (var fork in _forks)
                    fork.InUsage = false;
            }
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