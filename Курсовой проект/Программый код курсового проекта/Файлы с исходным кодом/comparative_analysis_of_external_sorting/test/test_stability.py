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


@pytest.mark.parametrize(
    "sort_class",
    [
        multipass_two_way_merge.MultipassTwoWayMerge,
        cascade_merge.CascadeMerge,
        natural_merge.NaturalMerge,
    ],
)
def test_empty_file(sort_class):
    with tempfile.TemporaryDirectory() as tmpdir:
        """
        Проверяет стабильность сортировок при работе с пустым файлом.
        """
        input_file = os.path.join(tmpdir, "empty.txt")
        output_file = os.path.join(tmpdir, "output.txt")

        open(input_file, "w").close()  # создает пустой файл

        sorter = sort_class()
        comparisons, total_time = sorter.external_sort(
            input_file, output_file, chunks_dir=tmpdir
        )

        assert os.path.exists(output_file)

        with open(output_file, "r") as f:
            result = f.read()

        assert result.strip() == ""


@pytest.mark.parametrize(
    "sort_class",
    [
        multipass_two_way_merge.MultipassTwoWayMerge,
        cascade_merge.CascadeMerge,
        natural_merge.NaturalMerge,
    ],
)
def test_single_number_file(sort_class):
    with tempfile.TemporaryDirectory() as tmpdir:
        """
        Проверяет стабильность сортировок при работе с файлом, содержащим одно число.
        """
        input_file = os.path.join(tmpdir, "single.txt")
        output_file = os.path.join(tmpdir, "output.txt")

        with open(input_file, "w") as f:
            f.write("123")

        sorter = sort_class()
        comparisons, total_time = sorter.external_sort(
            input_file, output_file, chunks_dir=tmpdir
        )

        with open(output_file) as f:
            result = f.read().strip()

        assert result == "123"


@pytest.mark.parametrize(
    "sort_class",
    [
        multipass_two_way_merge.MultipassTwoWayMerge,
        cascade_merge.CascadeMerge,
        natural_merge.NaturalMerge,
    ],
)
def test_all_same_numbers(sort_class):
    with tempfile.TemporaryDirectory() as tmpdir:
        """
        Проверяет стабильность сортировок при работе с файлом, заполненным одинаковыми числами.
        """
        input_file = os.path.join(tmpdir, "same.txt")
        output_file = os.path.join(tmpdir, "output.txt")

        with open(input_file, "w") as f:
            for _ in range(1000):
                f.write("7\n")

        sorter = sort_class()
        comparisons, total_time = sorter.external_sort(
            input_file, output_file, chunks_dir=tmpdir
        )

        with open(output_file) as f:
            lines = f.readlines()

        assert all(line.strip() == "7" for line in lines)
