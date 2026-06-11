#include <stdio.h>
#include <stdlib.h>

int main() {
    int m1, m2, m3, m4, max, abs_max;
    
    printf("請輸入四個整數 m1, m2, m3, m4：\n");
    scanf("%d %d %d %d", &m1, &m2, &m3, &m4);
    
    max = m1;
    if (m2 > max) max = m2;
    if (m3 > max) max = m3;
    if (m4 > max) max = m4;
    
    abs_max = abs(max);
    
    printf("四個數中最大值的絕對值是：%d\n", abs_max);
    return 0;
}