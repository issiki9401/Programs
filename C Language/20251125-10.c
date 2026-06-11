#include <stdio.h>

int main(){
    int m;
    printf("請輸入一個整數m:\n");
    scanf("%d",&m);

    int sum=0;

    for(int i=1; i<=m; i++){
        if(m%i==0){
            sum+=i;
        }
    }
    printf("m的所有因數和為:%d\n",sum);
    
    return 0;
}