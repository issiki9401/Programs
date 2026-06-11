#include <stdio.h>

int main(void) {
    unsigned int m;
    printf("請輸入一個非負整數 m: ");
    scanf("%u", &m);

    if (m % 2 == 1)  // 或者 (m & 1)
        printf("%u 是奇數\n", m);
    else
        printf("%u 是偶數\n", m);

    return 0;
}