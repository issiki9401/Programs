#include <stdio.h>
#include <math.h>

int a(int);

int a(int m){
    int root = (int)sqrt(m);

    if(root*root == m){
        return 0;
    }
    else{
        return 1;
    }
}

int main(){
    int m;
    printf("請輸入一個整數m:\n");
    scanf("%d",&m);

    ans = a(m);
    
    if(ans == 0){
        printf("m是完全平方數\n");
    }
    else{
        printf("m不是完全平方數\n");
    }

    return 0;
}