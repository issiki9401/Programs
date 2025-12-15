#include <stdio.h>

void calculate(int);

void calculate(int m) {
    int count=0;
    while(m!=0){
        m/=10;
        if(m%2==0){
            count++;
        }
    }
    printf("%d", count);
}

int main() {
    int m;
    scanf("%d", &m);
    
    calculate(m);
    printf("\n");
    return 0;
}