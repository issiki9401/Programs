import sys

def solve():
    data = sys.stdin.read().split()
    if not data:
        return
    
    queries = [int(x) for x in data]
    max_n = max(queries)
    
    if max_n < 2:
        print('\n'.join('0' for _ in queries))
        return

    is_prime = bytearray([1]) * (max_n + 1)
    is_prime[0] = is_prime[1] = 0
    
    for i in range(2, int(max_n**0.5) + 1):
        if is_prime[i]:
            is_prime[i * i : max_n + 1 : i] = bytearray(len(range(i * i, max_n + 1, i)))
            
    prefix = [0] * (max_n + 1)
    count = 0
    for i in range(2, max_n + 1):
        count += is_prime[i]
        prefix[i] = count
        
    sys.stdout.write('\n'.join(str(prefix[n]) for n in queries) + '\n')

if __name__ == "__main__":
    solve()