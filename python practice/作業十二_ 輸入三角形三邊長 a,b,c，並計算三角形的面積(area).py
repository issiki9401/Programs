#詢問三角形三邊長
print("請分別輸入三角形的三邊長：")
while True:
    try:
        a = int(input("請輸入a邊長："))
        b = int(input("請輸入b邊長："))
        c = int(input("請輸入c邊長："))
        break
    except ValueError:
        print("輸入無效，請確保您輸入的是數字。")

#計算面積
s = (a+b+c)/2
area = (s*(s-a)*(s-b)*(s-c))**0.5
if isinstance(area, complex) or (isinstance(area, (int, float)) and area <= 0):
    print("輸入的邊長無法成為三角形，請檢查輸入是否正確。")
else:
    print(f"三角形的面積為：{area:.2f}")