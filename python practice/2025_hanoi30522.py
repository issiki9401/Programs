def hanoi(n, A, B, C):
    if n == 1:
        print(f"{A}->{C}")
    else:
        hanoi(n-1, A, C, B) 
        hanoi(1, A, B, C) 
        hanoi(n-1, B, A, C)

n = int(input("請輸入想要運算的河內塔圓盤數量："))
print("將要運算的圓盤數量為"+str(n))
hanoi(int(n), 'A', 'B', 'C')