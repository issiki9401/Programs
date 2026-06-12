#include <stdio.h>
#include <string.h>

int main() {
    int n;
    if (scanf("%d\n", &n) != 1) return 0;

    char line[200];
    while (n--) {
        fgets(line, sizeof(line), stdin);
        int len = strlen(line);
        if (len > 0 && line[len - 1] == '\n') line[len - 1] = '\0';

        char stack[200];
        int top = -1;
        int balanced = 1;

        for (int i = 0; line[i] != '\0'; i++) {
            if (line[i] == '(' || line[i] == '[') {
                stack[++top] = line[i];
            } else if (line[i] == ')') {
                if (top < 0 || stack[top--] != '(') { balanced = 0; break; }
            } else if (line[i] == ']') {
                if (top < 0 || stack[top--] != '[') { balanced = 0; break; }
            }
        }
        if (top >= 0) balanced = 0;

        printf("%s\n", balanced ? "Yes" : "No");
    }
    
    return 0;
}