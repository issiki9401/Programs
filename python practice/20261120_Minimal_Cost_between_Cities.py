import heapq
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
        # 相容空格分隔與無空格連寫的矩陣格式
        if len(parts) != n:
            parts = re.findall(r'[Xx]|-?\d+(?:\.\d+)?', lines[i + 1])
        for j in range(n):
            if parts[j].upper() != 'X':
                adj[i][j] = float(parts[j])
                
    end_parts = lines[n + 1].split()
    if len(end_parts) != 2:
        end_parts = re.findall(r'\d+', lines[n + 1])
    start, end = int(end_parts[0]), int(end_parts[1])
    
    if start == end:
        print(0)
        return
        
    # Dijkstra 演算法（經過中繼城市加收 200 元過路費）
    dist = [INF] * n
    dist[start] = 0.0
    pq = [(0.0, start)]
    
    while pq:
        d, u = heapq.heappop(pq)
        if d > dist[u]:
            continue
        if u == end:
            break
            
        for v in range(n):
            if adj[u][v] != INF and u != v:
                toll = 200.0 if v != end else 0.0
                new_cost = d + adj[u][v] + toll
                if new_cost < dist[v]:
                    dist[v] = new_cost
                    heapq.heappush(pq, (new_cost, v))
                    
    if dist[end] == INF:
        print("X")
    else:
        print(int(dist[end]))

if __name__ == '__main__':
    solve()