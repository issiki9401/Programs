import sys
for line in sys.stdin:
    if line.strip():
        print(int(eval(line.strip().replace('^', '**'))))