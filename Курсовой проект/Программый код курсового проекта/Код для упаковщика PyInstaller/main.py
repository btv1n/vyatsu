# Импорт необходимых библиотек
import tkinter as tk
from tkinter import messagebox


# Внешние файлы
from multipass_two_way_merge import multipass_two_way_merge as MTWM  # прямое слияние
from natural_merge import natural_merge as NM  # естественное слияние
from cascade_merge import cascade_merge as CM  # каскадное слияние
from creating_large_file import creating_large_file as CLF  # создание входного файла

import os
import sys

# PyInstaller
def resource_path(relative_path):
    if hasattr(sys, "_MEIPASS"):
        base_path = sys._MEIPASS
    else:
        base_path = os.path.abspath(".")
    return os.path.join(base_path, relative_path)
# PyInstaller

class Program:
    def __init__(self):
        self.root = tk.Tk()
        self.root.geometry("750x590+400+200")
        self.root.title("Сравнительный анализ внешних сортировок")
        self.root.resizable(False, False)  # запрет на изменения размеров окна

        # Пути к внешним файлам
        # self.input_file = "data/large_input.txt"
        # self.output_file_MTWM = "data/sorted_output_multipass_two_way_merge.txt"
        # self.output_file_NM = "data/sorted_output_natural_merge.txt"
        # self.output_file_CM = "data/sorted_output_cascade_merge.txt"
        self.input_file = resource_path("data/large_input.txt") # PyInstaller
        self.output_file_MTWM = resource_path("data/sorted_output_multipass_two_way_merge.txt") # PyInstaller
        self.output_file_NM = resource_path("data/sorted_output_natural_merge.txt") # PyInstaller
        self.output_file_CM = resource_path("data/sorted_output_cascade_merge.txt") # PyInstaller

    # Создание файла и заполнение его случайными числами
    def create_large_input_file(self):
        try:
            count_numbers = int(self.entry_value1.get())
            lower_bound = int(self.entry_value2.get())
            upper_bound = int(self.entry_value3.get())

            if count_numbers > 0 and lower_bound < upper_bound:
                CLF.CreateAndFillFile.create_and_fill_file(
                    self,
                    resource_path("data/large_input.txt"),
                    count_numbers,
                    lower_bound,
                    upper_bound,
                )

                # Отображаем данные в поле вывода
                self.result_text.config(state=tk.NORMAL)
                self.result_text.insert(
                    tk.END,
                    "Файл сгенерирован.\n"
                    f"- количество чисел: {count_numbers}\n"
                    f"- нижняя граница: {lower_bound}\n"
                    f"- верхняя граница: {upper_bound}\n\n",
                )
                self.result_text.config(state=tk.DISABLED)
            else:
                messagebox.showerror(
                    "Ошибка",
                    "Количество чисел должно быть больше нуля, а нижняя граница должна быть по значению меньше верхней!",
                )
        except ValueError:
            messagebox.showerror("Ошибка", "Введены недопустимые значения!")

    # Создания меню предложения
    def create_application_menu(self):
        menubar = tk.Menu(
            self.root
        )  # создает объект меню и привязывает его к главному окну
        self.root.config(menu=menubar)  # устанавливает созданное меню в главное окно

        # Добавление подменю к меню
        settings_menu = tk.Menu(menubar, tearoff=0)  # tearoff - отключает разрыв в меню
        settings_menu.add_command(label="Очистить", command=self.clear_data)
        settings_menu.add_command(
            label="О программе", command=self.create_information_window
        )
        settings_menu.add_command(label="Выход", command=self.root.destroy)
        menubar.add_cascade(label="Файл", menu=settings_menu)

    # Дочернее окно - описание программы
    def create_information_window(self):
        new_window = tk.Toplevel(self.root)
        new_window.title("Сведения о программе")
        new_window.geometry("400x250+600+400")
        new_window.resizable(False, False)

        label = tk.Label(
            new_window,
            text="Данная программа создана для того, чтобы сравнить эффективность различных видов внешней сортировки на разном объеме входных данных. Программа выводит основные сведения о работе сортировок. Она помогает проанализировать сортировки и сравнить их друг с другом."
            "\n\nТочные названия внешних сортировок: "
            "\n - Multi-pass Two-way External Merge Sort"
            "\n - Multi-pass Multi-way Natural External Merge Sort"
            "\n - Multi-pass Two-way Cascade External Merge Sort "
            "\n\nАвтор программы: студент группы ФИб-2.",
            wraplength=380,
            justify="left",
            anchor="e",
        )
        label.pack(padx=10, pady=10)

        button_close = tk.Button(
            new_window,
            text="ОК",
            width=10,
            activebackground="#A0A0A0",
            command=new_window.destroy,
        )
        button_close.pack(padx=10, pady=(10, 10), anchor="e")

        new_window.grab_set()  # захватываем пользовательский ввод т.е. блокирует главное окно

    # Очистка всех объектов программы
    def clear_data(self):
        # Очистка трех полей ввода
        self.entry_value1.delete(0, tk.END)
        self.entry_value2.delete(0, tk.END)
        self.entry_value3.delete(0, tk.END)

        # Сброс выбора радио кнопки
        self.selected_method.set(0)

        # Очистка поля вывода информации
        self.result_text.config(state=tk.NORMAL)  # разрешаем редактирование
        self.result_text.delete(1.0, tk.END)  # очищаем текст
        self.result_text.config(
            state=tk.DISABLED
        )  # возвращаем в состояние только для чтения

    # Функция для выполнения сортировки и отображения результата
    def sort_data(self):
        sort_method = self.selected_method.get()  # получаем выбранный метод сортировки
        total_comparisons = total_time = (
            0  # общее количество времени и количества сравнений
        )

        if sort_method == 1:
            sorter1 = MTWM.MultipassTwoWayMerge()
            total_comparisons, total_time = sorter1.external_sort(
                self.input_file,
                self.output_file_MTWM,
                resource_path("multipass_two_way_merge/chunks_directory"), # PyInstaller
            )
            self.print_in_tk_text(
                "сортировка многопроходным двусторонним слиянием",
                total_comparisons,
                total_time,
            )
        elif sort_method == 2:
            sorter2 = NM.NaturalMerge()
            total_comparisons, total_time = sorter2.external_sort(
                self.input_file,
                self.output_file_NM,
                resource_path("natural_merge/chunks_directory"), # PyInstaller
            )
            self.print_in_tk_text(
                "сортировка естественным слиянием", total_comparisons, total_time
            )
        elif sort_method == 3:
            sorter3 = CM.CascadeMerge()
            total_comparisons, total_time = sorter3.external_sort(
                self.input_file,
                self.output_file_CM,
                resource_path("cascade_merge/chunks_directory"), # PyInstaller
            )
            self.print_in_tk_text(
                "сортировка каскадным слиянием", total_comparisons, total_time
            )
        else:
            messagebox.showerror("Ошибка", "Не выбран метод сортировки!")

    # Отображение в поле для вывода
    def print_in_tk_text(self, text, comparisons, time):
        self.result_text.config(state=tk.NORMAL)
        self.result_text.insert(
            tk.END,
            f"Выполнена {text}.\nПараметры сортировки:\n"
            f"- количество сравнений: {comparisons}\n"
            f"- время выполнения: {time}\n\n",
        )
        # f"- использованная память: {memory}\n"
        self.result_text.config(state=tk.DISABLED)

    def create_application(self):
        self.create_application_menu()  # меню

        # Фреймы для группировки и правильного расположения элементов
        frame_left = tk.Frame(self.root)
        frame_left.grid(row=0, column=0, padx=10, pady=10)

        frame_right = tk.Frame(self.root)
        frame_right.grid(row=0, column=1, padx=10, pady=10)

        frame_label_1 = tk.LabelFrame(
            frame_left, text="Генерация файла", padx=10, pady=10, labelanchor="n"
        )
        frame_label_1.grid(row=0, column=0, padx=20, pady=20, sticky="nsew")

        frame_label_2 = tk.LabelFrame(
            frame_left, text="Внешние сортировки", padx=10, pady=10, labelanchor="n"
        )
        frame_label_2.grid(row=1, column=0, padx=20, pady=20, sticky="nsew")

        self.selected_method = (
            tk.IntVar()
        )  # создаёт переменную, которая хранит число и автоматически обновляется при изменении элемента интерфейса - радио кнопки

        # Дополнительные поля для ввода значений
        label1 = tk.Label(
            frame_label_1,
            text="Количество чисел:",
            anchor="w",
            width=26,
        )
        label1.grid(row=0, column=0, pady=5)

        self.entry_value1 = tk.Entry(
            frame_label_1, width=30, highlightthickness=1, highlightcolor="black"
        )
        self.entry_value1.grid(row=1, column=0, pady=5)

        label2 = tk.Label(
            frame_label_1,
            text="Нижняя граница:",
            anchor="w",
            width=26,
        )
        label2.grid(row=2, column=0, pady=5)

        self.entry_value2 = tk.Entry(
            frame_label_1, width=30, highlightthickness=1, highlightcolor="black"
        )
        self.entry_value2.grid(row=3, column=0, pady=5)

        label3 = tk.Label(
            frame_label_1,
            text="Верхняя граница:",
            anchor="w",
            width=26,
        )
        label3.grid(row=4, column=0, pady=5)

        self.entry_value3 = tk.Entry(
            frame_label_1, width=30, highlightthickness=1, highlightcolor="black"
        )
        self.entry_value3.grid(row=5, column=0, pady=5)

        # Кнопка создание файла с случайными числами
        button_create_file = tk.Button(
            frame_label_1,
            text="Сгенерировать",
            activebackground="#A0A0A0",
            command=self.create_large_input_file,
        )
        button_create_file.grid(row=6, column=0, pady=10)

        # Радио кнопки
        radio1 = tk.Radiobutton(
            frame_label_2,
            text="Многопр. двуст. слияние",
            variable=self.selected_method,
            value=1,
            anchor="w",
            width=20,
        )
        radio1.grid(row=7, column=0, padx=10, pady=(30, 5))

        radio2 = tk.Radiobutton(
            frame_label_2,
            text="Естественное слияние",
            variable=self.selected_method,
            value=2,
            anchor="w",
            width=20,
        )
        radio2.grid(row=8, column=0, padx=10, pady=5)

        radio3 = tk.Radiobutton(
            frame_label_2,
            text="Каскадное слияние",
            variable=self.selected_method,
            value=3,
            anchor="w",
            width=20,
        )
        radio3.grid(row=9, column=0, padx=10, pady=5)

        # Кнопка запуска сортировки
        sort_button = tk.Button(
            frame_label_2,
            text="Сортировать",
            activebackground="#A0A0A0",
            command=self.sort_data,
        )
        sort_button.grid(row=10, column=0, pady=10)

        # Текстовое окно отображения результата
        self.result_text = tk.Text(
            frame_right,
            height=31.5,
            width=50,
            wrap=tk.WORD,
            state=tk.DISABLED,
            borderwidth=1,
        )
        self.result_text.grid(row=0, column=0, pady=10, padx=10)

        # Cкроллбар
        scrollbar = tk.Scrollbar(self.root, command=self.result_text.yview)
        scrollbar.grid(row=0, column=1, sticky="nse", pady=35, padx=3)
        self.result_text.config(
            yscrollcommand=scrollbar.set
        )  # связываем скроллбар с текстовым полем

    # PyInstaller
    # Запускает программу
    def launch(self):
        self.create_application()
        self.root.mainloop()


if __name__ == "__main__":
    program = Program()  # создание объекта класса
    program.launch()  # запуск программы