import sys

#詢問遊戲初始資金(美元)與口袋裡的預算
x, d = map(int, input().split())

#計算結果
if not (0 < x <= 10**9 and 0 < d <= 10**9):
    sys.exit()
elif 2 * x > d:
    print("take it")
else:
    print("double it")
