#include <stdio.h>

int compare(int m1, int m2) {
    if (m1 == m2) return 0;
    return m1 > m2 ? 1 : -1;
}

int main() {
    int a = 0, b = 0;
    printf("請輸入兩個整數：");
    scanf("%d %d", &a, &b);
    
    int res = compare(a, b);
    printf("比較結果：%d\n", res);
    return 0;
}