#include <stdio.h>
#include <string.h>

char stack[50000][20];
int top = -1;

int main() {
    int n;
    if (scanf("%d", &n) != 1) return 0;

    char op[10], name[20];
    for (int i = 0; i < n; i++) {
        scanf("%s", op);
        if (strcmp(op, "Sleep") == 0) {
            scanf("%s", name);
            strcpy(stack[++top], name);
        } else if (strcmp(op, "Kick") == 0) {
            if (top >= 0) top--;
        } else if (strcmp(op, "Test") == 0) {
            if (top >= 0) printf("%s\n", stack[top]);
            else printf("Not in a dream\n");
        }
    }
    
    return 0;
}