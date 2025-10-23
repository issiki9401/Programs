#接收輸入
times = int(input())
nums = []
register = []

for i in range(times):
    value = (input())
    nums.append(value)

last = int(input())

#主程式
for s in range(last+1):
    value = nums[s]
    register.append(value)

print(last)