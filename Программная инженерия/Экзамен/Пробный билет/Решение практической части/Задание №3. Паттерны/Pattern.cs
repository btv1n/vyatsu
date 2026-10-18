using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern
{
    using System;

    // Интерфейс состояния
    interface IWaterState
    {
        void Heat(Water water);
        void Frost(Water water);
        string GetName();
    }

    // Твердое состояние
    class SolidState : IWaterState
    {
        public void Heat(Water water)
        {
            Console.WriteLine("Нагревание льда");
            Console.WriteLine("Лёд превращается в воду");

            water.SetState(new LiquidState());
        }

        public void Frost(Water water)
        {
            Console.WriteLine("Вода уже находится в твёрдом состоянии");
        }

        public string GetName()
        {
            return "SOLID";
        }
    }

    // Жидкое состояние
    class LiquidState : IWaterState
    {
        public void Heat(Water water)
        {
            Console.WriteLine("Нагревание воды");
            Console.WriteLine("Вода превращается в пар");

            water.SetState(new GasState());
        }

        public void Frost(Water water)
        {
            Console.WriteLine("Охлаждение воды");
            Console.WriteLine("Вода превращается в лёд");

            water.SetState(new SolidState());
        }

        public string GetName()
        {
            return "LIQUID";
        }
    }

    // Газообразное состояние
    class GasState : IWaterState
    {
        public void Heat(Water water)
        {
            Console.WriteLine("Нагревание пара");
            Console.WriteLine("Вода остаётся в газообразном состоянии");
        }

        public void Frost(Water water)
        {
            Console.WriteLine("Охлаждение пара");
            Console.WriteLine("Пар превращается в жидкую воду");

            water.SetState(new LiquidState());
        }

        public string GetName()
        {
            return "GAS";
        }
    }

    // Класс Water
    class Water
    {
        private IWaterState state;

        public Water(IWaterState state)
        {
            this.state = state;
        }

        public void SetState(IWaterState state)
        {
            this.state = state;
            Console.WriteLine($"Текущее состояние воды: {state.GetName()}");
        }

        public void Heat()
        {
            Console.WriteLine("\nНагревание:");
            state.Heat(this);
        }

        public void Frost()
        {
            Console.WriteLine("\nОхлаждение:");
            state.Frost(this);
        }

        public void ShowState()
        {
            Console.WriteLine($"Состояние воды: {state.GetName()}");
        }
    }

    internal class Program
    {  
        static void Main(string[] args)
        {
            // Начальное состояние - твердое
            Water water = new Water(new SolidState());

            water.ShowState();

            // Твердое -> жидкое
            water.Heat();

            // Жидкое -> Газообразное
            water.Heat();

            // Газообразное -> жидкое
            water.Frost();

            // Жидкое -> Твердое
            water.Frost();
        }
    }
}
