#include <stdio.h>

int calculate(int);

int calculate(int m) {
    int count=0;
    while(m!=0){
        m/=10;
        if(m%2==0){
            count++;
        }
    }
    return count;
}

int main() {
    int m, ans;
    scanf("%d", &m);
    
    ans = calculate(m);
    printf("%d\n", ans);
    return 0;
}