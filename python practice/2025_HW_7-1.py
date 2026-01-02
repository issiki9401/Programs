class TreeNode:
    def __init__(self, val):
        self.val = val
        self.left = None
        self.right = None

def insert(root, val):
    if root is None:
        return TreeNode(val)
    if val < root.val:
        root.left = insert(root.left, val)
    else:
        root.right = insert(root.right, val)
    return root

def find_all_paths(root, path, all_paths):
    if root is None:
        return
    
    # 加入當前節點到路徑中
    current_path = path + [root.val]
    
    # 如果是葉子節點，存入路徑清單
    if root.left is None and root.right is None:
        all_paths.append(current_path)
    else:
        find_all_paths(root.left, current_path, all_paths)
        find_all_paths(root.right, current_path, all_paths)

def solve():
    try:
        # 讀入一序列的正整數數字 [cite: 16, 20]
        data = list(map(int, input().split()))
        if not data:
            return

        # 建立二元搜尋樹 
        root = None
        for val in data:
            root = insert(root, val)

        # 找出所有從根到葉子的路徑
        all_paths = []
        find_all_paths(root, [], all_paths)

        # 找出最長路徑的長度
        max_len = max(len(p) for p in all_paths)

        # 列印出所有最長路徑的節點 [cite: 2, 11]
        for p in all_paths:
            if len(p) == max_len:
                print(" ".join(map(str, p)))

    except EOFError:
        pass

if __name__ == "__main__":
    solve()