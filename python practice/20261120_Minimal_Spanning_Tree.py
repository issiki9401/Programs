import re
import sys

def solve():
    lines = [line.strip() for line in sys.stdin.read().splitlines() if line.strip()]
    if not lines:
        return
        
    n = int(lines[0])
    INF = float('inf')
    adj = [[INF] * n for _ in range(n)]
    
    for i in range(n):
        parts = lines[i + 1].split()
        # 相容標準空格分隔，以及無空格連寫的測試資料格式
        if len(parts) != n:
            parts = re.findall(r'[Xx]|-?\d+(?:\.\d+)?', lines[i + 1])
        for j in range(n):
            if parts[j].upper() != 'X':
                adj[i][j] = float(parts[j])
                
    # Prim 演算法求最小成本展開樹
    visited = [False] * n
    min_edge = [INF] * n
    min_edge[0] = 0.0
    total_cost = 0.0
    
    for _ in range(n):
        u = -1
        for v in range(n):
            if not visited[v] and (u == -1 or min_edge[v] < min_edge[u]):
                u = v
                
        if min_edge[u] == INF:
            break
            
        visited[u] = True
        total_cost += min_edge[u]
        
        for v in range(n):
            if not visited[v] and adj[u][v] < min_edge[v]:
                min_edge[v] = adj[u][v]
                
    print(int(total_cost))

if __name__ == '__main__':
    solve()