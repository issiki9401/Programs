
#include <stdio.h>
#include <math.h>

int main() {
    unsigned int m;
    
    printf("请输入一个非负整数 m: ");
    scanf("%u", &m);
    
    // 获取千位数和百位数
    int thousand_digit = (m / 1000) % 10;
    int hundred_digit = (m / 100) % 10;
    
    // 计算交换后的数值
    int swapped_m = m;
    
    // 减去原来的千位数和百位数
    swapped_m -= thousand_digit * 1000;
    swapped_m -= hundred_digit * 100;
    
    // 加上交换后的千位数和百位数
    swapped_m += hundred_digit * 1000;
    swapped_m += thousand_digit * 100;
    
    printf("原始数值: %u\n", m);
    printf("交换后的数值: %d\n", swapped_m);
    
    return 0;
}
//小記：不一定要創立spapped_m，可以直接使用m計算（只是為了清晰表示交換前後的差異）