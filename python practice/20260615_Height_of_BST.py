import sys

class Node:
    def __init__(self, val):
        self.val = val
        self.left = None
        self.right = None

def insert(root, val):
    if not root:
        return Node(val)
    if val < root.val:
        root.left = insert(root.left, val)
    else:
        root.right = insert(root.right, val)
    return root

def height(root):
    if not root:
        return 0
    return 1 + max(height(root.left), height(root.right))

def solve():
    data = sys.stdin.read().split()
    idx = 0
    while idx < len(data):
        n = int(data[idx])
        idx += 1
        root = None
        for _ in range(n):
            root = insert(root, int(data[idx]))
            idx += 1
        print(height(root))

if __name__ == '__main__':
    solve()