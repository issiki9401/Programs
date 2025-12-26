from collections import deque
import sys

def main():
    input = sys.stdin.read().split()
    idx = 0
    N = int(input[idx])
    idx += 1
    M = int(input[idx])
    idx += 1
    grid = []
    for _ in range(N):
        row = list(map(int, input[idx:idx+M]))
        idx += M
        grid.append(row)
    
    visited = [[False]*M for _ in range(N)]
    dirs = [(-1,0), (1,0), (0,-1), (0,1)]
    sizes = []
    
    for i in range(N):
        for j in range(M):
            if grid[i][j] == 1 and not visited[i][j]:
                q = deque()
                q.append((i,j))
                visited[i][j] = True
                count = 0
                while q:
                    x, y = q.popleft()
                    count += 1
                    for dx, dy in dirs:
                        nx = x + dx
                        ny = y + dy
                        if 0 <= nx < N and 0 <= ny < M:
                            if grid[nx][ny] == 1 and not visited[nx][ny]:
                                visited[nx][ny] = True
                                q.append((nx, ny))
                sizes.append(count)
    
    sizes.sort(reverse=True)
    for s in sizes:
        print(s)

if __name__ == "__main__":
    main()