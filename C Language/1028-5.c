#include <stdio.h>
#include <math.h>

int main(void) {
    float f1, f2;
    printf("請輸入浮點數 f1: ");
    scanf("%f", &f1);

    f2 = roundf(f1 * 100) / 100; // 四捨五入至小數第2位
    printf("四捨五入到百分位後 f2 = %.2f\n", f2);

    return 0;
}