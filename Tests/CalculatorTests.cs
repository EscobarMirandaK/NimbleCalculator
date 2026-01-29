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
            (int result, string formula) = calculator.Add(input);

            Assert.Equal(20, result);
            Assert.Equal("20 = 20", formula);
        }

        [Fact]
        public void EmptyInput_ReturnsZero()
        {
            var calculator = new Calculator();
            var input = "";
            (int result, string formula) = calculator.Add(input);

            Assert.Equal(0, result);
            Assert.Equal("0 = 0", formula);
        }

        [Fact]
        public void InvalidNumber_ReturnsSumIgnoringInvalids()
        {
            var calculator = new Calculator();
            var input = "5,tytyt";
            (int result, string formula) = calculator.Add(input);

            Assert.Equal(5, result);
            Assert.Equal("5 + 0 = 5", formula);
        }

        [Fact]
        public void MultipleNumbers_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "1,2,3,4,5,6,7,8,9,10,11,12";
            (int result, string formula) = calculator.Add(input);

            Assert.Equal(78, result);
            Assert.Equal("1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10 + 11 + 12 = 78", formula);
        }

        [Fact]
        public void NewlineDelimiter_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "1\n2";
            (int result, string formula) = calculator.Add(input);

            Assert.Equal(3, result);
            Assert.Equal("1 + 2 = 3", formula);
        }

        [Fact]
        public void MixedDelimiters_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "1\n2,3";
            (int result, string formula) = calculator.Add(input);

            Assert.Equal(6, result);
            Assert.Equal("1 + 2 + 3 = 6", formula);
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
            (int result, string formula) = calculator.Add(input);

            Assert.Equal(8, result);
            Assert.Equal("2 + 0 + 6 = 8", formula);
        }

        [Fact]
        public void CustomDelimiter_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "//#\n2#5";
            (int result, string formula) = calculator.Add(input);

            Assert.Equal(7, result);
            Assert.Equal("2 + 5 = 7", formula);
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
            (int result, string formula) = calculator.Add(input);

            Assert.Equal(66, result);
            Assert.Equal("11 + 22 + 33 = 66", formula);
        }

        [Fact]
        public void MultipleCustomDelimiter_ReturnsSum()
        {
            var calculator = new Calculator();
            var input = "//[*][!!][r9r]\n11r9r22*hh*33!!44";
            (int result, string formula) = calculator.Add(input);

            Assert.Equal(110, result);
            Assert.Equal("11 + 22 + 0 + 33 + 44 = 110", formula);
        }
    }
}