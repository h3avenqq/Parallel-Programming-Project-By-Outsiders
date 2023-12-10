namespace ParallelLabs.Tests
{
    public class IntegralTests
    {
        private const double EPS = 0.01;

        private double F1(double x) 
            => x * x;
        private double F2(double x)
            => 5 * x;
        private double F3(double x)
            => Math.Sin(x);
        private double F4(double x)
            => Math.Cos(x);
        private double F5(double x)
            => Math.Sqrt(x * x - 2 * x + 7);
        private double F6(double x)
            => (x * x) * Math.Sqrt(4 - (x * x));
        private double F7(double x)
            => Math.Pow(Math.Sin(x), 2) + Math.Pow(Math.Cos(x), 2) + Math.Sin(2 * x);
        private double F8(double x)
            => 1d / x;
        private double F9(double x)
            => x * x * x;
        private double F10(double x)
            => 10 * x + 15;

        [Fact]
        public void IntegralF1_Success()
        {
            // Arrange
            int a = 0;
            int b = 1;
            int n = 100;
            int numThreads = 4;

            // Act
            var result = LeftTriangleMethod.ParallelCalculateIntegral(F1, a, b, n, numThreads);
            var expectedResult = 0.32835;

            // Assert
            Assert.True(Math.Abs(expectedResult - result) <= EPS);
        }

        [Fact]
        public void IntegralF2_Success()
        {
            // Arrange
            int a = 0;
            int b = 1;
            int n = 100;
            int numThreads = 4;

            // Act
            var result = LeftTriangleMethod.ParallelCalculateIntegral(F2, a, b, n, numThreads);
            var expectedResult = 2.475;

            // Assert
            Assert.True(Math.Abs(expectedResult - result) <= EPS);
        }

        [Fact]
        public void IntegralF3_Success()
        {
            // Arrange
            int a = 0;
            int b = 1;
            int n = 100;
            int numThreads = 4;

            // Act
            var result = LeftTriangleMethod.ParallelCalculateIntegral(F3, a, b, n, numThreads);
            var expectedResult = 0.455487;

            // Assert
            Assert.True(Math.Abs(expectedResult - result) <= EPS);
        }

        [Fact]
        public void IntegralF4_Success()
        {
            // Arrange
            int a = 0;
            int b = 1;
            int n = 100;
            int numThreads = 4;

            // Act
            var result = LeftTriangleMethod.ParallelCalculateIntegral(F4, a, b, n, numThreads);
            var expectedResult = 0.843762;

            // Assert
            Assert.True(Math.Abs(expectedResult - result) <= EPS);
        }

        [Fact]
        public void IntegralF5_Success()
        {
            // Arrange
            int a = 0;
            int b = 1;
            int n = 100;
            int numThreads = 4;

            // Act
            var result = LeftTriangleMethod.ParallelCalculateIntegral(F5, a, b, n, numThreads);
            var expectedResult = 2.516908;

            // Assert
            Assert.True(Math.Abs(expectedResult - result) <= EPS);
        }

        [Fact]
        public void IntegralF6_Success()
        {
            // Arrange
            int a = 0;
            int b = 1;
            int n = 100;
            int numThreads = 4;

            // Act
            var result = LeftTriangleMethod.ParallelCalculateIntegral(F6, a, b, n, numThreads);
            var expectedResult = 0.605549;

            // Assert
            Assert.True(Math.Abs(expectedResult - result) <= EPS);
        }

        [Fact]
        public void IntegralF7_Success()
        {
            // Arrange
            int a = 0;
            int b = 1;
            int n = 100;
            int numThreads = 4;

            // Act
            var result = LeftTriangleMethod.ParallelCalculateIntegral(F7, a, b, n, numThreads);
            var expectedResult = 1.703503;

            // Assert
            Assert.True(Math.Abs(expectedResult - result) <= EPS);
        }

        [Fact]
        public void IntegralF8_Success()
        {
            // Arrange
            int a = 2;
            int b = 4;
            int n = 100;
            int numThreads = 4;

            // Act
            var result = LeftTriangleMethod.ParallelCalculateIntegral(F8, a, b, n, numThreads);
            var expectedResult = 0.695653;

            // Assert
            Assert.True(Math.Abs(expectedResult - result) <= EPS);
        }

        [Fact]
        public void IntegralF9_Success()
        {
            // Arrange
            int a = 0;
            int b = 1;
            int n = 100;
            int numThreads = 4;

            // Act
            var result = LeftTriangleMethod.ParallelCalculateIntegral(F9, a, b, n, numThreads);
            var expectedResult = 0.245025;

            // Assert
            Assert.True(Math.Abs(expectedResult - result) <= EPS);
        }

        [Fact]
        public void IntegralF10_Success()
        {
            // Arrange
            int a = 0;
            int b = 1;
            int n = 100;
            int numThreads = 4;

            // Act
            var result = LeftTriangleMethod.ParallelCalculateIntegral(F10, a, b, n, numThreads);
            var expectedResult = 19.95;

            // Assert
            Assert.True(Math.Abs(expectedResult - result) <= EPS);
        }
    }
}
