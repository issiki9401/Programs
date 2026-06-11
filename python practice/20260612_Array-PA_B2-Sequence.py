import sys

def solve():
    input_data = sys.stdin.read().split()
    if not input_data:
        return
    
    iterator = iter(input_data)
    case_num = 1
    
    while True:
        try:
            n_str = next(iterator)
        except StopIteration:
            break
            
        n = int(n_str)
        b = [int(next(iterator)) for _ in range(n)]
        
        is_b2 = True
        
        if b[0] < 1:
            is_b2 = False
            
        for i in range(1, n):
            if b[i] <= b[i-1]:
                is_b2 = False
                break
        
        if is_b2:
            sums = set()
            for i in range(n):
                for j in range(i, n):
                    current_sum = b[i] + b[j]
                    if current_sum in sums:
                        is_b2 = False
                        break
                    sums.add(current_sum)
                if not is_b2:
                    break
        
        if is_b2:
            print(f"Case #{case_num}: It is a B2-Sequence.")
        else:
            print(f"Case #{case_num}: It is not a B2-Sequence.")
        print()
        
        case_num += 1

if __name__ == '__main__':
    solve()