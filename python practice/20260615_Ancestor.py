import sys

def main():
    lines = sys.stdin.read().splitlines()
    idx = 0
    while idx < len(lines):
        if not lines[idx].strip():
            idx += 1
            continue
        n, x, y = map(int, lines[idx].split())
        idx += 1
        parent = {}
        for _ in range(n):
            while idx < len(lines) and not lines[idx].strip():
                idx += 1
            row = list(map(int, lines[idx].split()))
            idx += 1
            u = row[0]
            for v in row[1:]:
                parent[v] = u
        
        path = set()
        curr = x
        path.add(curr)
        while curr in parent:
            curr = parent[curr]
            path.add(curr)
            
        curr = y
        while curr not in path:
            curr = parent[curr]
        print(curr)

if __name__ == '__main__':
    main()