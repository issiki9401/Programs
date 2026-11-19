import math
import sys

def solve():
    s = sys.stdin.read().strip()
    if not s:
        return
    
    n = len(s)
    k = n - 2  # 右子樹的節點數量（B 之後的字母數）
    
    # 計算第 k 個卡塔蘭數 C_k
    ans = math.comb(2 * k, k) // (k + 1)
    print(ans)

if __name__ == '__main__':
    solve()