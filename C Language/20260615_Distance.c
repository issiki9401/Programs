#include <stdio.h>
#include <string.h>

#define MAX_NODES 100005

// 使用陣列實作的 Adjacency List 以加快速度
int head[MAX_NODES], to[MAX_NODES * 2], nxt[MAX_NODES * 2], edge_cnt;
int visited[MAX_NODES], q[MAX_NODES], dist[MAX_NODES];

void add_edge(int u, int v) {
    to[++edge_cnt] = v;
    nxt[edge_cnt] = head[u];
    head[u] = edge_cnt;
}

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
        sscanf(line, "%d", &n);
        
        get_valid_line(line, sizeof(line));
        sscanf(line, "%d %d", &x, &y);
        
        memset(head, 0, sizeof(head));
        memset(visited, 0, sizeof(visited));
        edge_cnt = 0;
        
        for (int i = 0; i < n; i++) {
            if (!get_valid_line(line, sizeof(line))) break;
            int u, v, offset;
            char *ptr = line;
            if (sscanf(ptr, "%d%n", &u, &offset) != 1) continue;
            ptr += offset;
            while (sscanf(ptr, "%d%n", &v, &offset) == 1) {
                add_edge(u, v);
                add_edge(v, u);
                ptr += offset;
            }
        }
        
        int front = 0, tail = 0;
        q[tail++] = x;
        visited[x] = 1;
        dist[x] = 0;
        
        while (front < tail) {
            int curr = q[front++];
            if (curr == y) {
                printf("%d\n", dist[curr]);
                break;
            }
            for (int e = head[curr]; e; e = nxt[e]) {
                int nxt_node = to[e];
                if (!visited[nxt_node]) {
                    visited[nxt_node] = 1;
                    dist[nxt_node] = dist[curr] + 1;
                    q[tail++] = nxt_node;
                }
            }
        }
    }
    return 0;
}