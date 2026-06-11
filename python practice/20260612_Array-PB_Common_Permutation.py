import sys

def main():
    lines = sys.stdin.read().splitlines()
    for i in range(0, len(lines) - 1, 2):
        a = lines[i]
        b = lines[i + 1]
        ans = []
        for c in sorted(set(a) & set(b)):
            ans.append(c * min(a.count(c), b.count(c)))
        print("".join(ans))

if __name__ == "__main__":
    main()