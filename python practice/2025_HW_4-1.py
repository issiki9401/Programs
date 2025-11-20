def calculate_g(m):
    if m == 1:
        return 1
    elif m == 2:
        return 2
    elif m == 3:
        return 4
    elif m == 4:
        return 7
    else:
        d1, d2, d3 = 1, 2, 3  # 對應m=2,3,4時的差值
        g = 7  # m=4時的數量
        for k in range(5, m + 1):
            dk = d1 + d2 + d3
            g += dk
            # 更新差值，為下一輪迭代做準備
            d1, d2, d3 = d2, d3, dk
        return g

import sys

# 讀取所有輸入行
for line in sys.stdin:
    line = line.strip()
    if not line:
        continue
    n, m = map(int, line.split())
    total = n * calculate_g(m)
    print(total)