#include <stdio.h>

int main(void) {
    unsigned int m;
    printf("請輸入一個非負整數 m: ");
    scanf("%u", &m);

    unsigned int mask = 0x55555555; // 二進位：010101... 偶數位為1, 奇數位為0
    unsigned int result = m | mask;

    printf("設定偶數位為1後結果 = %u\n", result);
    return 0;
}