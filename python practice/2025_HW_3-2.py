def main():
    # 指令數量
    n = int(input())
    # 指令區
    instructions = [input().strip() for _ in range(n)]
    # 只讀下一行作為資料
    data = [int(input())]

    # 初始化
    PC = 0
    registers = [0] * 16
    memory = [0] * 256
    for i in range(n):
        memory[i] = int(instructions[i], 16)
    for i, d in enumerate(data):
        memory[n + i] = d

    # 主迴圈
    while True:
        instr = memory[PC]
        opcode = (instr >> 12) & 0xF
        d2 = (instr >> 8) & 0xF
        d3 = (instr >> 4) & 0xF
        d4 = instr & 0xF
        if opcode == 0:
            break
        elif opcode == 1:
            registers[d2] = memory[d3 * 16 + d4]
        elif opcode == 2:
            memory[d3 * 16 + d4] = registers[d2]
        elif opcode == 3:
            registers[d2] = registers[d3] + registers[d4]
        elif opcode == 4:
            registers[d2] = registers[d3] + registers[d4]
        elif opcode == 5:
            registers[d2] = registers[d3]
        elif opcode == 6:
            registers[d2] = ~registers[d3]
        elif opcode == 7:
            registers[d2] = registers[d3] & registers[d4]
        elif opcode == 8:
            registers[d2] = registers[d3] | registers[d4]
        elif opcode == 9:
            registers[d2] = registers[d3] ^ registers[d4]
        elif opcode == 0xA:
            registers[d2] += 1
        elif opcode == 0xB:
            registers[d2] -= 1
        elif opcode == 0xC:
            n = d3 * 16 + d4
            r = registers[d2]
            if n == 0:
                registers[d2] = ((r << 1) | (r >> 15)) & 0xFFFF
        elif opcode == 0xD:
            if registers[d2] == 0:
                PC = d3 * 16 + d4
                continue
        PC += 1

    # 直接輸出 registers[1] 結果
    print(registers[1])

if __name__ == "__main__":
    main()