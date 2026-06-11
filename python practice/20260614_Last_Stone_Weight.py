import sys
import heapq

def solve():
    input_data = sys.stdin.read().split()
    if not input_data:
        return
    
    idx = 0
    while idx < len(input_data):
        n = int(input_data[idx])
        idx += 1
        
        stones = [-int(val) for val in input_data[idx:idx+n]]
        idx += n
        
        heapq.heapify(stones)
        
        while len(stones) > 1:
            y = heapq.heappop(stones)
            x = heapq.heappop(stones)
            
            if x != y:
                heapq.heappush(stones, y - x)
                
        print(-stones[0] if stones else 0)

if __name__ == '__main__':
    solve()