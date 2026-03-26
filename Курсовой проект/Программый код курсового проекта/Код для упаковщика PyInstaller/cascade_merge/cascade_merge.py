import os
import heapq
import time


class CascadeMerge:
    # Функция для разделения большого файла на несколько естественно отсортированных частей
    def natural_split(self, input_file, chunks_dir="chunks"):
        comparisons = 0  # переменная для подсчета сравнений

        # создаем директорию, если её нет
        if not os.path.exists(chunks_dir):
            os.makedirs(chunks_dir)

        chunks = []
        current_chunk = []

        with open(input_file, "r") as f:
            previous = None  # предыдущее число
            for line in f:
                num = int(line.strip())
                # Если предыдущий файл не пуст и следующее число больше его
                if previous is None or num >= previous:
                    current_chunk.append(num)
                else:
                    # Новый блок, когда найдено число меньшее previous
                    chunk_filename = os.path.join(
                        chunks_dir, f"chunk_{len(chunks)}.txt"
                    )
                    # Запись последовательности в файл
                    with open(chunk_filename, "w") as chunk_file:
                        for number in current_chunk:
                            chunk_file.write(f"{number}\n")
                    chunks.append(chunk_filename)
                    current_chunk = [
                        num
                    ]  # начинает новый блок, добавляя число в массив
                previous = num
                comparisons += 1

            # Если после завершения чтения файла, текущий chunk не пуст, записываем его в файл
            if current_chunk:
                chunk_filename = os.path.join(chunks_dir, f"chunk_{len(chunks)}.txt")
                with open(chunk_filename, "w") as chunk_file:
                    for number in current_chunk:
                        chunk_file.write(f"{number}\n")
                chunks.append(chunk_filename)

        return chunks, comparisons

    # Слияние двух файлов в один
    def merge_two_files(self, file1, file2, output_file):
        comparisons = 0
        with open(file1, "r") as f1, open(file2, "r") as f2, open(
            output_file, "w"
        ) as output:
            # Извлекаем по одному числу из каждого файла
            number_1 = f1.readline().strip()
            number_2 = f2.readline().strip()

            # Пока есть числа в обоих файлах
            while number_1 and number_2:
                comparisons += 1
                if int(number_1) < int(number_2):
                    output.write(f"{number_1}\n")
                    number_1 = f1.readline().strip()
                else:
                    output.write(f"{number_2}\n")
                    number_2 = f2.readline().strip()

            # Пока есть числа в первом файле
            while number_1:
                output.write(f"{number_1}\n")
                number_1 = f1.readline().strip()

            # Пока есть числа во втором файле
            while number_2:
                output.write(f"{number_2}\n")
                number_2 = f2.readline().strip()

        return comparisons

    # Функция каскадного слияния. Итеративно сливает пары файлов из списка chunks, пока не останется одни итоговый файл.
    # chunks - список путей к отсортированным файлам
    def cascade_merge(self, chunks, output_file, chunks_dir="chunks"):
        comparisons = 0  # счетчик всех сравнений при слиянии
        current_chunks = chunks  # текущий список файлов, который надо сливать
        round_num = 0  # контролирует количество циклов обработки файлов

        # Пока количество файлов в списке больше одного
        while len(current_chunks) > 1:
            new_chunks = (
                []
            )  # новый список файлов, который получается после текущего круга слияния
            for i in range(0, len(current_chunks), 2):
                # Если у текущего файла есть пара, сливаем их
                if i + 1 < len(current_chunks):
                    output_chunk = os.path.join(
                        chunks_dir, f"merged_{round_num}_{len(new_chunks)}.txt"
                    )
                    # Сливает два файла, результат записывается в output_chunk
                    merge_comparisons = self.merge_two_files(
                        current_chunks[i], current_chunks[i + 1], output_chunk
                    )
                    comparisons += merge_comparisons
                    new_chunks.append(
                        output_chunk
                    )  # добавляет результат слияния в список новых chunks
                else:
                    # Если нечётное количество, добавляем последний файл без изменений
                    new_chunks.append(current_chunks[i])
            round_num += 1
            current_chunks = (
                new_chunks  # обновляем список файлов для следующего круга слияния
            )

        # Проверяем, что список не пуст
        try:
            # Удаляем итоговый файл, если он уже существует. Необходимо для повторного запуска алгоритма
            if os.path.exists(output_file):
                os.remove(output_file)

            # Переименовываем итоговый результат
            os.rename(current_chunks[0], output_file)
        except IndexError:
            # Создание пустого файла внутри папки 'data'
            file_path = os.path.join("data", "sorted_output_cascade_merge.txt")
            file = os.open(
                file_path, os.O_CREAT | os.O_WRONLY
            )  # создание файла и открытие его на запись
            os.close(file)
            print("Ошибка: список current_chunks пуст.")

        return comparisons

    # Главная функция
    def external_sort(self, input_file, output_file, chunks_dir="chunks_dir"):
        start_time = time.time()  # замер времени начала внешней сортировки

        chunks, split_comparisons = self.natural_split(
            input_file, chunks_dir
        )  # разделение на естественные блоки
        merge_comparisons = self.cascade_merge(
            chunks, output_file, chunks_dir
        )  # каскадное слияние
        total_comparisons = (
            split_comparisons + merge_comparisons
        )  # количество сравнений

        end_time = time.time()  # замер времени конца внешней сортировки

        total_time = f"{end_time - start_time:.4f}"  # общее время выполнения

        # Если входной файл пуст — просто создаем пустой выходной
        if os.path.getsize(input_file) == 0:
            open(output_file, "w").close()

        # Вывод информации о времени и количестве сравнений
        # print(f"Общее количество сравнений: {total_comparisons}")
        # print(f"Общее время выполнения: {total_time} секунд")

        return total_comparisons, total_time
