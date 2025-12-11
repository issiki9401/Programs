precedence = {'+': 1, '-': 1, '*': 2, '/': 2}
import sys

for line in sys.stdin:
    line = line.strip()
    if not line:
        print("error!")
        continue
    tokens = line.split()
    output = []
    stack = []
    error = False
    for token in tokens:
        if token.isdigit():
            output.append(token)
        elif token == '(':
            stack.append(token)
        elif token == ')':
            while stack and stack[-1] != '(':
                output.append(stack.pop())
            if not stack:
                error = True
                break
            stack.pop()
        elif token in precedence:
            while stack and stack[-1] != '(' and precedence[stack[-1]] >= precedence[token]:
                output.append(stack.pop())
            stack.append(token)
        else:
            error = True
            break
    if not error:
        while stack:
            top = stack.pop()
            if top == '(':
                error = True
                break
            output.append(top)
    if not error:
        operands = 0
        operators = 0
        for item in output:
            if item.isdigit():
                operands += 1
            elif item in precedence:
                operators += 1
        if operands != operators + 1 or operands == 0:
            error = True
    print(' '.join(output) if not error else 'error!')