#include <stdio.h>

int main() {
    int T, n;

    if (scanf("%d", &T) != 1) {
        return 0;
    }

    for (int i = 0; i < T; i++) {
        if (scanf("%d", &n) == 1) {
            // 印出 n 次 Hello World
            for (int j = 0; j < n; j++) {
                printf("Hello World\n");
            }
            
            if (i < T - 1) {
                printf("\n");
            }
        }
    }

    return 0;
}