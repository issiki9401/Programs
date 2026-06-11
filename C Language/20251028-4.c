#include <stdio.h>

int main(void) {
    unsigned int m;
    printf("請輸入一個非負整數 m: ");
    scanf("%u", &m);

    unsigned int mask = 0x55555555; // 偶數位為1
    unsigned int result = m ^ mask; // XOR反轉偶數位

    printf("反轉偶數位後結果 = %u\n", result);
    return 0;
}