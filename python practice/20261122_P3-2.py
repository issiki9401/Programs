import sys

def solve():
    lines = (line.strip() for line in sys.stdin if line.strip())
    
    for line in lines:
        n = int(line)
        if n == 0:
            break
            
        trace = 0
        for i in range(n):
            row = [int(x.strip()) for x in next(lines).split(',')]
            trace += row[i]
            
        print(trace)

if __name__ == "__main__":
    solve()