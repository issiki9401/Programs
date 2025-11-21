#include <stdio.h>

int compare_diff(int m1, int m2) {
    return m1 - m2;  // 直接返回差值
}

int main() {
    int a = 0, b = 0;
    printf("請輸入兩個整數");
    scanf("%d %d", &a, &b);
    
    int res = compare_diff(a, b);
    printf("差值結果：%d\n", res);
    return 0;
}