#include <stdio.h>

int main() {
    int m1, m2, m3, result;
    
    printf("請輸入三個非負整數 m1, m2, m3：\n");
    scanf("%d %d %d", &m1, &m2, &m3);
    
    if (m1 == m2 && m2 == m3) {
        result = 1;
    } else if (m1 != m2 && m1 != m3 && m2 != m3) {
        result = -1;
    } else {
        result = 0;
    }
    
    printf("關係結果：%d\n", result);
    return 0;
}