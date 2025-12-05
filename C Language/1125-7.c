#include <stdio.h>

int main(){
    int m;
    printf("請輸入一個大於1的整數m:\n");
    scanf("%d",&m);

    int count=0;

    for(int i=1; i<=m; i++){
        if(m%i==0){
        count++;
        }
    }

    if(m==1){
        printf("m不能為1\n");
    }
    else if(count==2){
        printf("m是質數\n");
    }
    else{
        printf("m不是質數\n");
    }

    return 0;
}