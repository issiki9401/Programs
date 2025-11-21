#include <stdio.h>

void season(int m) {
    if (m >= 3 && m <= 5) {
        printf("春季");
    } else if (m >= 6 && m <= 8) {
        printf("夏季");
    } else if (m >= 9 && m <= 11) {
        printf("秋季");
    } else if (m == 12 || m == 1 || m == 2) {
        printf("冬季");
    } else {
        printf("輸入錯誤");
    }
}

int main() {
    int m = 0;
    printf("請輸入月份（1~12）：");
    scanf("%d", &m);
    
    season(m);
    printf("\n");
    return 0;
}