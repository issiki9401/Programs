#include <stdio.h>

void roman_num(int m) {
    switch (m) {
        case 1: printf("I"); break;
        case 2: printf("II"); break;
        case 3: printf("III"); break;
        case 4: printf("IV"); break;
        case 5: printf("V"); break;
        default: printf("輸入錯誤");
    }
}

int main() {
    int m = 0;
    printf("請輸入 1~5 的整數:");
    scanf("%d", &m);
    
    roman_num(m);
    printf("\n");
    return 0;
}