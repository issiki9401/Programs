# -*- coding: utf-8 -*-

# Initialize registers and memory
register = [0] * 16  # 16 registers R0-R15
memory = [0] * 256  # 256 memory units (00-FF)

# Load program
n = int(input())
for i in range(n):
    memory[i] = int(input(), base=16)

# Initialize program counter and instruction register
ir = 0
pc = 0

# CPU execution loop
while True:
    # Fetch instruction
    ir = memory[pc]

    # Decode instruction
    opcode = (ir >> 12) & 0xF  # Operation code (d1)
    d2 = (ir >> 8) & 0xF  # 2nd operand
    d3 = (ir >> 4) & 0xF  # 3rd operand
    d4 = ir & 0xF  # 4th operand
    Ms = ir & 0xFF  # Source memory address (d3d4)
    Md = (ir >> 4) & 0xFF  # Destination memory address (d2d3)

    # Execute instruction
    if opcode == 0:  # HALT - Stop program execution
        break

    elif opcode == 1:  # LOAD - RD ← MS
        if Ms == 0xFE:  # Special address: input from keyboard
            register[d2] = int(input(), 16)
        else:
            register[d2] = memory[Ms]

    elif opcode == 2:  # STORE - MD ← RS
        if Md == 0xFF:  # Special address: output to screen
            print(f"{register[d4]:x}")
        else:
            memory[Md] = register[d4]

    elif opcode == 3:  # ADDI - Integer addition RD ← RS1 + RS2
        register[d2] = (register[d3] + register[d4]) & 0xFFFF

    elif opcode == 4:  # ADDF - Floating point addition RD ← RS1 + RS2
        # Simplified floating point implementation
        register[d2] = (register[d3] + register[d4]) & 0xFFFF

    elif opcode == 5:  # MOVE - RD ← RS
        register[d2] = register[d3]

    elif opcode == 6:  # NOT - Logical NOT RD ← R̄S
        register[d2] = (~register[d3]) & 0xFFFF

    elif opcode == 7:  # AND - Logical AND RD ← RS1 AND RS2
        register[d2] = register[d3] & register[d4]

    elif opcode == 8:  # OR - Logical OR RD ← RS1 OR RS2
        register[d2] = register[d3] | register[d4]

    elif opcode == 9:  # XOR - Logical XOR RD ← RS1 XOR RS2
        register[d2] = register[d3] ^ register[d4]

    elif opcode == 10:  # INC - Increment R ← R + 1
        register[d2] = (register[d2] + 1) & 0xFFFF

    elif opcode == 11:  # DEC - Decrement R ← R - 1
        register[d2] = (register[d2] - 1) & 0xFFFF

    elif opcode == 12:  # ROTATE - Rotation operation
        value = register[d2]
        n = d3  # Number of rotation bits
        direction = d4  # 0=right rotate, 1=left rotate

        if direction == 0:  # Right rotation
            register[d2] = ((value >> n) | (value << (16 - n))) & 0xFFFF
        else:  # Left rotation
            register[d2] = ((value << n) | (value >> (16 - n))) & 0xFFFF

    elif opcode == 13:  # JUMP - Conditional jump
        n = (d3 << 4) | d4  # Jump target address
        if register[0] != register[d2]:  # If R0 ≠ R, then jump
            pc = n
            continue  # Skip PC increment

    # PC increment (except when JUMP instruction has already jumped)
    pc += 1