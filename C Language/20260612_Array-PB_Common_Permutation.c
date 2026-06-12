#include <stdio.h>
#include <string.h>

#define MAX_LEN 1005

// 尋找兩個數字的最小值
int min(int x, int y) {
    return (x < y) ? x : y;
}

int main() {
    char a[MAX_LEN];
    char b[MAX_LEN];

    // 每次讀取兩行字串，直到檔案結束
    while (fgets(a, MAX_LEN, stdin) && fgets(b, MAX_LEN, stdin)) {
        int count_a[256] = {0};
        int count_b[256] = {0};

        // 統計字串 a 的字元出現次數
        for (int i = 0; a[i] != '\0' && a[i] != '\n'; i++) {
            count_a[(unsigned char)a[i]]++;
        }

        // 統計字串 b 的字元出現次數
        for (int i = 0; b[i] != '\0' && b[i] != '\n'; i++) {
            count_b[(unsigned char)b[i]]++;
        }

        // 依 ASCII 碼順序（即字典序）輸出共同的字元
        for (int i = 0; i < 256; i++) {
            if (count_a[i] > 0 && count_b[i] > 0) {
                int repeat = min(count_a[i], count_b[i]);
                for (int j = 0; j < repeat; j++) {
                    putchar(i);
                }
            }
        }
        putchar('\n');
    }

    return 0;
}