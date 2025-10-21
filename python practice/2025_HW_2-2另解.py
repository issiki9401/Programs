import sys


def ieee754_to_binary_scientific(hex_str):
    """
    將 8 位 16 進位數轉換為 IEEE754 單精度浮點數的二進位科學記數法

    參數:
        hex_str: 8 位 16 進位字串

    返回:
        二進位科學記數法字串，如果是 NaN 則返回 None
    """
    # 將 16 進位轉為 32 位整數
    num = int(hex_str, 16)

    # 使用位元運算提取各部分
    sign = (num >> 31) & 1  # 取最高位
    exponent = (num >> 23) & 0xFF  # 取 bits 30-23
    mantissa = num & 0x7FFFFF  # 取 bits 22-0

    # 判斷特殊情況
    if exponent == 255:
        if mantissa == 0:
            # 無窮大
            return "-inf" if sign else "inf"
        else:
            # NaN (用來標記輸入結束)
            return None

    # 處理符號
    sign_str = "-" if sign else ""

    if exponent == 0:
        # 非規格化數 (Denormalized)
        if mantissa == 0:
            # 零
            return f"{sign_str}0.0"

        # 非規格化數: 0.mantissa × 2^(-126)
        # 需要將 mantissa 調整為 1.xxx 的形式

        # 構建 mantissa 的二進位表示（23 位）
        mantissa_bits = bin(mantissa)[2:].zfill(23)

        # 找到第一個 1 的位置（從左往右）
        first_one_pos = mantissa_bits.index("1")

        # 計算實際指數
        # mantissa 的每一位代表 2^(-1), 2^(-2), ..., 2^(-23)
        # 第一個 1 在位置 first_one_pos，代表 2^(-(first_one_pos+1))
        # 要變成 1.xxx × 2^y 的形式，y = -126 - (first_one_pos + 1)
        actual_exponent = -126 - (first_one_pos + 1)

        # 從第一個 1 之後的位數就是小數部分
        fractional_part = mantissa_bits[first_one_pos + 1 :]

        if fractional_part and fractional_part.rstrip("0"):
            # 有小數部分
            mantissa_binary = "1." + fractional_part.rstrip("0")
        else:
            # 沒有小數部分
            mantissa_binary = "1.0"

    else:
        # 規格化數 (Normalized)
        # 實際指數 = 儲存的指數 - 127
        actual_exponent = exponent - 127

        # mantissa 有隱含的 1，即 1.mantissa
        # 將 mantissa 轉為二進位字串
        mantissa_bits = bin(mantissa)[2:].zfill(23)

        # 移除尾部的 0
        fractional_part = mantissa_bits.rstrip("0")

        if fractional_part:
            mantissa_binary = "1." + fractional_part
        else:
            mantissa_binary = "1.0"

    # 格式化輸出
    return f"{sign_str}{mantissa_binary} x 2 ** {actual_exponent}"


def main():
    """
    主程式：讀取 16 進位數並輸出對應的二進位科學記數法
    """
    for line in sys.stdin:
        hex_str = line.strip()
        if not hex_str:
            continue

        result = ieee754_to_binary_scientific(hex_str)

        # 如果是 NaN，結束程式
        if result is None:
            break

        print(result)


if __name__ == "__main__":
    main()
