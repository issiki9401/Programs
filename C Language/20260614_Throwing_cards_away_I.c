#include <stdio.h>

int main() {
    int n;
    while (scanf("%d", &n) == 1 && n != 0) {
        int q[10000], head = 0, tail = n;
        for (int i = 0; i < n; ++i) {
            q[i] = i + 1;
        }

        printf("Discarded cards:");
        int first = 1;
        while (tail - head > 1) {
            if (!first) printf(",");
            printf(" %d", q[head++]);
            first = 0;
            
            // 將下一個元素移到尾端
            q[tail++] = q[head++];
        }
        printf("\nRemaining card: %d\n", q[head]);
    }
    return 0;
}