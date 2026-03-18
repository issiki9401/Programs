#include <stdlib.h>

int sumOfAllDigits(int arr[], int n) {
    int total_sum = 0;
    for (int i = 0; i < n; i++) {
        int temp = abs(arr[i]);
        if (temp == 0) continue;
        
        while (temp > 0) {
            total_sum += temp % 10;
            temp /= 10;
        }
    }
    return total_sum;
}