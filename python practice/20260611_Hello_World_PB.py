import sys


def main():
    inputs = sys.stdin.read().split()

    for token in inputs:
        n = int(token)
        if n == 0:
            break
        print("Hello World")


if __name__ == "__main__":
    main()