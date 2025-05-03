using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

// Недоделан проект

/*
Лабораторная работа № 11
Классы с вложением в С#

Разработать приложение Windows Forms на языке C#, которое решает указанную в вашем варианте задачу. 
Объекты классов добавлять вручную и из файла. 

Определить конструктор без параметров, конструкторы с параметрами. Продумать, какие свойства должны 
быть доступны только для просмотра, а какие – и для изменения значения. Переопределить стандартный метод 
ToString() для перевода информации об объекте класса в строковый формат. Определить методы для сравнения 
двух объектов класса. При наличии точно такого же экземпляра сообщать об этом пользователю.

Предусмотреть возможность работы с произвольным числом записей, поиска записи по какому-либо признаку, 
добавления, удаления и просмотр записей, сортировки по разным полям.

Интерфейс должен позволять выполнять все нужные операции. Функциональность приложения должна соответствовать 
решаемой задаче.


Вариант 2. Описать класс «Студент». 
Класс должен включать в себя следующие поля (свойства):
•	Фамилия
•	Имя
•	Отчество
•	Дата рождения
•	Адрес
•	Телефон
•	Электронный адрес
•	Курс
•	Группа
•	Номер зачетной книжки
•	…
Используя класс «Студент», описать класс «Студенческая группа». 

Баллы: полностью реализованное приложение оценивается в 5 баллов.
*/

namespace Lab_11
{
    public partial class Form1 : Form
    {
        public class Student
        {
            // public - доступен для просмотра и изменения значения ; private - доступен только для просмотра
            public string Surname { get; set; }
            public string FirstName { get; set; }
            private string Patronymic { get; set; }
            private int DateOfBirth { get; set; } // DateTime
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            private int Course { get; set; }
            private string Group { get; set; }
            private int StudentId { get; set; }



            // Конструктор без параметров
            public Student()
            {
                //Surname = "Alex";
            }


            // Конструктор со всеми параметрами
            public Student(string surname, string firstName, string patronymic, 
                int dateOfBirth, string address, string phoneNumber, string email, 
                int course, string group, int studentId)
            {
                Surname = surname;
                FirstName = firstName;
                Patronymic = patronymic;
                DateOfBirth = dateOfBirth;
                Address = address;
                PhoneNumber = phoneNumber;
                Email = email;
                Course = course;
                Group = group;
                StudentId = studentId;
            }


            // Конструктор с выборочными параметрами
            public Student(string surname, string firstName, string address, int studentId)
            {
                Surname = surname;
                FirstName = firstName;
                Address = address;
                StudentId = studentId;
            }


            // Переопределение метода ToString()
            public override string ToString() // использование интерполяции строк
            {
                return $"Surname: {Surname},\n" +
                       $"FirstName: {FirstName},\n" +
                       $"Patronymic: {Patronymic},\n" +
                       $"DataOfBirth: {DateOfBirth},\n" +
                       $"Address: {Address},\n" +
                       $"PhoneNumber: {PhoneNumber},\n" +
                       $"Email: {Email},\n" +
                       $"Course: {Course},\n" +
                       $"Group: {Group},\n" +
                       $"StudentId {StudentId},\n";
            }
            //public override string ToString()
            //{
            //    return $"{Number}";
            //}


        }


        public Form1()
        {
            InitializeComponent();
            Student student = new Student();
            student.FirstName = "Kuznetsov";
            //student.StudentId = 999;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}