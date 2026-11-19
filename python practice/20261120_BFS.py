from collections import deque
import re
import sys

class Node:
    def __init__(self, val):
        self.val = val
        self.left = None
        self.right = None

def insert(root, val):
    if not root:
        return Node(val)
    curr = root
    while True:
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
    return root

def solve():
    line = sys.stdin.read().strip()
    if not line:
        return
    
    # 提取所有整數（相容負號黏連的格式）
    nums = list(map(int, re.findall(r'-?\d+', line)))
    
    # 依前序序列建立二元搜尋樹
    root = None
    for num in nums:
        root = insert(root, num)
        
    # 廣度優先搜尋 (BFS)
    res = []
    queue = deque([root])
    while queue:
        node = queue.popleft()
        res.append(str(node.val))
        if node.left:
            queue.append(node.left)
        if node.right:
            queue.append(node.right)
            
    print(" ".join(res))

if __name__ == '__main__':
    solve()