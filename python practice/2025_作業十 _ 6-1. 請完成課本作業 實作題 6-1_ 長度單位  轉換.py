#詢問轉換功能
print("請選擇您要執行的轉換類型：")
print("1. 英呎 -> 公分")
print("2. 英吋 -> 公分")
print("3. 公分 -> 英呎/英吋")
choice = (input ("請輸入您的選擇 (1, 2 或 3): "))

#預先計算
feet_to_cm = 1/12*2.54

#開始轉換
notice = "notice:輸入不限小數, 輸出將至小數點後2位。"
if choice == '1':
    try:
        print(notice)
        feet = float(input("請輸入英呎數: "))
        print(f"{feet}英呎={feet*feet_to_cm:.2f}公分")
    except ValueError:
        print("輸入無效，請確保您輸入的是數字。")
elif choice == '2':
    try:
        print(notice)
        inches = float(input("請輸入英吋數: "))
        print(f"{inches}英吋={inches*2.54:.2f}公分")
    except ValueError:
        print("輸入無效，請確保您輸入的是數字。")
elif choice == '3':
    try:
        print(notice)
        cm = float(input("請輸入公分數: "))
        print(f"{cm}公分={cm/feet_to_cm:.2f}英呎={cm/2.54:.2f}英吋")
    except ValueError:
        print("輸入無效，請確保您輸入的是數字。")
else:
    print("輸入選擇無效")