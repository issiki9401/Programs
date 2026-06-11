import sys

def main():
    input_data = sys.stdin.read().split()
    if not input_data:
        return
    
    n = int(input_data[0])
    idx = 1
    stack = []
    
    for _ in range(n):
        if idx >= len(input_data):
            break
        op = input_data[idx]
        if op == "Sleep":
            stack.append(input_data[idx+1])
            idx += 2
        elif op == "Kick":
            if stack:
                stack.pop()
            idx += 1
        elif op == "Test":
            print(stack[-1] if stack else "Not in a dream")
            idx += 1

if __name__ == '__main__':
    main()