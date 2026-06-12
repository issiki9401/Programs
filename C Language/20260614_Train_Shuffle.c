#include <stdio.h>
#include <string.h>

int main() {
    int n;
    char A[1005][50], B[1005][50];
    
    while (scanf("%d", &n) == 1) {
        for (int i = 0; i < n; ++i) scanf("%s", A[i]);
        for (int i = 0; i < n; ++i) scanf("%s", B[i]);

        int left = 0, right = n - 1;
        int possible = 1;
        
        for (int i = n - 1; i >= 0; --i) {
            if (strcmp(B[left], A[i]) == 0) {
                left++;
            } else if (strcmp(B[right], A[i]) == 0) {
                right--;
            } else {
                possible = 0;
                break;
            }
        }
        
        if (possible) {
            printf("Success\n");
        } else {
            printf("Fail\n");
        }
    }
    return 0;
}