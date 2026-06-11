#include <stdio.h>

int main(void) {
    unsigned int m1;
    unsigned int m2;
    unsigned int m3;
    unsigned int m4;
    unsigned int m = 0;
    printf("請輸入一個非負整數 m1: ");
    scanf("%u", &m1);
    printf("請輸入一個非負整數 m2: ");
    scanf("%u", &m2);
    printf("請輸入一個非負整數 m3: ");
    scanf("%u", &m3);
    printf("請輸入一個非負整數 m4: ");
    scanf("%u", &m4);

    if (m1 % 2 == 1)  // 或者 (m & 1)
        m += 1;
    if (m2 % 2 == 1)
        m += 1;
    if (m3 % 2 == 1)
        m += 1;
    if (m4 % 2 == 1)
        m += 1;
    if (m == 3 || 4)
        printf("有3或4個數的值是奇數\n");
    else
        printf("奇數的個數為: %d\n", m);
}