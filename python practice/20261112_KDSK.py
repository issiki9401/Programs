import sys
from itertools import combinations

def solve():
    lines = [line.strip() for line in sys.stdin if line.strip()]
    if len(lines) < 3:
        return

    # 以逗號切分並去除名字前後可能的多餘空白
    students = [s.strip() for s in lines[0].split(',')]
    conflict = [s.strip() for s in lines[1].split(',')]
    besties = [s.strip() for s in lines[2].split(',')]

    c1, c2 = conflict[0], conflict[1]
    b1, b2 = besties[0], besties[1]

    valid_count = 0

    # 枚舉所有「至少 1 人參加」的出席組合 (人數 r = 1 ~ N)
    for r in range(1, len(students) + 1):
        for comb in combinations(students, r):
            group = set(comb)
            # 1. 王不見王：兩位不能同時出現
            if c1 in group and c2 in group:
                continue
            # 2. 后要見后：兩位必須同時出席或同時缺席
            if (b1 in group) != (b2 in group):
                continue
            valid_count += 1

    print(valid_count)

if __name__ == "__main__":
    solve()