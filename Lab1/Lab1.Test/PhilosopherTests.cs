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

        
    }
}