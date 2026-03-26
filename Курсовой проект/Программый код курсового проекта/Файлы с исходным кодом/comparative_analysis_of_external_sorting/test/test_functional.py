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


# Алгоритмы сортировки
@pytest.mark.parametrize(
    "sort_class",
    [
        multipass_two_way_merge.MultipassTwoWayMerge,
        cascade_merge.CascadeMerge,
        natural_merge.NaturalMerge,
    ],
)
def test_sorting_functional(sort_class):
    """
    Проверяет корректность работы сортировок.
    """

    # Создаём временную директорию для всех файлов (input, output, chunks)
    with tempfile.TemporaryDirectory() as tmpdir:
        # Пути к каталогам
        input_file = os.path.join(tmpdir, "input.txt")
        output_external = os.path.join(tmpdir, "output_external.txt")
        output_internal = os.path.join(tmpdir, "output_internal.txt")
        chunks_dir = os.path.join(tmpdir, "chunks")

        # Генерирует тестовый входной файл
        large_file = creating_large_file.CreateAndFillFile()
        large_file.create_and_fill_file(
            input_file, num_numbers=1000, lower_bound=0, upper_bound=10000
        )

        # Читает и сортирует встроенным методом
        with open(input_file, "r") as f:
            numbers = [int(line.strip()) for line in f if line.strip()]
        sorted_internal = sorted(numbers)

        # Записывает отсортированный файл
        with open(output_internal, "w") as f1:
            for num in sorted_internal:
                f1.write(f"{num}\n")

        # Сортировка алгоритмом внешней сортировки
        sorter = sort_class()  # инициализирует класс сортировки
        sorter.external_sort(input_file, output_external, chunks_dir=chunks_dir)

        # Чтение результата внешней сортировки
        with open(output_external, "r") as f2:
            sorted_external = [int(line.strip()) for line in f2 if line.strip()]

        # Сравнение файлов внутренней и внешней сортировки
        assert sorted_external == sorted_internal, (
            f"{sort_class.__name__}: Результат внешней сортировки отличается от "
            "встроенной сортировки Python."
        )
