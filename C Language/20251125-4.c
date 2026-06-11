#include <stdio.h>

int main(){
    int m, n, temp;
    scanf("%d %d", &m, &n);
    temp=m;

    if(n==0){
        temp=0;
    }
    else{
        for(int i=1; i<n; i++){
            temp+=m;
        }
    }

    printf("%d\n", temp);

    return 0;
}