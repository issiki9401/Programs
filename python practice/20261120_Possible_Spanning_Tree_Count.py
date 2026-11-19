from fractions import Fraction
import sys

def determinant(matrix):
    n = len(matrix)
    if n == 0:
        return 1
    
    mat = [[Fraction(x) for x in row] for row in matrix]
    det = Fraction(1)
    
    for i in range(n):
        # 尋找主元（pivot）
        pivot = i
        while pivot < n and mat[pivot][i] == 0:
            pivot += 1
        if pivot == n:
            return 0  # 圖不連通，展開樹數量為 0
            
        if pivot != i:
            mat[i], mat[pivot] = mat[pivot], mat[i]
            det = -det
            
        det *= mat[i][i]
        for j in range(i + 1, n):
            factor = mat[j][i] / mat[i][i]
            for k in range(i, n):
                mat[j][k] -= factor * mat[i][k]
                
    return int(det)

def solve():
    lines = [line.strip() for line in sys.stdin.read().splitlines() if line.strip()]
    if not lines:
        return
        
    n = int(lines[0])
    if n == 1:
        print(1)
        return
        
    adj = []
    for i in range(1, n + 1):
        parts = lines[i].split()
        # 相容有空格分隔與無空格連寫的矩陣輸入
        row = [int(x) for x in parts] if len(parts) == n else [int(ch) for ch in lines[i]]
        adj.append(row)
        
    # 建立拉普拉斯矩陣 L = D - A
    laplacian = [[0] * n for _ in range(n)]
    for i in range(n):
        degree = 0
        for j in range(n):
            if i != j and adj[i][j] != 0:
                laplacian[i][j] = -adj[i][j]
                degree += adj[i][j]
        laplacian[i][i] = degree
        
    # 取 (n-1) x (n-1) 餘因子矩陣並計算行列式
    cofactor = [row[:-1] for row in laplacian[:-1]]
    print(determinant(cofactor))

if __name__ == '__main__':
    solve()