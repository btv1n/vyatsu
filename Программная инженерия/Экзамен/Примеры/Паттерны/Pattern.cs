using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern
{

    // Абстрактный базовый класс Пользователь
    public abstract class User
    {
        public string Username { get; set; }

        protected User(string username)
        {
            Username = username;
        }

        // Абстрактный метод
        public abstract void DisplayRole();
    }

    // Пользователь: Администратор
    public class Administrator : User
    {
        // Создает конструктор для класса, который принимает имя пользователя и сразу передает его в родительский класс User
        public Administrator(string username) : base(username) { }

        public override void DisplayRole()
        {
            Console.WriteLine($"Пользователь {Username} авторизован как Администратор.");
        }
    }

    // Пользователь: Модератор
    public class Moderator : User
    {
        public Moderator(string username) : base(username) { }

        public override void DisplayRole()
        {
            Console.WriteLine($"Пользователь {Username} авторизован как Модератор.");
        }
    }

    // Пользователь: Гость
    public class Guest : User
    {
        public Guest(string username) : base(username) { }

        public override void DisplayRole()
        {
            Console.WriteLine($"Пользователь {Username} авторизован как Гость.");
        }
    }


    // Фабричный метод

    // Базовый класс создателя. Объявляет фабричный метод.
    public abstract class UserCreator
    {
        // Фабричный Метод
        public abstract User FactoryMethod(string username);

        public void InitializeUser(string username)
        {
            // Вызываем фабричный метод для получения объекта, не зная его конкретного класса
            User user = FactoryMethod(username);
            Console.Write("Вход в систему: ");
            user.DisplayRole();
        }
    }

    // Фабрика для Администраторов
    public class AdministratorCreator : UserCreator
    {
        public override User FactoryMethod(string username)
        {
            return new Administrator(username);
        }
    }

    // Фабрика для Модераторов
    public class ModeratorCreator : UserCreator
    {
        public override User FactoryMethod(string username)
        {
            return new Moderator(username);
        }
    }

    // Фабрика для Гостей
    public class GuestCreator : UserCreator
    {
        public override User FactoryMethod(string username)
        {
            return new Guest(username);
        }
    }

    // Клиент
    class Program
    {
        // Mетод работает с создателями через их базовый класс
        static void Run(UserCreator creator, string name)
        {
            creator.InitializeUser(name);
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Фабричный метод\n");

            // Инициализируем систему через разные фабрики
            UserCreator adminCreator = new AdministratorCreator();
            Run(adminCreator, "Алексей Администратор");

            UserCreator moderatorCreator = new ModeratorCreator();
            Run(moderatorCreator, "Елена Модератор");

            UserCreator guestCreator = new GuestCreator();
            Run(guestCreator, "Неизвестный пользователь");

            Console.ReadKey();
        }
    }
}


/*
Фабричнный метод — это порождающий паттерн проектирования, который определяет единый интерфейс для создания
объектов вклассе, но позволяет его подкшлассам изменять тип солздаваемых объектов.
Суть: Вместо прямого вызова оператора new для создания конкретного пользователя, этот процесс передается 
специальному методу - фабрике. Элтот метод сам решает, объект какого именно класса нужно создать и вернуть.


Преимущества: - легко добавлять новые типы объектов, не ломая старый код, но при этом в
коде появляется много новых классов и интерфейсов.
Код, который используется объекты, вообще не знает, как они устроены внутри
- Полезно, когда не известо объекты каких типов понадобятся 
 */

/*
 
Есть абстрактный класс User и три его наследника: Administrator, Moderator, Guest. 
Каждый умеет выводить свою роль.

Есть абстрактный класс UserCreator с фабричным методом FactoryMethod(), который создаёт пользователя, 
и методом InitializeUser(), который его использует.

Три конкретные фабрики — AdministratorCreator, ModeratorCreato, GuestCreator 
— переопределяют FactoryMethod() и создают соответствующий тип пользователя.

В главноей функции создаются разные фабрики и через общий метод Run()
инициализируются пользователи. 
Клиентский код работает только с базовым классом UserCreator и не знает, какой именно объект будет создан.

 код, создающий пользователя, отделён от конкретных классов пользователей 
— новый тип можно добавить, просто создав новую фабрику, не меняя существующий код.
 */