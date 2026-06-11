import sys

for line in sys.stdin:
    data = list(map(int, line.split()))
    if not data:
        continue
    
    n = data[0]
    seq = data[1:]
    
    if n == 1:
        print("Jolly")
        continue
        
    diffs = {abs(seq[i] - seq[i+1]) for i in range(n - 1)}
    
    if all(i in diffs for i in range(1, n)):
        print("Jolly")
    else:
        print("Not jolly")