#include <stdio.h>

int calculate(int, int*);

int calculate(int m, int *times){
    if (m == 0) {
        return 0;
    }
    (*times)++;
    return (m%2) + calculate(m/10, times);
}

int main(){
    int m, times=0, result=0;
    scanf("%d", &m);

    result = calculate(m, &times);
    printf("%d\n", times-result);

    return 0;
}