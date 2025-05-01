#include <cstdio>

using namespace std;

int main()
{
    size_t n, k, m;
    scanf_s("%zu %zu %zu", &n, &k, &m); // zu - беззнакове целое число ; z - size_t | u - unsigned

    std::printf("YES\n");
    for (size_t i = 0; i < k; ++i) {
        size_t q;
        scanf_s("%zu", &q); // вводим количество видов товара

        unsigned s = 0;
        for (size_t j = 0; j < q; ++j) {
            unsigned x;
            scanf_s("%u", &x); // вводим товары

            s += x;
        }

        std::printf("%u\n", 1 + s % m); // формула для решения задачи
    }

    return 0;
}