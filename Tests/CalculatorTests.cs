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
        public void TwoParameters_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "1,5000";
            var result = calculator.Add(input);

            Assert.Equal(5001, result);
        }

        [Fact]
        public void NegavtiveValue_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = " 4,-3";
            var result = calculator.Add(input);

            Assert.Equal(1, result);
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
    }
}