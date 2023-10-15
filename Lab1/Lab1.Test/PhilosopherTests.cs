using System;
using System.Collections.Generic;
using Xunit;

namespace Lab1.Test
{
    public class PhilosopherTests
    {

        [Fact]
        public void PhilosopherCtor_Success()
        {
            // Arrange
            int id = 1;
            var forks = new List<Fork>()
            {
                new Fork(1),
                new Fork(2),
                new Fork(3),
                new Fork(4)
            };
            bool wellFed = false;
            var rnd = new Random();

            // Act
            var phil = new Philosopher(id, forks, rnd);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(id, phil.Id);
                Assert.Equal(forks, phil.Forks);
                Assert.Equal(wellFed, phil.WellFed);
            });
        }

        [Fact]
        public void PhilosopherCtor_FailOnEmptyForksList()
        {
            // Arrange
            int id = 1;
            List<Fork> forks1 = null;
            List<Fork> forks2 = new();
            var rnd = new Random();

            // Act

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() => new Philosopher(id, forks1, rnd));
                Assert.Throws<ArgumentException>(() => new Philosopher(id, forks2, rnd));
            });
        }

        [Fact]

        public void PhilosopherEating_Test()
        {
            // Arrange
            int id = 2;
            var forks = new List<Fork>()
            {
                new Fork(2),
                new Fork(3)
            };
            bool wellFed = false;
            var rnd = new Random();
            var obj = new object();

            // Act
            var phil = new Philosopher(id, forks, rnd);
            phil.Eating(obj);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(!wellFed, phil.WellFed);
                Assert.Equal(forks, phil.Forks);
            });
        }

    }
}