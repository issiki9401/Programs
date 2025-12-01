#include <stdio.h>

int main(){
    int m, total= 0;
    scanf("%d",&m);

    while(m!=0){
        total+=m%10;
        m/=10;
    }

    printf("%d\n",total);
}