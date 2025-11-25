#include <stdio.h>

int main() {
    int score;
    
    printf("請輸入學生成績（0-100）：\n");
    scanf("%d", &score);
    
    if (score < 0 || score > 100) {
        printf("輸入無效！成績應在 0-100 之间\n");
    } else if (score >= 90) {
        printf("等級：A\n");
    } else if (score >= 80) {
        printf("等級：B\n");
    } else if (score >= 70) {
        printf("等級：C\n");
    } else if (score >= 60) {
        printf("等級：D\n");
    } else {
        printf("等級：F\n");
    }
    
    return 0;
}