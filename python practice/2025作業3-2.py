# -*- coding: utf-8 -*-

register=[0]*16
memory=[0 for i in range(254)]
# loading programs
n=int(input())
for i in range(n):
    memory[i]=int(input(),base=16)    
ir=0 # initialization
pc=0

while True:
    ir=memory[pc] # fetch instruction
    opcode=(ir>>12) & 0xf  # decode the instruction by getting all fileds
    d2=(ir>>8) & 0xf
    d3=(ir>>4) & 0xf
    d4=ir&0xf
    Ms=ir&0xff
    Md=(ir>>4) & 0xff
    if opcode==0:    # execute the instruction
        break
    elif opcode==1:
        if Ms==0xfe:
            register[d2]=int(input(),16)
        else:
            register[d2]=memory[Ms]
    elif opcode==2:
        if Md==0xff:
            print(f'{register[d4]:x}')
        else:
            memory[Md]=register[d4]
    elif opcode==3:
        register[d2]=register[d3]+register[d4]
    elif opcode==4:
        register[d2]=register[d3]+register[d4]
    elif opcode==5:
        register[d2]=register[d3]
    elif opcode==6:
        pass
    elif opcode==7:
        pass  
    elif opcode==8:
        pass        
    elif opcode==9:
        pass
    elif opcode==10:
        pass
    elif opcode==11:
        pass
    elif opcode==12:
        pass
    elif opcode==13:
        pass
    else:
        break