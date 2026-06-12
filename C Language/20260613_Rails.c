#include <stdio.h>

int main() {
    int n;
    while (scanf("%d", &n) == 1 && n != 0) {
        while (1) {
            int target[1005];
            scanf("%d", &target[0]);
            if (target[0] == 0) {
                printf("\n");
                break;
            }
            for (int i = 1; i < n; i++) {
                scanf("%d", &target[i]);
            }

            int stack[1005], top = -1;
            int curr = 1, possible = 1, target_idx = 0;

            while (target_idx < n) {
                if (top >= 0 && stack[top] == target[target_idx]) {
                    top--;
                    target_idx++;
                } else if (curr <= n) {
                    stack[++top] = curr++;
                } else {
                    possible = 0;
                    break;
                }
            }

            printf("%s\n", possible ? "Yes" : "No");
        }
    }
    
    return 0;
}