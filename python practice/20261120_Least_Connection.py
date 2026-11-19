from collections import deque
import sys

def solve():
    lines = [line.strip() for line in sys.stdin.read().splitlines() if line.strip()]
    if not lines:
        return
    
    n = int(lines[0])
    adj = []
    for i in range(1, n + 1):
        parts = lines[i].split()
        # 相容有空格分隔（如 "0 1 1 1 0"）與無空格（如 "01110"）的輸入
        row = [int(x) for x in parts] if len(parts) == n else [int(ch) for ch in lines[i]]
        adj.append(row)
        
    start, end = map(int, lines[n + 1].split())
    if start == end:
        print(0)
        return
    
    visited = [False] * n
    visited[start] = True
    queue = deque([(start, 0)])
    
    while queue:
        curr, dist = queue.popleft()
        for nxt in range(n):
            if adj[curr][nxt] == 1 and not visited[nxt]:
                if nxt == end:
                    print(dist + 1)
                    return
                visited[nxt] = True
                queue.append((nxt, dist + 1))
                
    print(-1)

if __name__ == '__main__':
    solve()