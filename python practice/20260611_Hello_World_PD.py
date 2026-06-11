import sys
import math

lines = sys.stdin.read().splitlines()
is_first = True

for line in lines:
    if not line.strip():
        continue
    if not is_first:
        print()
    is_first = False
    
    p = math.prod(map(int, line.split()))
    for _ in range(p):
        print("Hello World")