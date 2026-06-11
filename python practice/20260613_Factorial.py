import sys

MOD = 10**9 + 7
MAX_N = 1000
fact = [1] * (MAX_N + 1)

for i in range(1, MAX_N + 1):
    fact[i] = (fact[i - 1] * i) % MOD

def main():
    input_data = sys.stdin.read().split()
    for s in input_data:
        print(fact[int(s)])

if __name__ == '__main__':
    main()