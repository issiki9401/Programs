#include <stdio.h>

int compare(int m1, int m2) {
    if (m1 == m2) return 0;
    return m1 > m2 ? 1 : -1;
}

int main() {
    int a, b;
    scanf("%d %d", &a, &b);
    
    int res = compare(a, b);
    printf("%d\n", res);
    return 0;
}