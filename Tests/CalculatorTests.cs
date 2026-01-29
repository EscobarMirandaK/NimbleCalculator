using NimbleCalculator.Core;

namespace Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void OneParameter_ReturnsSameNumber()
        {
            var calculator = new Calculator();
            var input = "20";
            var result = calculator.Add(input);

            Assert.Equal(20, result);
        }

        [Fact]
        public void EmptyInput_ReturnsZero()
        {
            var calculator = new Calculator();
            var input = "";
            var result = calculator.Add(input);

            Assert.Equal(0, result);
        }

        [Fact]
        public void InvalidNumber_ReturnsSumIgnoringInvalids()
        {
            var calculator = new Calculator();
            var input = "5,tytyt";
            var result = calculator.Add(input);
            Assert.Equal(5, result);
        }

        [Fact]
        public void MultipleNumbers_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "1,2,3,4,5,6,7,8,9,10,11,12";
            var result = calculator.Add(input);
            Assert.Equal(78, result);
        }

        [Fact]
        public void NewlineDelimiter_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "1\n2";
            var result = calculator.Add(input);
            Assert.Equal(3, result);
        }

        [Fact]
        public void MixedDelimiters_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "1\n2,3";
            var result = calculator.Add(input);
            Assert.Equal(6, result);
        }

        [Fact]
        public void NegativeNumbers_ThrowsException()
        {
            var calculator = new Calculator();
            var input = "4,-3,-7,2";
            var exception = Assert.Throws<ArgumentException>(() => calculator.Add(input));
            Assert.Equal("Negative numbers are not allowed: -3, -7", exception.Message);
        }

        [Fact]
        public void NumbersGreaterThan1000_IgnoredInSum()
        {
            var calculator = new Calculator();
            var input = "2,1001,6";
            var result = calculator.Add(input);
            Assert.Equal(8, result);
        }

        [Fact]
        public void CustomDelimiter_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "//#\n2#5";
            var result = calculator.Add(input);
            Assert.Equal(7, result);
        }

        [Fact]
        public void CustomDelimiterWithNegativeNumbers_ThrowsException()
        {
            var calculator = new Calculator();
            var input = "//*\n4*-3*2*-1";
            var exception = Assert.Throws<ArgumentException>(() => calculator.Add(input));
            Assert.Equal("Negative numbers are not allowed: -3, -1", exception.Message);
        }

        [Fact]
        public void CustomDelimiterOfAnyLength_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "//[***]\n11***22***33";
            var result = calculator.Add(input);
            Assert.Equal(66, result);
        }

        [Fact]
        public void MultipleCustomDelimiter_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "//[*][!!][r9r]\n11r9r22*hh*33!!44";
            var result = calculator.Add(input);
            Assert.Equal(110, result);
        }
    }
}