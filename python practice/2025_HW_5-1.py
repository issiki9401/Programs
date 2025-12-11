import sys

def solve():
    priority = {'+': 1, '-': 1, '*': 2, '/': 2}
    
    for line in sys.stdin:
        s = line.strip()
        if not s: continue
        
        if not set(s).issubset(set("0123456789+-*/() \t")):
            print("error!")
            continue

        temp = s
        for char in "+-*/()":
            temp = temp.replace(char, f" {char} ")
        tokens = temp.split()
        
        stack = []
        output = []
        error = False
        
        for t in tokens:
            if t.isdigit():
                output.append(t)
            elif t == '(':
                stack.append(t)
            elif t == ')':
                while stack and stack[-1] != '(':
                    output.append(stack.pop())
                if not stack:
                    error = True
                    break
                stack.pop()
            elif t in priority:
                while stack and stack[-1] != '(' and priority.get(stack[-1], 0) >= priority[t]:
                    output.append(stack.pop())
                stack.append(t)
        
        while stack:
            if stack[-1] == '(':
                error = True
                break
            output.append(stack.pop())
            
        if error:
            print("error!")
            continue

        counter = 0
        for item in output:
            if item.isdigit():
                counter += 1
            else:
                if counter < 2:
                    error = True
                    break
                counter -= 1
        
        if error or counter != 1:
            print("error!")
        else:
            print("".join(output))

if __name__ == "__main__":
    solve()