#include <stdio.h>

void calculate(int m, int count) {
        while(m!=0){
            m/=10;
            if(m%2==0){
                count++;
            }
        }
        printf("%d", count);
}

int main() {
    int m, count=0;
    scanf("%d", &m);
    
    calculate(m, count);
    printf("\n");
    return 0;
}