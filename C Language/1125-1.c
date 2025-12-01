#include <stdio.h>
#include <stdlib.h>

int main(){
    int number, temp, count, digits = 0;
    scanf("%d",&number);

    temp=abs(number);

    while(temp!=0){
        temp/=10;
        digits++;
    }
    for(int i=1; i<=digits; i++){
        if(number%2==0){
            count++;
        }
        number/=10;
    }

    printf("%d\n",count);

    return 0;
}