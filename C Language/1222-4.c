#include <stdio.h>

int a(int, int);

int a(int m, int n){
    if(m<n){
        return m;
    }
    return a(m-n, n);
}

int main(){
    int m, n;
    scanf("%d %d",&m,&n);

    printf("%d\n", a(m, n));

    return 0;
}