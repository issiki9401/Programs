import sys

def solve():
    input_data = sys.stdin.read().split()
    if not input_data:
        return
    
    idx = 0
    n_elements = len(input_data)
    
    while idx < n_elements:
        n = int(input_data[idx])
        idx += 1
        
        A = input_data[idx : idx + n]
        idx += n
        
        B = input_data[idx : idx + n]
        idx += n
        
        left = 0
        right = n - 1
        possible = True
        
        for i in range(n - 1, -1, -1):
            if B[left] == A[i]:
                left += 1
            elif B[right] == A[i]:
                right -= 1
            else:
                possible = False
                break
        
        if possible:
            sys.stdout.write("Success\n")
        else:
            sys.stdout.write("Fail\n")

if __name__ == '__main__':
    solve()