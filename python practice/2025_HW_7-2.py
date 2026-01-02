import sys
for line in sys.stdin:
    nums = list(map(int, line.strip().split()))
    if not nums:
        continue
    max_sum = current_sum = nums[0]
    max_len = current_len = 1
    for num in nums[1:]:
        if current_sum + num >= num:
            current_sum += num
            current_len += 1
        else:
            current_sum = num
            current_len = 1
        if current_sum > max_sum:
            max_sum = current_sum
            max_len = current_len
        elif current_sum == max_sum:
            if current_len > max_len:
                max_len = current_len
    print(max_len, max_sum)