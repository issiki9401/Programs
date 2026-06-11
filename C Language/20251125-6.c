#include <stdio.h>

int main(){
    int m, n = 0;
    scanf("%d %d",&m,&n);

    while(m>=n){
        m-=n;
    }

    printf("%d\n", m);

    return 0;
}