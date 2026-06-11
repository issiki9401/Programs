#include <stdio.h>

int a(int, int, int, int);

int a(int m, int n, int i, int x){
    if(m%i==0 && n%i==0){
        x++;
    }
    if(i==m || i==n){
        return x;
    }

    return a(m, n, i+1, x);
}

int main(){
    int m, n, i=1, x=0;
    scanf("%d %d",&m,&n);

    if(a(m, n, i, x)==1){
        printf("Yes\n");
    }
    else{
        printf("No\n");
    }

    return 0;
}