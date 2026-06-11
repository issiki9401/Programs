#include <stdio.h>

int compare(int, int);

int compare(int m1, int m2){
    return (m1 == m2) ? 0 : (m1 > m2) ? 1 : -1;
}

int main(){
    int m1, m2;
    scanf("%d %d", &m1, &m2);

    printf("%d\n", compare(m1, m2));
    return 0;
}