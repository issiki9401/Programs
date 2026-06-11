import requests
from datetime import datetime

url = "https://ace.io/polarisex/oapi/v2/list/tradePrice"
response = requests.get(url)

# 獲取當下時間
current_time = datetime.now()

# 格式化當下時間並印出
formatted_time = current_time.strftime("%Y-%m-%d %H:%M:%S")
print(f"\n請求時間：{formatted_time}")

# 如果程式狀態回應是200，​那回應將會解析成json格式的數據，存在變數data中
if response.status_code == 200:
    data = response.json()
    # 定義變數為ACE_usdt_twd，它取得data內USDT/TWD的last_price
    ACE_usdt_twd = data.get("USDT/TWD").get("last_price")
    
    # 變數字串化
    print(f"ACE 的USDT/TWD匯率: {ACE_usdt_twd}")

else : 
    print("壞了，根本就不能用啊，ERROR CODE:",response.status_code)