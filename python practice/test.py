def parse_ieee754_single(hex_string):
    """
    Parse an 8-digit hex string as IEEE 754 single precision float
    and return binary scientific notation
    """
    # Convert hex string to 32-bit unsigned integer
    value = int(hex_string, 16)
    
    # Extract components using bitwise operations
    sign_bit = (value >> 31) & 0x1        # 取得符號位元 (第 31 位)
    exponent = (value >> 23) & 0xFF       # 取得指數 (第 23-30 位)
    mantissa = value & 0x7FFFFF          # 取得尾數 (第 0-22 位)
    
    # Check for special cases
    if exponent == 0xFF:  # 指數全為 1
        if mantissa == 0:
            return "inf" if sign_bit == 0 else "-inf"
        else:
            return "NaN"
    
    # Determine if normalized or denormalized
    if exponent == 0:  # 非正規化數
        if mantissa == 0:
            return "0.0"
        else:
            # 對於非正規化數，找到第一個 1 位元並轉換為 1.xxx 格式
            mantissa_binary = format(mantissa, '023b')
            first_one_pos = mantissa_binary.find('1')
            
            if first_one_pos == -1:
                return "0.0"
            
            # 移位以取得 1.xxx 格式
            shifted_mantissa = mantissa_binary[first_one_pos + 1:]
            actual_exponent = -126 - first_one_pos - 1
            
            # 移除尾端的零
            shifted_mantissa = shifted_mantissa.rstrip('0')
            if not shifted_mantissa:
                result = f"1.0 x 2 ** {actual_exponent}"
            else:
                result = f"1.{shifted_mantissa} x 2 ** {actual_exponent}"
    else:  # 正規化數
        # 正規化數: (-1)^s × 1.mantissa × 2^(exponent-127)
        bias = 127
        actual_exponent = exponent - bias
        mantissa_binary = format(mantissa, '023b')
        # 移除尾端的零以得到更簡潔的輸出
        mantissa_binary = mantissa_binary.rstrip('0')
        if not mantissa_binary:
            result = f"1.0 x 2 ** {actual_exponent}"
        else:
            result = f"1.{mantissa_binary} x 2 ** {actual_exponent}"
    
    # 加上符號
    if sign_bit == 1:
        if result.startswith("inf"):
            return "-inf"
        else:
            result = "-" + result
    
    return result

def main():
    """
    Main program that reads hex inputs and outputs binary scientific notation
    """
    while True:
        try:
            hex_input = input().strip().upper()
            
            # 檢查輸入是否為 NaN 模式
            value = int(hex_input, 16)
            exponent = (value >> 23) & 0xFF
            mantissa = value & 0x7FFFFF
            
            # 如果遇到 NaN (指數 = 255 且尾數 != 0) 就停止
            if exponent == 0xFF and mantissa != 0:
                break
                
            result = parse_ieee754_single(hex_input)
            print(result)
            
        except (ValueError, EOFError):
            break

if __name__ == "__main__":
    main()