import sys

def solve():
    lines = [line.strip() for line in sys.stdin if line.strip()]
    if not lines:
        return
    
    trace = 0
    for i, line in enumerate(lines):
        row = [int(x.strip()) for x in line.split(',')]
        trace += row[i]
        
    print(trace)

if __name__ == "__main__":
    solve()