import re
import sys

class Node:
    def __init__(self, val):
        self.val = val
        self.left = None
        self.right = None

def insert(root, val):
    if not root:
        return Node(val), 1
    
    curr = root
    depth = 1
    while True:
        depth += 1
        if val < curr.val:
            if curr.left is None:
                curr.left = Node(val)
                break
            curr = curr.left
        else:
            if curr.right is None:
                curr.right = Node(val)
                break
            curr = curr.right
            
    return root, depth

def solve():
    line = sys.stdin.read().strip()
    if not line:
        return
    
    # 提取所有整數（支援負數與空白分隔）
    nums = list(map(int, re.findall(r'-?\d+', line)))
    
    root = None
    max_height = 0
    for num in nums:
        root, depth = insert(root, num)
        if depth > max_height:
            max_height = depth
            
    print(max_height)

if __name__ == '__main__':
    solve()