#include <stdio.h>

int main(){
    int m, n, quotient = 0;
    scanf("%d %d",&m,&n);

    while(m>=n){
        m-=n;
        quotient++;
    }

    printf("%d\n",quotient);

    return 0;
}