register = [0] * 16
memory = [0] * 256

n = int(input())
for i in range(n):
    memory[i] = int(input(), base=16)

ir = 0
pc = 0

while True:
    ir = memory[pc]

    opcode = (ir >> 12) & 0xF
    d2 = (ir >> 8) & 0xF
    d3 = (ir >> 4) & 0xF
    d4 = ir & 0xF
    Ms = ir & 0xFF
    Md = (ir >> 4) & 0xFF

    if opcode == 0:
        break

    elif opcode == 1:
        if Ms == 0xFE:
            register[d2] = int(input(), 16)
        else:
            register[d2] = memory[Ms]

    elif opcode == 2:
        if Md == 0xFF:
            print(f"{register[d4]:x}")
        else:
            memory[Md] = register[d4]

    elif opcode == 3:
        register[d2] = (register[d3] + register[d4]) & 0xFFFF

    elif opcode == 4:
        register[d2] = (register[d3] + register[d4]) & 0xFFFF

    elif opcode == 5:
        register[d2] = register[d3]

    elif opcode == 6:
        register[d2] = (~register[d3]) & 0xFFFF

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
        value = register[d2]
        n = d3
        direction = d4

        if direction == 0:
            register[d2] = ((value >> n) | (value << (16 - n))) & 0xFFFF
        else:
            register[d2] = ((value << n) | (value >> (16 - n))) & 0xFFFF

    elif opcode == 13:
        n = (d3 << 4) | d4
        if register[0] != register[d2]:
            pc = n
            continue

    pc += 1