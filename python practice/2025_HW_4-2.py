import sys
def f(n, m):
    def F(m):
        if m == 0:
            return 1
        elif m == 1:
            return 1
        elif m == 2:
            return 2
        elif m == 3:
            return 4
        else:
            return 2*F(m-1) - F(m-3)
    return n * F(m)

while True:
    try:
        n, m = map(int, input().split())
        if (m<1)|(n<1) :
            sys.exit()
        print(f(n, m))
    except EOFError:
        break