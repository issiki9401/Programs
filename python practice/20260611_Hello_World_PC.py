import sys

def solve():
    input_data = sys.stdin.read().split()
    if not input_data:
        return
    
    T = int(input_data[0])
    
    output_blocks = []
    
    for i in range(1, T + 1):
        n = int(input_data[i])
        
        block = "Hello World\n" * n
        output_blocks.append(block)
    
    sys.stdout.write("\n".join(output_blocks))

if __name__ == "__main__":
    solve()