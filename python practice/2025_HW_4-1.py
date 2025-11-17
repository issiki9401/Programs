import sys

while True:
    try:
        n, m = map(int, input().split())
        if (m < 1) or (n < 1):
            sys.exit()
        A = [1, 1, 2, 4]
        if m == 1:
            print(n * A[1])
        elif m == 2:
            print(n * A[2])
        elif m == 3:
            print(n * A[3])
        else:
            for i in range(4, m + 1):
                A.append(2*A[i - 1] - A[i - 3])
            print(n * A[m])
    except EOFError:
        break