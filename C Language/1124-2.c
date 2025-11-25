#include <stdio.h>

int main() {
    int m;
    
    printf("請輸入一個整數 m：\n");
    scanf("%d", &m);
    
    if (m == 0) {
        printf("m 是零\n");
    } else if (m > 0) {
        if (m % 2 == 0) {
            printf("m 是正偶數\n");
        } else {
            printf("m 是正奇數\n");
        }
    } else {  // m < 0
        if (m % 2 == 0) {
            printf("m 是負偶數\n");
        } else {
            printf("m 是負奇數\n");
        }
    }
    
    return 0;
}