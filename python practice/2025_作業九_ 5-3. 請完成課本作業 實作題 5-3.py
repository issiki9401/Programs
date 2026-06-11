#請定義a,b,c
a = int(input ("請定義a="))
b = int(input ("請定義b="))
c = int(input ("請定義c="))

#計算b2-4ac值
q = b**2-4*a*c
print(f"b2-4ac={q}")

#判斷是否有實數解
print("判斷是否有實數解...")
if q < 0:
    print("Your equation has no root")
elif q == 0:
    print(f"output={(-b / (2 * a)):.2f}")
else:
    sqrt_q = q ** 0.5
    q1 = (-b + sqrt_q) / (2 * a)
    q2 = (-b - sqrt_q) / (2 * a)
    print(f"output={q1:.2f},{q2:.2f}")
    print(f"q1+q2={q1+q2:.2f}")