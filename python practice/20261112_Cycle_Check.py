import sys

def solve():
    lines = [line.strip() for line in sys.stdin if line.strip()]
    if not lines:
        return

    n = int(lines[0])
    adj = []
    for i in range(1, n + 1):
        # 同時相容「空格隔開」與「無空格連寫」的輸入格式
        row = [int(x) for x in lines[i].split()] if ' ' in lines[i] else [int(c) for c in lines[i]]
        adj.append(row)

    visited = [False] * n

    def has_cycle(curr, parent):
        visited[curr] = True
        for nxt in range(n):
            if adj[curr][nxt] == 1:
                if not visited[nxt]:
                    if has_cycle(nxt, curr):
                        return True
                # 若相鄰節點已拜訪過且非父節點，代表無向圖中存在迴路
                elif nxt != parent:
                    return True
        return False

    # 逐一檢查所有未拜訪節點，避免漏掉非連通圖（Disconnected Graph）中的迴路
    for i in range(n):
        if not visited[i]:
            if has_cycle(i, -1):
                print("True")
                return

    print("False")

if __name__ == "__main__":
    solve()