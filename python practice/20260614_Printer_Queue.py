import sys
from collections import deque

def main():
    input_data = sys.stdin.read().split()
    if not input_data:
        return
    
    t = int(input_data[0])
    idx = 1
    
    for _ in range(t):
        n = int(input_data[idx])
        m = int(input_data[idx+1])
        idx += 2
        
        queue = deque([(int(input_data[idx+i]), i == m) for i in range(n)])
        idx += n
        
        time = 0
        while queue:
            current = queue.popleft()
            if any(current[0] < item[0] for item in queue):
                queue.append(current)
            else:
                time += 1
                if current[1]:
                    print(time)
                    break

if __name__ == '__main__':
    main()