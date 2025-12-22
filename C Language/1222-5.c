#include <stdio.h>

int a(int, int);

int a(int m, int n){
    int count=0, x;

    if(m<n){
        x=n;
    }
    else{
        x=m;
    }

    for(int i=1; i<=x; i++){
        if(m%i==0 && n%i==0){
        count++;
        }
    }

    if(m==1 || n==1){
        return 0;
    }
    else if(count==1){
        return 1;
    }
    else{
        return 2;
    }
}

int main(){
    int m, n, ans;
    printf("請輸入兩個整數m和n:\n");
    scanf("%d %d",&m,&n);

    ans = a(m, n);

    if(ans==0){
        printf("m或n不能為1\n");
    }
    else if(ans==1){
        printf("m和n互質\n");
    }
    else{
        printf("m和n不互質\n");
    }

    return 0;
}