import sys

def solve():
    input_data = sys.stdin.read().split()
    if not input_data:
        return
    iterator = iter(input_data)
    
    while True:
        try:
            n_str = next(iterator)
        except StopIteration:
            break
        n = int(n_str)
        if n == 0:
            break
        
        while True:
            first_val = int(next(iterator))
            if first_val == 0:
                print()
                break
            
            target = [first_val]
            for _ in range(n - 1):
                target.append(int(next(iterator)))
            
            stack = []
            current = 1
            possible = True
            
            for coach in target:
                while current <= coach:
                    stack.append(current)
                    current += 1
                if stack and stack[-1] == coach:
                    stack.pop()
                else:
                    possible = False
                    break
            
            if possible and not stack:
                print("Yes")
            else:
                print("No")

if __name__ == '__main__':
    solve()