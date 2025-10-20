import sys

for line in sys.stdin:
    num = int(line.strip())
    count_ones = bin(num).count('1')
    print(count_ones)