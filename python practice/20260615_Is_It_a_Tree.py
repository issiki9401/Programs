import sys

def solve():
    content = sys.stdin.read().split()
    if not content:
        return
        
    idx = 0
    while idx < len(content):
        n = int(content[idx])
        idx += 1
        
        elements = []
        while idx < len(content) and len(elements) < n * n:
            token = content[idx]
            for char in token:
                if char in '01':
                    elements.append(int(char))
                    if len(elements) == n * n:
                        break
            idx += 1
            
        if len(elements) < n * n:
            break
            
        matrix = []
        for i in range(n):
            matrix.append(elements[i*n : (i+1)*n])
            
        parent = list(range(n))
        
        def find(i):
            root = i
            while parent[root] != root:
                root = parent[root]
            curr = i
            while curr != root:
                nxt = parent[curr]
                parent[curr] = root
                curr = nxt
            return root
            
        has_cycle = False
        components = n
        
        for i in range(n):
            if matrix[i][i] == 1:
                has_cycle = True
            for j in range(i + 1, n):
                if matrix[i][j] == 1 or matrix[j][i] == 1:
                    root_i = find(i)
                    root_j = find(j)
                    if root_i == root_j:
                        has_cycle = True
                    else:
                        parent[root_i] = root_j
                        components -= 1
                        
        if has_cycle:
            print("It is not a tree.")
        elif components == 1:
            print("It is a tree.")
        else:
            print("It is forest.")

if __name__ == '__main__':
    solve()