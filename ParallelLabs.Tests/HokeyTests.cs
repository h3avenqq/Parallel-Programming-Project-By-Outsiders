using MPI;
using System.Diagnostics;
using System.Text;

namespace ParallelLabs.Tests
{
    public class HokeyTests
    {

        [Fact]
        public void HockeyPlayerCtor_Success()
        {
            // Arrange
            var player = new HockeyPlayer(1, 0);

            // Assert
            Assert.Equal(1, player.Id);
            Assert.Equal(0, player.TeamId);
            Assert.False(player.HasHockeyPuck);
        }

        [Fact]
        public void HockeyPlayerCtor_NegativePlayerId()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new HockeyPlayer(-1, 0));

            Assert.Equal("Id can't be negative.", exception.ParamName);
        }

        [Fact]
        public void HockeyPlayerPass_Success()
        {
            // Arrange
            var players = new List<HockeyPlayer>()
            {
                new HockeyPlayer(0, 1) { HasHockeyPuck = true },
                new HockeyPlayer(1, 1),
                new HockeyPlayer(2, 1),
                new HockeyPlayer(3, 1),
                new HockeyPlayer(4, 1),
                new HockeyPlayer(5, 1),
                new HockeyPlayer(6, 0),
                new HockeyPlayer(7, 0),
                new HockeyPlayer(8, 0),
                new HockeyPlayer(9, 0),
                new HockeyPlayer(10, 0),
                new HockeyPlayer(11, 0),
            };

            // Act
            int receiverId = players[0].Pass(players);

            // Assert
            Assert.False(players[0].HasHockeyPuck, "Player 0 should not have the puck after passing.");
            Assert.True(players[receiverId].HasHockeyPuck, $"Player {receiverId} should have received the puck.");
        }

        [Fact]
        public void HockeyPlayerPass_NotEnogthPlayersException()
        {
            // Arrange
            var players = new List<HockeyPlayer>()
            {
                new HockeyPlayer(0, 0) { HasHockeyPuck = true }
            };

            // Act
            var exception = Assert.Throws<Exception>(() => players[0].Pass(players));

            Assert.Equal("Not enough players", exception.Message);
        }

        [Fact]
        public void HockeyPlayerPass_WithoutHockeyPuck()
        {
            // Arrange
            var players = new List<HockeyPlayer>()
            {
                new HockeyPlayer(0, 1) { HasHockeyPuck = true },
                new HockeyPlayer(1, 1),
                new HockeyPlayer(2, 1),
                new HockeyPlayer(3, 1),
                new HockeyPlayer(4, 1),
                new HockeyPlayer(5, 1),
                new HockeyPlayer(6, 0),
                new HockeyPlayer(7, 0),
                new HockeyPlayer(8, 0),
                new HockeyPlayer(9, 0),
                new HockeyPlayer(10, 0),
                new HockeyPlayer(11, 0),
            };

            // Act
            var exception = Assert.Throws<Exception>(() => players[1].Pass(players));

            Assert.Equal("Player #1 (Team #1) cannot pass", exception.Message);
        }

        [Fact]
        public void TestMPI_Success()
        {
            Process process = new Process();
            process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            process.StartInfo.FileName = "mpiexec";
            process.StartInfo.Arguments = "-n 12 ParallelLabs.Lab3.exe";

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            string message = string.Empty;

            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    message = e.Data;
                }
            };

            process.EnableRaisingEvents = true;

            process.Exited += (sender, e) =>
            {
               // Assert.Equal("Number of passes - 2000", error);
            };

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();
            Assert.Equal("Number of passes - 2000", message);
        }
    }
}