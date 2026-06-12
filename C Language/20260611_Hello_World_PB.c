#include <stdio.h>

int main() {
    int n;

    while (scanf("%d", &n) == 1) {
        if (n == 0) {
            break;
        }
        printf("Hello World\n");
    }

    return 0;
}