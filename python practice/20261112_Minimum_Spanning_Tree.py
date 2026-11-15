import sys

def solve():
    lines = [line.strip() for line in sys.stdin if line.strip()]
    if not lines:
        return

    n = int(lines[0])
    adj = []
    for i in range(1, n + 1):
        # 優先以空格分割（支援多位數權重），若無空格則逐字元讀取
        row = [int(x) for x in lines[i].split()] if ' ' in lines[i] else [int(c) for c in lines[i]]
        adj.append(row)

    # Prim 演算法求最小生成樹（MST）總成本
    visited = [False] * n
    min_dist = [float('inf')] * n
    min_dist[0] = 0
    total_cost = 0

    for _ in range(n):
        u = -1
        for i in range(n):
            if not visited[i] and (u == -1 or min_dist[i] < min_dist[u]):
                u = i

        # 若圖不連通，無法形成完整展開樹
        if min_dist[u] == float('inf'):
            break

        visited[u] = True
        total_cost += min_dist[u]

        for v in range(n):
            weight = adj[u][v]
            if not visited[v] and weight > 0 and weight < min_dist[v]:
                min_dist[v] = weight

    print(total_cost)

if __name__ == "__main__":
    solve()