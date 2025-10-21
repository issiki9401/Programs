import sys

for s in sys.stdin:
    s = s.strip()
    if not s: continue
    n = int(s,16)
    sign = '-' if (n>>31)&1 else ''
    e = (n>>23)&255
    m = n&0x7FFFFF

    if e==255:
        if m: break
        print(sign+"inf"); continue
    if not e and not m:
        print(sign+"0.0"); continue  # 修改這裡

    imp = 0 if e==0 else 1
    be = -126 if e==0 else e-127
    bits = bin((imp<<23)|m)[2:].rjust(24,'0')
    i = bits.find('1')
    frac = bits[i+1:].rstrip('0') or '0'
    print(f"{sign}1.{frac} x 2 ** {be-i}")