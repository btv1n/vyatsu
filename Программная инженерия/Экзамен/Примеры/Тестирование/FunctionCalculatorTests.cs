using FunctionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
Тестирование черного ящика - тестируем внеиший функционал программы без знания ее внутренненго кода
Знаем что отправить на вход и знаем какой резултат должен быть на выходе
Тестирование классов эквивалетности и граничных значений

Тестирование белого ящика - это метод тестирования, который опирается на полное знание исходного кода  и внутренней структуры программы
Пишем тесты так, чтобы они гарантировано прошли  по всем веткам условий if/else, строками, циклам...
Тесты на проверку условий double.IsNaN или double.IsInfinity, написанные под конкретные защитные блоки в коде.
*/


namespace FunctionAppTests
{
    public class FuctionCalculatorTests
    {
        // ЧЕРНЫЙ ЯЩИК

        /*
        Классы эквивалентности:
        x >= 1 && x != 4 - неотрицательное вещественный результат
        x < 1 - исключение
        x = 4 - исключение
        */

        // x > 0 && x != 4 - неотрицательное вещественный результат
        [TestCase(1, 0)]
        [TestCase(8, 0.661437827766148)]
        public void Calculate_WhenXIsValid_ReturnExpected(double x, double expected)
        {
            // Arrange
            var calculator = new FunctionCalculator();

            // Act
            var result = calculator.Calculate(x);

            // Assert
            Assert.That(result, Is.EqualTo(expected).Within(0.000001));
        }

        // x < 0 - исключение
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

        // x = 0 - Ошибка ArgumentOutOfRangeException
        [TestCase(0)]
        public void Calculate_WhenXIsEqualsZero_ThrowsArgumentOutOfRangeException(double x)
        {
            // Arrange
            var calculator = new FunctionCalculator();

            // Act
            void Act() => calculator.Calculate(x);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(Act);
        }

        // x = 4 - исключение
        [Test] 
        public void Calculate_WhenXEquals4_ThrowArgumentException()
        {
            // Arrange
            var calculate = new FunctionCalculator();
            double x = 4;

            // Act
            void Act() => calculate.Calculate(x);

            // Assert
            Assert.Throws<DivideByZeroException>(Act);
        }


        // ГРАНИЧНЫЕ ЗНАЧЕНИЯ
        /*
        Проверяем:
            значения немного меньшие 1
            значения немного большие 1
            значения немного меньшие 4
            значения немного большие 4
        */

        // отрицательные значения рядом с 1
        //[TestCase(0.999)]
        [TestCase(0.999999)]
        public void Calculate_WhenXIsJustBelowOne_ReturnArgumentException(double x)
        {
            // Arrange
            var calculated = new FunctionCalculator();

            // Act
            void Act() => calculated.Calculate(x);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(Act);
        }

        // положительные значения рядом с 1
        [TestCase(1.000001)]
        //[TestCase(1.001)]
        public void Calculate_WhenXIsJustAboveOne_ReturnPositiveResult(double x)
        {
            // Arrange
            var calculate = new FunctionCalculator();

            // Act
            var result = calculate.Calculate(x);

            // Assert
            Assert.That(result, Is.GreaterThan(0));
        }


        // значения немного меньшие 4
        //[TestCase(3.999)]
        [TestCase(3.999999)]
        public void Calculate_WhenXIsJustBelow4_ReturnPositiveResult(double x)
        {
            // Arrange
            var calculate = new FunctionCalculator();

            // Act
            var result = calculate.Calculate(x);

            // Assert
            Assert.That(result, Is.GreaterThan(0));
        }


        // значения немного большие 4
        [TestCase(4.000001)]
        //[TestCase(4.001)]
        public void Calculate_WhenXIsJustAbove4_ReturnPositiveResult(double x)
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

        //[Test]
        //public void Calculate_WhenXEquals4_ThrowsDivideByZeroException1()
        //{
        //    // Arrange
        //    var calculate = new FunctionCalculator();
        //    double x = 4;

        //    // Act
        //    void Act() => calculate.Calculate(x);

        //    // Assert
        //    Assert.Throws<DivideByZeroException>(Act);
        //}
    }
}