def f(n, m):
    # Base cases
    if m == 1:
        return n
    elif m == 2:
        return 2 * n
    elif m == 3:
        return 4 * n
    else:
        return f(n, m-1) + f(n, m-2) + f(n, m-3)

import sys

for line in sys.stdin:
    line = line.strip()
    if not line:
        continue
    n, m = map(int, line.split())
    print(f(n, m))