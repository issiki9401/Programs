import sys

#詢問次數
t = int(input())
if not (1 <= t <= 10):
    sys.exit()
x = 1
 
#質數與雙生判斷
def prime(a, b):
    if abs(a - b) != 2:
        print("N")
        return
    if a < 2 or b < 2:
        print("N")
        return
    for i in range(2, a):
        if a % i == 0:
            print("N")
            return
    for i in range(2, b):
        if b % i == 0:
            print("N")
            return
    print("Y")

#主程式
while x <= t:
    a, b = map(int, input().split())
    if not (1 <= a <= 10**6 and 1 <= b <= 10**6):
        sys.exit()
    prime(a, b)
    x += 1