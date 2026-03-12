#include <stdio.h>
#include <stdlib.h>

int main(){
    int n;
    int total_sum = 0;

    if (scanf("%d", &n) != 1 || n <= 0){
        return 1;
    }

    int arr[n];

    for(int i = 0; i <n; i++){
        scanf("%d", &arr[i]);
    }

    return 0;
}