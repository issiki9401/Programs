#include <stdio.h>

int main(void) {
    unsigned int m;
    printf("請輸入一個非負整數 m: ");
    scanf("%u", &m);

    unsigned int remainder = m % 1000;
    unsigned int result;

    if (remainder < 500)
        result = m - remainder;
    else
        result = m + (1000 - remainder);

    printf("四捨五入到千位數後結果 = %u\n", result);
    return 0;
}