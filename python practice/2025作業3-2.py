# -*- coding: utf-8 -*-

#建立暫存器和記憶體
register=[0]*16
memory=[0 for i in range(254)]
#為何要有不同寫法?

#loading programs讀取程式到記憶體
n=int(input())
for i in range(n):
    memory[i]=int(input(),base=16)    
ir=0 #initialization初始化
pc=0

while True:
    ir=memory[pc] #fetch instruction讀取指令
    opcode=(ir>>12) & 0xf  #取4位
    d2=(ir>>8) & 0xf
    d3=(ir>>4) & 0xf
    d4=ir&0xf
    Ms=ir&0xff  #取8位
    Md=(ir>>4) & 0xff
    if opcode==0:    #execute the instruction執行指令
        break
    elif opcode == 1:  # LOAD
        register[d2] = memory[Ms]
    elif opcode == 2:  # STORE
        memory[Md] = register[d4]
    elif opcode == 3:  # ADDI
        register[d2] = register[d3] + register[d4]
    elif opcode == 4:  # ADDF
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