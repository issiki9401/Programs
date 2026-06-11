import sys

# 讀取所有標準輸入，並自動依換行或空白分割成陣列
inputs = sys.stdin.read().split()

# 遍歷所有輸入的數字，每有一個數字就印出一次 Hello World
for _ in inputs:
    print("Hello World")