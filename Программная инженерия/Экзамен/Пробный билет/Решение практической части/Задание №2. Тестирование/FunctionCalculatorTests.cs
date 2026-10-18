using FunctionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Пример задания на тестирование
//y=1/ (10-x)
//Формальными методами черного ящика определить тестовые варианты.
//Написать код, используя TDD, использовать корректные имена тестов, выделять ААА.
//Формальными методами белого ящика определить тестовые варианты и добавить нужные тесты.


// МЕТОД ЧЕРНОГО ЯЩИКА
//
// Функция: y = 1 / (10 - x)
// 
// Формальные методы черного ящика:
// Классы эквивалентности — это группы входных данных, для которых программа должна вести себя одинаковым образом.
// 1. Разбиение на классы эквивалентности:
//    - x < 10: корректный класс, результат положительный.
//    - x > 10: корректный класс, результат отрицательный.
//    - x = 10: некорректный класс, деление на ноль.
//
// 2. Анализ граничных значений:
//    - x = 9.999: значение рядом с 10 слева.
//    - x = 10.001: значение рядом с 10 справа.
//
// Принцип работы:
// - мы не смотрим на код метода Calculate;
// - берем входное значение x;
// - заранее считаем ожидаемый результат по формуле y = 1 / (10 - x);
// - сравниваем фактический результат метода с ожидаемым.

// МЕТОД БЕЛОГО ЯЩИКА
// Покрытие ветвей if
//
// Формальный метод белого ящика: покрытие ветвей.
//
// В методе Calculate есть две ветви:
// - x == 10: выбрасывается DivideByZeroException.
// - x != 10: выполняется вычисление 1 / (10 - x).
//
// Принцип работы:
// - мы знаем внутреннюю структуру метода;
// - специально подбираем x так, чтобы попасть в нужную ветку;
// - проверяем, что ветка с исключением и ветка вычисления работают правильно.

namespace FunctionAppTests
{
    public class FunctionCalculatorTests
    {
        /// МЕТОД ЧЕРНОГО ЯЩИКА /// 
        
        [Test]
        public void Calculator_WhexXIsLessThen10_ReturnPositive()
        {
            // Arrange
            double x = 9;
            var calculator = new FunctionCalculator();

            // Act
            var result = calculator.Calculator(x);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Calculator_WhenXEquals10_ReturnException()
        {
            // Arrange
            double x = 10;
            var calculator = new FunctionCalculator();

            // Act
            void Act() => calculator.Calculator(x);

            // Assert
            Assert.Throws<ArgumentException>(Act);
        }

        [Test]
        public void Calculator_WhenXIsGreaterThen10_ReturnNegative()
        {
            // Arrange
            double x = 11;
            var calculator = new FunctionCalculator();

            // Act
            double result = calculator.Calculator(x);

            // Assert
            Assert.That(result, Is.EqualTo(-1));
        }

        [TestCase(10)]
        public void Calculator_WhenXEquals10_ReturnException2(double x)
        {
            // Arrange
            var calculator = new FunctionCalculator();

            // Act
            void Act() => calculator.Calculator(x);

            // Assert
            Assert.Throws<ArgumentException>(Act);
        }

        /// ПАРАМЕТРИЗОВАННЫЕ ТЕСТЫ ///

        [TestCase(9,1)]
        [TestCase(5, 0.2)]
        [TestCase(0, 0.1)]
        [TestCase(-10, 0.05)]
        public void Calculate_WhenXIsLessThan10_ReturnPositive(double x, double expected)
        {
            // Arrange
            var calculate = new FunctionCalculator();

            // Act
            var result = calculate.Calculator(x);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(11, -1)]
        [TestCase(20, -0.1)]
        [TestCase(12, -0.5)]
        public void Calculate_WhenXIsGreateThan10_ReturnNegative(double x, double expected)
        {
            // Arrange
            var calculated = new FunctionCalculator();

            // Act
            var result = calculated.Calculator(x);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Calculate_WhenXEquals10_ReturnException()
        {
            // Arrange
            double x = 10;
            var calculator = new FunctionCalculator();

            // Act
            void Act() => calculator.Calculator(x);

            // Assert
            Assert.Throws<ArgumentException>(Act); // DivideByZeroException
        }

        /// ГРАНИЧНЫЕ ЗНАЧЕНИЯ ///
        // Within — это допустимая погрешность при сравнении чисел

        [TestCase(9.999, 1000)]
        [TestCase(9.99, 100)]
        [TestCase(10.001, -1000)]
        [TestCase(10.01, -100)]
        public void Calculate_WhexXIsNear10_ReturnExpected(double x, double expected)
        {
            // Arrange
            var calculator = new FunctionCalculator();

            // Act
            var result = calculator.Calculator(x);
            
            // Assert
            Assert.That(result, Is.EqualTo(expected).Within(0.000001));
        }


        /// МЕТОД БЕЛОГО ЯЩИКА ///
        
        [Test]
        public void Calculate_WhenXIsEquls10_CoversCalculateBranch()
        {
            // Arrange
            var calculate = new FunctionCalculator();

            // Act
            void Act() => calculate.Calculator(10);

            // Assert
            Assert.Throws<ArgumentException>(Act);

        }

        [TestCase(8, 0.5)]
        [TestCase(9, 1)]
        [TestCase(12, -0.5)]
        public void Calcuate_WhenXIsNot10_CoversCalculateBranch(double x, double expected)
        {
            // Arrange
            var calculate = new FunctionCalculator();

            // Act
            var result = calculate.Calculator(x);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
