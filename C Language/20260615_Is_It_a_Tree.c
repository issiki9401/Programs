#include <stdio.h>

#define MAX_N 2005

int parent[MAX_N];
char matrix[MAX_N][MAX_N];

int find(int i) {
    int root = i;
    while (parent[root] != root) {
        root = parent[root];
    }
    int curr = i;
    while (curr != root) {
        int nxt = parent[curr];
        parent[curr] = root;
        curr = nxt;
    }
    return root;
}

int main() {
    int n;
    while (scanf("%d", &n) == 1) {
        int count = 0;
        // 逐字元讀取，忽略空白、換行，只收集 '0' 與 '1'
        while (count < n * n) {
            char c = getchar();
            if (c == EOF) break;
            if (c == '0' || c == '1') {
                matrix[count / n][count % n] = c - '0';
                count++;
            }
        }
        
        for (int i = 0; i < n; i++) {
            parent[i] = i;
        }
        
        int has_cycle = 0;
        int components = n;
        
        for (int i = 0; i < n; i++) {
            if (matrix[i][i] == 1) has_cycle = 1;
            for (int j = i + 1; j < n; j++) {
                if (matrix[i][j] == 1 || matrix[j][i] == 1) {
                    int root_i = find(i);
                    int root_j = find(j);
                    if (root_i == root_j) {
                        has_cycle = 1;
                    } else {
                        parent[root_i] = root_j;
                        components--;
                    }
                }
            }
        }
        
        if (has_cycle) {
            printf("It is not a tree.\n");
        } else if (components == 1) {
            printf("It is a tree.\n");
        } else {
            printf("It is forest.\n");
        }
    }
    return 0;
}