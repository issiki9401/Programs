#詢問所得
while True:
    try:
        income = int(input("請輸入您的所得："))
        break
    except ValueError:
        print("輸入無效，請確保您輸入的是數字。")

#計算稅額
notice = "notice:輸出結果將四捨五入至整數。"
if income <= 300000:
    print(notice)
    print(f"您的稅額為 {round(income * 0.06)} 元")
elif income <= 800000:
    print(notice)
    print(f"您的稅額為 {round(income * 0.13)} 元")
elif income <= 1500000:
    print(notice)
    print(f"您的稅額為 {round(income * 0.21)} 元")
elif income <= 3000000:
    print(notice)
    print(f"您的稅額為 {round(income * 0.30)} 元")
else:
    print(notice)
    print(f"您的稅額為 {round(income * 0.40)} 元")
#省略了大於的計算過程，繼承elif結果