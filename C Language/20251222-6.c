#include <stdio.h>

int a(int m, int i) {
    if (i > m) {
        return 0;
    }

    if (m % i == 0) {
        return i + a(m, i + 1);
    } else {
        return a(m, i + 1);
    }
}

int main() {
    int m;
    if (scanf("%d", &m) != 1) return 0;

    int sum = a(m, 1);
    
    printf("%d\n", sum);

    return 0;
}