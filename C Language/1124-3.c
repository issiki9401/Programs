#include <stdio.h>

int main() {
    int m;
    
    printf("請輸入整數 m（1<=m<=7）：\n");
    scanf("%d", &m);
    
    switch (m) {
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
            printf("m 代表工作日\n");
            break;
        case 6:
            printf("m 代表週六\n");
            break;
        case 7:
            printf("m 代表週日\n");
            break;
        default:
            printf("輸入無效！m 應在 1-7 之間\n");
    }
    
    return 0;
}