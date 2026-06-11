#include <stdio.h>

int main(void) {
    unsigned int m;
    printf("請輸入一個非負整數 m: ");
    scanf("%u", &m);

    unsigned int mask = 0xAAAAAAAA; // 二進位：101010... 偶數位為0, 奇數位為1
    unsigned int result = m & mask;

    printf("清除偶數位後結果 = %u\n", result);
    return 0;
}