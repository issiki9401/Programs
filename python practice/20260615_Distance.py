import sys
from collections import defaultdict, deque

def main():
    lines = sys.stdin.read().splitlines()
    idx = 0
    while idx < len(lines):
        if not lines[idx].strip():
            idx += 1
            continue
        n = int(lines[idx].strip())
        idx += 1
        
        while idx < len(lines) and not lines[idx].strip():
            idx += 1
        x, y = map(int, lines[idx].split())
        idx += 1
        
        adj = defaultdict(list)
        for _ in range(n):
            while idx < len(lines) and not lines[idx].strip():
                idx += 1
            parts = list(map(int, lines[idx].split()))
            idx += 1
            if parts:
                u = parts[0]
                for v in parts[1:]:
                    adj[u].append(v)
                    adj[v].append(u)
                    
        q = deque([(x, 0)])
        visited = {x}
        while q:
            curr, dist = q.popleft()
            if curr == y:
                print(dist)
                break
            for nxt in adj[curr]:
                if nxt not in visited:
                    visited.add(nxt)
                    q.append((nxt, dist + 1))

if __name__ == '__main__':
    main()