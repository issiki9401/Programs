#include <stdio.h>

int a(int);

int a(int m, int n){
    while(m>=n){
        m-=n;
    }
    return m;
}

int main(){
    int m, n, ans = 0;
    scanf("%d %d",&m,&n);

    ans = a(m, n);

    printf("%d\n", ans);

    return 0;
}