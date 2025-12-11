import sys
import re

def get_precedence(op):
    if op in ('*', '/'):
        return 2
    elif op in ('+', '-'):
        return 1
    return 0

def infix_to_rpn(expression):
    if re.search(r'[^\d+\-*/() \t]', expression):
        return "error!"

    tokens = re.findall(r'\d+|[+\-*/()]', expression)
    
    if not tokens:
        return "error!"

    stack = []
    output = []
    
    for token in tokens:
        if token.isdigit():
            output.append(token)
        elif token == '(':
            stack.append(token)
        elif token == ')':
            found_left = False
            while stack:
                top = stack.pop()
                if top == '(':
                    found_left = True
                    break
                output.append(top)
            if not found_left:
                return "error!"
        elif token in ('+', '-', '*', '/'):
            while stack and stack[-1] != '(' and \
                  get_precedence(stack[-1]) >= get_precedence(token):
                output.append(stack.pop())
            stack.append(token)

    while stack:
        top = stack.pop()
        if top == '(':
            return "error!"
        output.append(top)

    val_stack_depth = 0
    for token in output:
        if token.isdigit():
            val_stack_depth += 1
        else:
            if val_stack_depth < 2:
                return "error!"
            val_stack_depth -= 1
            
    if val_stack_depth != 1:
        return "error!"

    return "".join(output)

def main():
    lines = sys.stdin.read().splitlines()
    
    for line in lines:
        line = line.strip()
        if not line:
            continue
        result = infix_to_rpn(line)
        print(result)

if __name__ == "__main__":
    main()