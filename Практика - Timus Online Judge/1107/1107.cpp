#include <cstdio>

using namespace std;

int main()
{
    size_t n, k, m;
    scanf_s("%zu %zu %zu", &n, &k, &m); // zu - ���������� ����� ����� ; z - size_t | u - unsigned

    std::printf("YES\n");
    for (size_t i = 0; i < k; ++i) {
        size_t q;
        scanf_s("%zu", &q); // ������ ���������� ����� ������

        unsigned s = 0;
        for (size_t j = 0; j < q; ++j) {
            unsigned x;
            scanf_s("%u", &x); // ������ ������

            s += x;
        }

        std::printf("%u\n", 1 + s % m); // ������� ��� ������� ������
    }

    return 0;
}