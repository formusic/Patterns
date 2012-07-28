using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConsoleApplication1
{
    // Conditions  http://osherove.com/tdd-kata-1/
    public class SсTests
    {
        [Test]
        public void ReturnZeroForEmpty()
        {
            // arrange
            var calc = new StringCalculator();
            // act
            int result = calc.Add("");
            // assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void ReturnFiveForFive()
        {
            // arrange
            var calc = new StringCalculator();
            // act
            int result = calc.Add("5");
            // assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Return6For4_2()
        {
            // arrange
            var calc = new StringCalculator();
            // act
            int result = calc.Add("4,2");
            // assert
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Return97For4_2_5_6_10_20_50()
        {
            // arrange
            var calc = new StringCalculator();
            // act
            int result = calc.Add("4,2,5,6,10,20,50");
            // assert
            Assert.AreEqual(97, result);
        }

        [Test]
        public void Return6For1Enter2Zapataya3()
        {
            // arrange
            var calc = new StringCalculator();
            // act
            int result = calc.Add("1\n2,3");
            // assert
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Return1For1Enter()
        {
            // arrange
            var calc = new StringCalculator();
            // act
            int result = calc.Add("1\n");
            // assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void ReturnSumForPredefinedDilimeter()
        {
            // arrange
            var calc = new StringCalculator();
            // act
            int result = calc.Add("//;\n1;2");
            // assert
            Assert.AreEqual(3, result);
        }

        [Test, ExpectedException(typeof (NegativesNotAllowed),
            ExpectedMessage = "Error: Negatives Not Allowed -2; -4")]
        public void ReturnExceptionForMinus()
        {
            // arrange
            var calc = new StringCalculator();
            // act
            calc.Add("//;\n1;-2;-4");
            // assert in header
        }
    }

    public class NegativesNotAllowed : Exception
    {
        public NegativesNotAllowed(string message)
            : base(message)
        {
        }
    }

    public class StringCalculator
    {
        public int Add(string numbers)
        {
            string delimeter = numbers.Contains("//") ? numbers.Split('/', '\n')[2] : ",\n";
            string[] numbersWithSplits = numbers.Split(delimeter.ToCharArray());
            IEnumerable<int> ints = numbersWithSplits.Select(s => Parse(s));
            IEnumerable<int> negatives = ints.SkipWhile(s => s >= 0);
            if (negatives.Any())
            {
                throw new NegativesNotAllowed("Error: Negatives Not Allowed "
                                              + String.Join("; ", negatives));
            }
            return ints.Sum(s => s);
        }

        private static int Parse(string number)
        {
            int resultParse;
            Int32.TryParse(number, out resultParse);
            return resultParse;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
        }
    }
}