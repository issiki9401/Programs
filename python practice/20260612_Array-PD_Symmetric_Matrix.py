import sys
import re

def solve():
    data = sys.stdin.read()
    if not data.strip():
        return
    
    tokens = re.findall(r'N\s*=\s*(\d+)|(-?\d+)', data)
    if not tokens:
        return
        
    T = int(tokens[0][1])
    idx = 1
    
    for t in range(1, T + 1):
        if idx >= len(tokens):
            break
            
        if tokens[idx][0]:
            n = int(tokens[idx][0])
        else:
            n = int(tokens[idx][1])
        idx += 1
        
        matrix = []
        for _ in range(n * n):
            matrix.append(int(tokens[idx][1]))
            idx += 1
            
        is_sym = True
        limit = (n * n) // 2 + 1
        for i in range(limit):
            if matrix[i] < 0 or matrix[n * n - 1 - i] < 0 or matrix[i] != matrix[n * n - 1 - i]:
                is_sym = False
                break
                
        if is_sym:
            print(f"Test #{t}: Symmetric.")
        else:
            print(f"Test #{t}: Non-symmetric.")

if __name__ == '__main__':
    solve()