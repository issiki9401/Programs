
#include <stdio.h>
#include <math.h>

int main() {
    unsigned int m;
    
    printf("請輸入一個非負整數 m: ");
    scanf("%u", &m);
    
    int thousand_digit = (m / 1000) % 10;
    int hundred_digit = (m / 100) % 10;
    
    int swapped_m = m;
    
    swapped_m -= thousand_digit * 1000;
    swapped_m -= hundred_digit * 100;
    
    swapped_m += hundred_digit * 1000;
    swapped_m += thousand_digit * 100;
    
    printf("原始數值: %u\n", m);
    printf("交換後的數值: %d\n", swapped_m);
    
    return 0;
}
//小記：不一定要創立spapped_m，可以直接使用m計算（只是為了清晰表示交換前後的差異）