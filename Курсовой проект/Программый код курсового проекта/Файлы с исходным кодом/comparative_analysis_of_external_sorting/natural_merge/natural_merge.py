import os
import heapq
import time
import math


class NaturalMerge:
    # Функция для разделения большого файла на несколько естественно отсортированных частей
    def natural_split(self, input_file, chunks_dir="chunks_directory"):
        comparisons = 0  # переменная для подсчета сравнений

        # Создаем директорию, если её нет
        if not os.path.exists(chunks_dir):
            os.makedirs(chunks_dir)

        chunks = []
        current_chunk = []

        # Ищет отсортированные chunks в файле
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
                chunk_filename = os.path.join(
                    chunks_dir, f"chunk_{len(chunks)}.txt"
                )  # "chunks_dir/chunk_777.txt"
                with open(chunk_filename, "w") as chunk_file:
                    for number in current_chunk:
                        chunk_file.write(f"{number}\n")
                chunks.append(chunk_filename)

        return chunks, comparisons

    # Функция для слияния отсортированных частей
    # chunks - список путей к отсортированным файлам
    def merge_files(self, chunks, output_file):
        comparisons = 0  # переменная для подсчета сравнений

        files = [
            open(chunk, "r") for chunk in chunks
        ]  # открывает все файлы из chunks на чтение
        output = open(output_file, "w")  # открывает итоговый файл на запись

        # Создает пустую кучу, которая будет содержать пары (значение, индекс_файла)
        heap = []

        # Добавляет первые элементы каждого файла в кучу
        # Итерируется по всем открытым файлам
        for index, file in enumerate(files):
            number = (
                file.readline().strip()
            )  # считывает одну строку из файла + убирает лишние пробелы и \n
            # Если строка не пустая, преобразует ее в число и положит в кортеж
            if number:
                heapq.heappush(heap, (int(number), index))

        # Слияние данных, пока в куче есть элементы
        while heap:
            smallest, index = heapq.heappop(
                heap
            )  # извлекает наименьшее число из кучи, а также индекс файла, из которого оно пришло
            output.write(f"{smallest}\n")  # записывает это число в выходной файл

            # Читает следующий элемент из того файла, откуда пришло smallest
            next_number = files[index].readline().strip()
            # Если следующая строка не пуста - добавляем это число снова в кучу из того же файла
            if next_number:
                heapq.heappush(heap, (int(next_number), index))

            comparisons += int(
                math.log2(len(heap) + 1)
            )  # +1 - чтобы избежать log(0). Увеличиваем счетчик сравнений при каждом извлечении из кучи

        # Закрывает все файлы
        for file in files:
            file.close()
        output.close()

        return comparisons

    # Главная функция
    def external_sort(self, input_file, output_file, chunks_dir="chunks_directory"):
        start_time = time.time()  # замер времени начала внешней сортировки

        chunks, split_comparisons = self.natural_split(
            input_file, chunks_dir
        )  # разделение на естественные блоки
        merge_comparisons = self.merge_files(
            chunks, output_file
        )  # слияние отсортированных блоков
        total_comparisons = split_comparisons + merge_comparisons

        end_time = time.time()  # замер времени конца внешней сортировки

        total_time = f"{end_time - start_time:.4f}"  # Общее время выполнения

        # Вывод информации о времени и количестве сравнений
        # print(f"Общее количество сравнений: {total_comparisons}")
        # print(f"Общее время выполнения: {total_time} секунд")

        return total_comparisons, total_time
