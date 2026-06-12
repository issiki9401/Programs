#include <stdio.h>
#include <stdbool.h>

#define MAX_SIZE 10005 // N 最大通常不超過 100，N*N 大約 10000 左右，可依題目需求調整

long long matrix[MAX_SIZE]; // 元素可能較大，使用 long long 較安全

int main() {
    int T;
    // 讀取測試資料筆數
    if (scanf("%d", &T) != 1) return 0;

    for (int t = 1; t <= T; t++) {
        char dummy[10];
        int n;

        // 讀取 "N = %d" 格式。
        // scanf 會自動跳過空白，並將 "N"、"=" 以及數字分開解析
        scanf("%s %s %d", dummy, dummy, &n);

        int total_elements = n * n;
        for (int i = 0; i < total_elements; i++) {
            scanf("%lld", &matrix[i]);
        }

        bool is_sym = true;
        
        // 檢查對稱性與非負條件
        for (int i = 0; i < total_elements; i++) {
            int target_idx = total_elements - 1 - i;
            
            // 條件 1：不能為負數
            // 條件 2：首尾對應位置必須相等
            if (matrix[i] < 0 || matrix[i] != matrix[target_idx]) {
                is_sym = false;
                break;
            }
        }

        // 輸出結果
        if (is_sym) {
            printf("Test #%d: Symmetric.\n", t);
        } else {
            printf("Test #%d: Non-symmetric.\n", t);
        }
    }

    return 0;
}