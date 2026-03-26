import os
import sys
import pytest
import time
import tempfile


# sys.path.append("/путь/к/папке/проекта")
# запуск тестов: pytest,
# pytest test/test_time_multipass_two_way_merge.py,
# pytest tests/test_performance.py -v
# запуск: pytest test/test_time.py -s

sys.path.insert(
    1, os.path.join(sys.path[0], "..")
)  # выход к родительской папке ; '../../Folder1'


from multipass_two_way_merge import multipass_two_way_merge
from natural_merge import natural_merge
from cascade_merge import cascade_merge
from creating_large_file import creating_large_file


# Алгоритмы сортировки и лимит времени
@pytest.mark.parametrize(
    "sort_class",
    [
        multipass_two_way_merge.MultipassTwoWayMerge,
        cascade_merge.CascadeMerge,
        natural_merge.NaturalMerge,
    ],
)
@pytest.mark.parametrize("max_time", [30.0])  # максимальное время в секундах
def test_sorting_time(sort_class, max_time):
    """
    Проверяет, что сортировки выполняются в пределах заданного времени.
    """

    # Создаём временную директорию для всех файлов (input, output, chunks)
    with tempfile.TemporaryDirectory() as tmpdir:
        # Пути к входному, выходному и временному каталогу
        input_file = os.path.join(tmpdir, "input.txt")
        output_file = os.path.join(tmpdir, "output.txt")
        chunks_dir = os.path.join(tmpdir, "chunks")

        # Генерирует тестовый входной файл
        large_file = creating_large_file.CreateAndFillFile()
        large_file.create_and_fill_file(
            input_file, num_numbers=1000, lower_bound=0, upper_bound=10000
        )

        # Инициализирует класс сортировки
        sorter = sort_class()

        # Засекает время начала сортировки
        start = time.time()

        # Выполняет сортировку
        comparisons, total_time = sorter.external_sort(
            input_file, output_file, chunks_dir=chunks_dir
        )

        elapsed = time.time() - start  # Фактическое время выполнения

        # Печать результата в консоль
        print(f"\n{sorter} завершилась за {elapsed:.4f} сек")

        # Проверяет, что результат записан
        assert os.path.exists(output_file), "Файл с отсортированными данными не найден."

        # Проверяет, что сортировка завершилась за допустимое время
        assert elapsed < max_time, (
            f"Сортировка заняла {elapsed:.2f} сек., "
            f"что превышает лимит {max_time} сек."
        )
