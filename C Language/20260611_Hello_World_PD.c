#include <stdio.h>
#include <string.h>

int main() {
    char line[1024];
    int is_first = 1;

    // 逐行讀取輸入，直到檔案結束
    while (fgets(line, sizeof(line), stdin)) {
        int p = 1;
        int has_number = 0;
        
        // 使用 strtok 切割行內的數字字串
        char *token = strtok(line, " \t\r\n");
        while (token != NULL) {
            int num;
            if (sscanf(token, "%d", &num) == 1) {
                p *= num;       // 模擬 Python 的 math.prod() 連乘
                has_number = 1;
            }
            token = strtok(NULL, " \t\r\n");
        }

        // 如果是空白行（沒有讀到任何數字），則跳過
        if (!has_number) {
            continue;
        }

        // 模擬 Python 的區塊間空行邏輯
        if (!is_first) {
            printf("\n");
        }
        is_first = 0;

        // 印出 p 次 Hello World
        for (int i = 0; i < p; i++) {
            printf("Hello World\n");
        }
    }

    return 0;
}