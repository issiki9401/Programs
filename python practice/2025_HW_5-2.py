import sys

for line in sys.stdin.read().splitlines():
    tokens = line.split()
    stack = []
    for token in tokens:
        if token in '+-*/':
            a = stack.pop()
            b = stack.pop()
            stack.append(f"{token} {b} {a}")
        else:
            stack.append(token)
    print(stack[0])