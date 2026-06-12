#include <stdio.h>
#include <string.h>

#define MAX_NODES 100005

int parent[MAX_NODES];
int path[MAX_NODES];

// 讀取有效行（跳過空白行）
char* get_valid_line(char* buf, int size) {
    while (fgets(buf, size, stdin)) {
        for (int i = 0; buf[i]; i++) {
            if (buf[i] > ' ') return buf;
        }
    }
    return NULL;
}

int main() {
    char line[8192];
    while (get_valid_line(line, sizeof(line))) {
        int n, x, y;
        if (sscanf(line, "%d %d %d", &n, &x, &y) != 3) continue;
        
        memset(parent, -1, sizeof(parent));
        memset(path, 0, sizeof(path));
        
        for (int i = 0; i < n; i++) {
            if (!get_valid_line(line, sizeof(line))) break;
            int u, v, offset;
            char *ptr = line;
            if (sscanf(ptr, "%d%n", &u, &offset) != 1) continue;
            ptr += offset;
            while (sscanf(ptr, "%d%n", &v, &offset) == 1) {
                parent[v] = u;
                ptr += offset;
            }
        }
        
        int curr = x;
        path[curr] = 1;
        while (parent[curr] != -1) {
            curr = parent[curr];
            path[curr] = 1;
        }
        
        curr = y;
        while (!path[curr]) {
            curr = parent[curr];
        }
        printf("%d\n", curr);
    }
    return 0;
}