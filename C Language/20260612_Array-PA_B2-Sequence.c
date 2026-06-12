#include <stdio.h>
#include <stdbool.h>

#define MAX_N 105
#define MAX_SUM 20005 // 根據題目範圍調整，若 b[i] 最大 10000，兩數和最大約 20000

int main() {
    int n;
    int case_num = 1;
    int b[MAX_N];

    // 讀取輸入直到檔案結束 (EOF)
    while (scanf("%d", &n) != EOF) {
        for (int i = 0; i < n; i++) {
            scanf("%d", &b[i]);
        }

        bool is_b2 = true;

        // 檢查條件 1：必須為正整數 (b[0] >= 1)
        if (b[0] < 1) {
            is_b2 = false;
        }

        // 檢查條件 2：必須嚴格遞增
        for (int i = 1; i < n; i++) {
            if (b[i] <= b[i - 1]) {
                is_b2 = false;
                break;
            }
        }

        // 檢查條件 3：任意兩數之和不能重複
        if (is_b2) {
            // 使用陣列（雜湊表）來記錄出現過的和，初始皆為 false
            bool sums[MAX_SUM] = {false}; 
            
            for (int i = 0; i < n; i++) {
                for (int j = i; j < n; j++) {
                    int current_sum = b[i] + b[j];
                    
                    if (sums[current_sum]) {
                        is_b2 = false;
                        break;
                    }
                    sums[current_sum] = true;
                }
                if (!is_b2) break;
            }
        }

        // 輸出結果
        if (is_b2) {
            printf("Case #%d: It is a B2-Sequence.\n", case_num);
        } else {
            printf("Case #%d: It is not a B2-Sequence.\n", case_num);
        }
        printf("\n");

        case_num++;
    }

    return 0;
}