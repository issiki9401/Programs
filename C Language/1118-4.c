#include <stdio.h>

void season(int m) {
    if (m >= 3 && m <= 5) {
        printf("spring");
    } else if (m >= 6 && m <= 8) {
        printf("summer");
    } else if (m >= 9 && m <= 11) {
        printf("autumn");
    } else if (m == 12 || m == 1 || m == 2) {
        printf("winter");
    } else {
        printf("input error");
    }
}

int main() {
    int m = 0;
    scanf("%d", &m);
    
    season(m);
    printf("\n");
    return 0;
}