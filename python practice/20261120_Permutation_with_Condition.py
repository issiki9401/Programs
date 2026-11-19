import math
import sys

def solve():
    # 讀取輸入並去除前後空白與換行符號
    s = sys.stdin.read().strip()
    if not s:
        return
    
    n = len(s)
    # 使用整數除法 // 避免大數（如 n=26）產生浮點數誤差
    ans = math.factorial(n) // 2
    print(ans)

if __name__ == '__main__':
    solve()