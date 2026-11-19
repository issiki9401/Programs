import re
import sys

def solve():
    expr = sys.stdin.read().strip()
    if not expr:
        return
    
    # 將運算式中的所有數字運算元加 1
    modified_expr = re.sub(r'\d+', lambda m: str(int(m.group(0)) + 1), expr)
    
    # 計算結果（Python 3 的 / 預設為浮點數除法）並無條件捨去至整數位
    result = int(eval(modified_expr))
    print(result)

if __name__ == '__main__':
    solve()