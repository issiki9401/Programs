register = [0]*16
memory = [0 for i in range(254)]

n = int(input())
for i in range(n):
    memory[i] = int(input(), base=16)
# 假設 memory[254] 是 input(1) 進來
memory[254] = int(input())

ir = 0
pc = 0

while True:
    ir = memory[pc]
    opcode = (ir >> 12) & 0xf
    d2 = (ir >> 8) & 0xf
    d3 = (ir >> 4) & 0xf
    d4 = ir & 0xf
    Ms = ir & 0xff
    Md = (ir >> 4) & 0xff
    if opcode == 0:
        break
    elif opcode == 1:
        register[d2] = memory[Ms]
    elif opcode == 2:
        memory[Md] = register[d2]    # 修正這一行，其餘不變
    elif opcode == 3:
        register[d2] = register[d3] + register[d4]
    elif opcode == 4:
        register[d2] = register[d3] + register[d4]
    elif opcode == 5:
        register[d2] = register[d3]
    elif opcode == 6:
        register[d2] = ~register[d3] & 0xFFFF
    elif opcode == 7:
        register[d2] = register[d3] & register[d4]
    elif opcode == 8:
        register[d2] = register[d3] | register[d4]
    elif opcode == 9:
        register[d2] = register[d3] ^ register[d4]
    elif opcode == 10:
        register[d2] = (register[d2] + 1) & 0xFFFF
    elif opcode == 11:
        register[d2] = (register[d2] - 1) & 0xFFFF
    elif opcode == 12:
        n = d3
        if d4 == 0:
            register[d2] = ((register[d2] >> n) | (register[d2] << (16-n))) & 0xFFFF
        else:
            register[d2] = ((register[d2] << n) | (register[d2] >> (16-n))) & 0xFFFF
    elif opcode == 13:
        if register[0] != register[d2]:
            pc = d3 * 16 + d4
            continue
    else:
        break
    pc += 1

print(register[1])