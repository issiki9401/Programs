#include <stdio.h>

int main(){
    int m;
    printf("請輸入一個整數m:\n");
    scanf("%d",&m);

    int factors[1000];
    int even[1000];
    int count=0;
    int x=0;
    int sum=0;

    for(int i=1; i<=m; i++){
        if(m%i==0){
            factors[count]=i;
            count++;
        }
    }
    for(int i=0; i<count; i++){
        if(factors[i]%2==0){
            even[x]=factors[i];
            x++;
        }
    }
    for(int i=0; i<x; i++){
        sum+=even[i];
    }
    printf("m的所有偶數因數和為:%d\n",sum);
    return 0;
}