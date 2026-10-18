using FuctionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Чёрный ящик — проверяем поведение функции по входным данным и ожидаемому результату, не заглядывая во внутреннюю реализацию.
Белый ящик — проверяем разные ветви и условия, которые есть в коде программы.
Граничные значения — проверяем значения около критических точек, где поведение функции меняется или вычисление становится невозможным.
*/

namespace FunctionAppTests
{
    public class FuctionCalculatorTests
    {
        // ЧЕРНЫЙ ЯЩИК

        /*
        Примеры классов эквивалентности:
        Входные значения - Ожидаемое поведение
        x<0 - исключение
        x=0 - результат 0
        x>0, x != 10 - неотрицательный вещественный результат
        x=10 - исключение
        */

        [TestCase(0, 0)]
        [TestCase(1, 0.1111111111111111)] // [TestCase(1, 1.0 / 9.0)]
        [TestCase(9, 3)]
        [TestCase(11, 3.3166247903554)] // [TestCase(11, Math.Sqrt(11))]
        [TestCase(25, 0.3333333333333333)] // [TestCase(25, Math.Sqrt(25.0 / 225.0))]
        public void Calculate_WhenXIsValid_ReturnExpected(double x, double expected)
        {
            // Arrange
            var calculator = new FunctionCalculator();

            // Act
            var result = calculator.Calculate(x);

            // Assert
            Assert.That(result, Is.EqualTo(expected).Within(0.000001));
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Calculate_WhenXIsNegative_ThrowArgumentOutOfRangeException(double x)
        {
            // Arrange
            var calculate = new FunctionCalculator();

            // Act
            void Act() => calculate.Calculate(x);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(Act);
        }

        [Test]
        public void Calculate_WhenXEquals10_ThrowArgumentException()
        {
            // Arrange
            var calculate = new FunctionCalculator();
            double x = 10;

            // Act
            void Act() => calculate.Calculate(x);

            // Assert
            Assert.Throws<DivideByZeroException>(Act);
        }


        // ГРАНИЧНЫЕ ЗНАЧЕНИЯ

        [TestCase(-0.000001)]
        public void Calculate_WhenXIsJustBelowZero_ReturnArgumentOutOfRangeException(double x)
        {
            // Arrange
            var calculated = new FunctionCalculator();

            // Act
            void Act() => calculated.Calculate(x);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(Act);
        }

        public void Calculate_WhenXEqualsZero_ReturnZero()
        {
            // Arrange
            var calculate = new FunctionCalculator();
            double x = 0;

            // Act
            var result = calculate.Calculate(x);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [TestCase(0.000001)]
        public void Calculate_WhenXIsJustAboveZero_ReturnPositiveResult(double x)
        {
            // Arrange
            var calculate = new FunctionCalculator();

            // Act
            var result = calculate.Calculate(x);

            // Assert
            Assert.That(result, Is.GreaterThan(0));
        }

        [TestCase(9.999999)]
        public void Calculate_WhenXIsJustBelow10_ReturnPositiveResult(double x)
        {
            // Arrange
            var calculate = new FunctionCalculator();

            // Act
            var result = calculate.Calculate(x);

            // Assert
            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void Calculate_WhenXEquals10_ReturnDivideByZeroException()
        {
            // Arrange
            var calculate = new FunctionCalculator();
            double x = 10;

            // Act
            void Act() => calculate.Calculate(x);

            // Assert
            Assert.Throws<DivideByZeroException>(Act);
        }

        [TestCase(10.000001)]
        public void Calculate_WhenXIsJustAbove10_ReturnPositiveResult(double x)
        {
            // Arrange
            var calculate = new FunctionCalculator();

            // Act
            var result = calculate.Calculate(x);

            // Assert
            Assert.That(result, Is.GreaterThan(0));
        }


        // БЕЛЫЙ ЯЩИК
        // Белый ящик предполагает, что мы знаем внутреннюю логику программы и проверяем каждую ветвь if.

        [Test]
        public void Calculate_WhenXIsNan_ThrowsArgumentException()
        {
            // Arrange
            var calculate = new FunctionCalculator();
            double x = double.NaN;

            // Act
            void Act() => calculate.Calculate(x);

            // Assert
            Assert.Throws<ArgumentException>(Act);
        }

        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity)]
        public void Calculate_WhenXIsInfinity_ThrowsArgumentException(double x)
        {
            // Arrage
            var calculate = new FunctionCalculator();

            // Act
            void Act() => calculate.Calculate(x);

            // Assert
            Assert.Throws<ArgumentException>(Act);
        }

        [Test]
        public void Calculate_WhenXEquals10_ThrowsDivideByZeroException1()
        {
            // Arrage
            var calculate = new FunctionCalculator();
            double x = 10;

            // Act
            void Act() => calculate.Calculate(x);

            // Assert
            Assert.Throws<DivideByZeroException>(Act);
        }
    }
}
