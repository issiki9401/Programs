#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

#define MAX_N 3005

int main() {
    int n;
    int seq[MAX_N];

    // 讀取每行的第一個數字 n，直到檔案結束 (EOF)
    while (scanf("%d", &n) != EOF) {
        // 讀取接下來的 n 個數字
        for (int i = 0; i < n; i++) {
            scanf("%d", &seq[i]);
        }

        // 若長度為 1，必為 Jolly
        if (n == 1) {
            printf("Jolly\n");
            continue;
        }

        // 用來記錄 1 到 n-1 的差值是否出現過，初始皆為 false
        bool diffs[MAX_N] = {false};
        bool is_jolly = true;

        // 計算相鄰兩數差的絕對值
        for (int i = 0; i < n - 1; i++) {
            int diff = abs(seq[i] - seq[i + 1]);

            // 差值必須在 1 到 n-1 之間，且不能重複出現
            if (diff >= 1 && diff < n && !diffs[diff]) {
                diffs[diff] = true;
            } else {
                is_jolly = false;
                break;
            }
        }

        // 輸出結果
        if (is_jolly) {
            printf("Jolly\n");
        } else {
            printf("Not jolly\n");
        }
    }

    return 0;
}