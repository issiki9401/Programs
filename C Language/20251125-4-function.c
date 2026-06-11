#include <stdio.h>

int a(int, int);

int a(int m, int n){
    int temp;
    temp = m;
    if(n==0){
        temp=0;
    }
    else{
        for(int i=1; i<n; i++){
            temp+=m;
        }
    }
    return temp;
}

int main(){
    int m, n, ans;
    scanf("%d %d", &m, &n);

    ans = a(m, n);

    printf("%d\n", ans);

    return 0;
}
