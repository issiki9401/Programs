n = int(input("請輸入一個正整數："))

sum = 0
for i in range(1, n + 1):
    sum += i

print(f"1加到{n}的和={sum}")