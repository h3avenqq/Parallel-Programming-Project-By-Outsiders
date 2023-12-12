using MPI;

using var mpi = new MPI.Environment(ref args);

var rnd = new Random();
int rank = Communicator.world.Rank;
int size = Communicator.world.Size;

if (size != 12)
    throw new Exception("There are not 12 players on the field");

var players = new List<HockeyPlayer>();

for(int i = 0; i < size; i++)
{
    players.Add(new HockeyPlayer(i, i % 2));
}

int leadPlayer;

if (rank == 0)
{
    leadPlayer = rnd.Next(0, players.Count);
    players[leadPlayer].HasHockeyPuck = true;


    for (int i = 0; i < size; i++)
    {
        if (i == rank)
            continue;

        Communicator.world.Send(leadPlayer, i, 10);
    }


}
else
{
    leadPlayer = Communicator.world.Receive<int>(0, 10);
    players[leadPlayer].HasHockeyPuck = true;
}


if (rank == 0)
    Console.WriteLine($"Player #{leadPlayer} (Team #{players[leadPlayer].TeamId}) starts with hockey puck.");

int scoreA = 0;
int scoreB = 0;
int passCounter = 0;
int goodPassCounter = 0;

while(passCounter < 2000)
{
    int prevPlayer;
    if (rank == leadPlayer)
    {
        prevPlayer = rank;
        leadPlayer = players[rank].Pass(players);
        passCounter++;

        Console.WriteLine($"Player #{rank} (Team #{players[rank].TeamId}) pass to Player #{leadPlayer} (Team #{players[leadPlayer].TeamId})");

        if (prevPlayer == leadPlayer)
        {
            Console.WriteLine("ABORT!");
            System.Environment.Exit(0);
        }

        if (players[prevPlayer].TeamId == players[leadPlayer].TeamId)
            goodPassCounter++;
        else
            goodPassCounter = 0;

        if (goodPassCounter >= 4)
        {
            Console.WriteLine($"The player #{leadPlayer} (Team #{players[leadPlayer].TeamId}) scored in the goal!!!");

            int teamId = players[leadPlayer].TeamId;

            if (teamId == 0)
                scoreA++;
            else
                scoreB++;

            goodPassCounter = 0;

            players[leadPlayer].HasHockeyPuck = false;

            while (players[leadPlayer].TeamId == teamId)
            {
                leadPlayer = rnd.Next(0, players.Count - 1);
            }

            Console.WriteLine($"The player #{players[leadPlayer].Id} (Team {players[leadPlayer].TeamId}) now has the puck");

        }


        for (int i = 0; i < size; i++)
        {
            if (i == rank)
                continue;

            Communicator.world.Send(leadPlayer, i, 0);
            Communicator.world.Send(scoreA, i, 1);
            Communicator.world.Send(scoreB, i, 2);
            Communicator.world.Send(passCounter, i, 3);
            Communicator.world.Send(prevPlayer, i, 4);
            Communicator.world.Send(goodPassCounter, i, 5);
        }

    }
    else
    {
        prevPlayer = Communicator.world.Receive<int>(leadPlayer, 4);
        leadPlayer = Communicator.world.Receive<int>(prevPlayer, 0);
        scoreA = Communicator.world.Receive<int>(prevPlayer, 1);
        scoreB = Communicator.world.Receive<int>(prevPlayer, 2);
        passCounter = Communicator.world.Receive<int>(prevPlayer, 3);
        goodPassCounter = Communicator.world.Receive<int>(prevPlayer, 5);

        if (rank == leadPlayer)
            players[rank].HasHockeyPuck = true;

        players[prevPlayer].HasHockeyPuck = false;
    }

    Communicator.world.Barrier();

    if (rank == leadPlayer)
    {
        Console.WriteLine($"Score {scoreA}:{scoreB}");
        Console.WriteLine($"Number of passes - {passCounter}");
    }
    
}


public class HockeyPlayer
{
    public int Id { 
        get { return Id; }
        set 
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Id can't be negative.");

            Id = value;
        }
    }
    public int TeamId { get; set; }
    public bool HasHockeyPuck { get; set; }

    public HockeyPlayer(int id, int teamId)
    {
        if (id < 0)
            throw new ArgumentOutOfRangeException("Id can't be negative.");

        Id = id;
        TeamId = teamId;
        HasHockeyPuck = false;
    }

    public int Pass(List<HockeyPlayer> players)
    {
        var rnd = new Random();

        if (players.Count < 2)
            throw new Exception("Not enough players");

        if (!HasHockeyPuck)
            throw new Exception($"Player #{Id} (Team #{TeamId}) cannot pass");

        var chance = rnd.Next(0, 100);
        int teamId;

        if (chance <= 65)
        {
            teamId = TeamId;
        }
        else
        {
            teamId = (TeamId + 1) % 2;
        }

        int playerNumber;
        do
        {
            playerNumber = rnd.Next(0, players.Count);
        } while (players[playerNumber].TeamId != teamId || Id == playerNumber);

        HasHockeyPuck = false;
        players[playerNumber].HasHockeyPuck = true;

        return players[playerNumber].Id;
    }
}

