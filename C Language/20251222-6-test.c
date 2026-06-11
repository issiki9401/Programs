#include <stdio.h>

int a(int, int);

int a(int m, int i){
    if(i==m)
        return 1;
    if(m==i*i)
        return 2;
    return a(m, i+1);
}

int main(){
    int m, i=0;
    scanf("%d",&m);

    if(a(m, i)==2)
        printf("yes\n");
    else
        printf("no\n");
    
    return 0;
}