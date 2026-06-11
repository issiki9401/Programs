#include <stdio.h>

int main() {
    int n;
    
    // 持續讀取輸入，直到檔案結束 (EOF) 變更為止
    while (scanf("%d", &n) != EOF) {
        printf("Hello World\n");
    }
    
    return 0;
}