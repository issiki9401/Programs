#include <stdio.h>

int main(){
    int n, digits = 0;
    scanf("%d",&n);

    while(n!=0){
        digits=n;
        n/=10;
    }

    printf("%d\n", digits);

    return 0;
}