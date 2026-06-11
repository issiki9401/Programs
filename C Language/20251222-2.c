#include <stdio.h>

int a(int, int);

int a(int m, int n){
    if(n==0){
        return 0;
    }
    return m + a(m, n-1);
}

int main(){
    int m, n;
    scanf("%d %d", &m, &n);

    printf("%d\n", a(m, n));

    return 0;
}