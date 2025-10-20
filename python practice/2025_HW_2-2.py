def hex_to_ieee754_scientific(hex_str):
    # 將16進位字串轉為整數
    n = int(hex_str, 16)
    # 取得sign (最高位元，第31位)
    sign = (n >> 31) & 0x1
    # 取得exponent (第30~23位), 8位元
    exp = (n >> 23) & 0xFF
    # 取得mantissa (第22~0位), 23位元
    mantissa = n & 0x7FFFFF

    if exp == 255:  # 排除 infinity，樣本保證不會有
        return None
    if exp == 0:  # subnormal數值
        exponent_val = -126
        mantissa_val = mantissa
        if mantissa_val == 0:
            # 0值
            return "0.0 x 2 ** 0"
        digits = bin(mantissa_val)[2:].zfill(23).rstrip('0') or '0'
        result = f"{'-' if sign else ''}1.{digits} x 2 ** -149"
    else:  # normal數值
        exponent_val = exp - 127
        mantissa_val = mantissa
        digits = bin(mantissa_val)[2:].zfill(23).rstrip('0')
        # 將1.及mantissa (23位)組成boolean科學記數法
        if digits:
            result = f"{'-' if sign else ''}1.{digits} x 2 ** {exponent_val}"
        else:
            result = f"{'-' if sign else ''}1.0 x 2 ** {exponent_val}"

    return result

while True:
    s = input().strip()
    if s == "NaN":
        break
    output = hex_to_ieee754_scientific(s)
    if output is not None:
        print(output)