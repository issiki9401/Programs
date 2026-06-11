import sys

def is_balanced(s):
    stack = []
    mapping = {')': '(', ']': '['}
    for char in s:
        if char in "([":
            stack.append(char)
        elif char in ")]":
            if not stack or stack.pop() != mapping[char]:
                return False
    return not stack

def main():
    input_data = sys.stdin.read().splitlines()
    if not input_data:
        return
    n = int(input_data[0].strip())
    for i in range(1, n + 1):
        s = input_data[i] if i < len(input_data) else ""
        if is_balanced(s):
            print("Yes")
        else:
            print("No")

if __name__ == '__main__':
    main()