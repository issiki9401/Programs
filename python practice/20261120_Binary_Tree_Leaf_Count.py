import sys

def count_leaves(preorder: str, inorder: str) -> int:
    if not preorder:
        return 0
    # 僅剩單一節點，代表左右子樹皆為空，即為 leaf
    if len(preorder) == 1:
        return 1
    
    root = preorder[0]
    idx = inorder.index(root)
    
    left_in = inorder[:idx]
    right_in = inorder[idx + 1:]
    
    left_pre = preorder[1 : 1 + len(left_in)]
    right_pre = preorder[1 + len(left_in) :]
    
    return count_leaves(left_pre, left_in) + count_leaves(right_pre, right_in)

def solve():
    lines = sys.stdin.read().splitlines()
    if len(lines) < 2:
        return
    
    preorder = lines[0].strip()
    inorder = lines[1].strip()
    print(count_leaves(preorder, inorder))

if __name__ == '__main__':
    solve()