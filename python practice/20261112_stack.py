import sys

def is_valid_stack_permutation(target: str) -> bool:
    stack = []
    idx = 0
    n = len(target)
    
    # 依序將 'A', 'B', 'C'... (共 n 個字母) 推入堆疊
    for i in range(n):
        ch = chr(ord('A') + i)
        stack.append(ch)
        
        # 當堆疊頂端與目標序列當前字元相符時，持續彈出 (pop)
        while stack and idx < n and stack[-1] == target[idx]:
            stack.pop()
            idx += 1
            
    # 若最後堆疊清空且目標序列全數比對完成，即為合法的堆疊排列
    return len(stack) == 0 and idx == n

def solve():
    for line in sys.stdin:
        target = line.strip()
        if target:
            print(is_valid_stack_permutation(target))

if __name__ == "__main__":
    solve()