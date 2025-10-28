n=int(input())
for A in range(n):
    arr=input().split()
    hex_str=input()
    pc=arr[int(hex_str,16)]
    print(pc)