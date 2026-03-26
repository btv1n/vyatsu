import os
import heapq
import time
import math


class MultipassTwoWayMerge:
    # Делит файл на блоки фиксированной длины, сортируем каждый и сохраняем отдельно
    def fixed_split(self, input_file, chunk_size=1000, chunks_dir="chunks_directory"):
        comparisons = 0

        # Создаем директорию, если её нет
        if not os.path.exists(chunks_dir):
            os.makedirs(chunks_dir)

        chunks = []
        current_chunk = []

        with open(input_file, "r") as f:
            for line in f:
                num = int(line.strip())
                current_chunk.append(num)
                # Если текущий размер chunk равен заданному
                if len(current_chunk) == chunk_size:
                    current_chunk.sort()  # внутренняя сортировка
                    comparisons += int(
                        len(current_chunk) * math.log2(len(current_chunk))
                    )  # Python Timsort = O(n log n) - приближенная оценка количества сравнений при сортировке одного chunk
                    chunk_filename = os.path.join(
                        chunks_dir, f"chunk_{len(chunks)}.txt"
                    )  # создаем файл для chunk
                    with open(
                        chunk_filename, "w"
                    ) as chunk_file:  # записывает отсортированный chunk в файл
                        for number in current_chunk:
                            chunk_file.write(f"{number}\n")
                    chunks.append(chunk_filename)
                    current_chunk = []  # обнуляет текущий chunk

            # Если считали весь файл, а в текущем chunk остались незаписанные элементы
            if current_chunk:
                current_chunk.sort()
                comparisons += int(
                    len(current_chunk) * math.log2(len(current_chunk))
                )  # Python Timsort = O(n log n) - приближенная оценка количества сравнений при сортировке одного chunk
                chunk_filename = os.path.join(chunks_dir, f"chunk_{len(chunks)}.txt")
                with open(
                    chunk_filename, "w"
                ) as chunk_file:  # записывает отсортированный chunk в файл
                    for number in current_chunk:
                        chunk_file.write(f"{number}\n")
                chunks.append(chunk_filename)

        return chunks, comparisons

    # Слияние двух файлов в один
    def merge_two_files(self, file1, file2, output_file):
        comparisons = 0

        # Открывает два файла для чтения и одни для записи
        with open(file1, "r") as f1, open(file2, "r") as f2, open(
            output_file, "w"
        ) as output:
            # Считывает по одному числу из каждого файла
            line1 = f1.readline()
            line2 = f2.readline()
            while line1 and line2:
                comparisons += 1
                # Записывает числа в частично отсортированном порядке
                if int(line1) <= int(line2):
                    output.write(line1)
                    line1 = f1.readline()
                else:
                    output.write(line2)
                    line2 = f2.readline()

            # Записывает остатки в одном из файлов
            while line1:
                output.write(line1)
                line1 = f1.readline()
            while line2:
                output.write(line2)
                line2 = f2.readline()
        return comparisons

    # Итеративно объединяет пары отсортированных файлов, пока не останется один отсортированный файл.
    # chunks - список путей к временным отсортированным chunks
    def iterative_merge(self, chunks, final_output, temp_dir="merge_directory"):
        comparisons = 0  # счетчик сравнений
        round_num = 0  # контролирует количество циклов обработки файлов

        # Создаем директорию, если её нет
        if not os.path.exists(temp_dir):
            os.makedirs(temp_dir)

        # Пока в списке chunks больше одного файла, продолжаем сливать их попарно
        while len(chunks) > 1:
            new_chunks = (
                []
            )  # список для отслеживания новых файлов после слияния текущих пар
            # Проходится по chunks с шагом 2, поскольку работает в парами chunks
            for i in range(0, len(chunks), 2):
                # Если в паре второй файл не выходит за пределы списка
                if i + 1 < len(chunks):
                    output_file = os.path.join(
                        temp_dir, f"merge_{round_num}_{i//2}.txt"
                    )  # формирует имя файла для результата слияния
                    cmp = self.merge_two_files(
                        chunks[i], chunks[i + 1], output_file
                    )  # сливает два файла
                    comparisons += cmp  # считает общее количество сравнений
                    new_chunks.append(
                        output_file
                    )  # добавляет новый, объединенный файл в список, чтобы в следующем круге использовать его снова
                # Если число chunks нечетное, последний остается без пары - просто переносим его на следующий круг
                else:
                    new_chunks.append(
                        chunks[i]
                    )  # нечетный остаток - просто переносит дальше
            chunks = new_chunks  # обновляем список
            round_num += 1  # переход на следующий круг

        # Проверяем, что список не пуст
        try:
            # Удаляем итоговый файл, если он уже существует. Необходимо для повторного запуска алгоритма
            if os.path.exists(final_output):
                os.remove(final_output)

            # Переименовываем итоговый результат - последний файл, который остался
            os.rename(chunks[0], final_output)
        except IndexError:
            # Создание пустого файла внутри папки 'data'
            file_path = os.path.join(
                "data", "sorted_output_multipass_two_way_merge.txt"
            )
            file = os.open(
                file_path, os.O_CREAT | os.O_WRONLY
            )  # создание файла и открытие его на запись
            os.close(file)
            print("Ошибка: список current_chunks пуст.")

        return comparisons

    # Главная функция
    def external_sort(
        self, input_file, output_file, chunks_dir="chunks_dir", chunk_size=1000
    ):
        start_time = time.time()  # начала отсчета времени работы алгоритма

        chunks, split_comparisons = self.fixed_split(
            input_file, chunk_size=chunk_size, chunks_dir=chunks_dir
        )  # разделение на фиксированные блоки
        merge_comparisons = self.iterative_merge(
            chunks, output_file, chunks_dir
        )  # слияние блоков
        total_comparisons = split_comparisons + merge_comparisons

        end_time = time.time()  # завершение отсчета времени работы алгоритма

        total_time = f"{end_time - start_time:.4f}"

        # Если входной файл пуст — просто создаем пустой выходной
        if os.path.getsize(input_file) == 0:
            open(output_file, "w").close()

        # print(f"Общее количество сравнений: {total_comparisons}")
        # print(f"Общее время выполнения: {total_time} секунд")

        return total_comparisons, total_time
