#include <stdio.h>
#include <stdlib.h>

//此為函式放置的位置

int main() {
    int arr1[] = {15, -23, 7};
    int n1 = sizeof(arr1) / sizeof(arr1[0]);
    printf("1. 所有位數的和: %d\n", sumOfAllDigits(arr1, n1));

    int arr2[] = {1, 3, 4, 5, -2};
    int n2 = sizeof(arr2) / sizeof(arr2[0]);
    printf("2. 大於 3 的元素個數: %d\n", countGreaterThan3(arr2, n2));

    int arr3[] = {10, 50, 65, 100}; 
    int n3 = sizeof(arr3) / sizeof(arr3[0]);
    printf("3. 介於 '0' 與 'Z' 之間的元素個數: %d\n", countBetween0AndZ(arr3, n3));

    int arr4[] = {3, -7, 11, 5};
    int n4 = sizeof(arr4) / sizeof(arr4[0]);
    printf("4. 是否所有元素皆為奇數: %s\n", areAllOdd(arr4, n4) ? "是" : "否");

    int arr5[] = {10, 20, 20, 15, 5};
    int n5 = sizeof(arr5) / sizeof(arr5[0]);
    printf("5. 第二大數的位置(索引): %d\n", indexOfSecondLargest(arr5, n5));

    int arr6[] = {9, 7, 5, 3, 1};
    int n6 = sizeof(arr6) / sizeof(arr6[0]);
    printf("6. 是否為嚴格遞減: %s\n", isStrictlyDecreasing(arr6, n6) ? "是" : "否");

    return 0;
}