import tkinter as tk

# 設定視窗
window = tk.Tk()
window.title("Neon Sky Defender v0.1")
window.resizable(False, False)

# 畫布設定
canvas_width = 600
canvas_height = 800
canvas = tk.Canvas(window, width=canvas_width, height=canvas_height, bg="black")
canvas.pack()

# 玩家設定
player_size = 50
player_x = canvas_width // 2
player_y = canvas_height - 100
# 繪製玩家 (青色三角形)
player = canvas.create_polygon(
    player_x, player_y - player_size,         # 頂點
    player_x - player_size/2, player_y + player_size/2, # 左下
    player_x + player_size/2, player_y + player_size/2, # 右下
    fill="cyan", outline="white"
)

# 移動速度
speed = 20

def move_player(event):
    """處理鍵盤事件，移動玩家 """
    key = event.keysym
    # 取得目前座標
    coords = canvas.coords(player) # 回傳 [x1, y1, x2, y2, x3, y3]
    
    # 計算中心點 (簡單估算)
    current_x = coords[0]
    current_y = (coords[1] + coords[3]) / 2

    if key == "Left" and current_x > 25:
        canvas.move(player, -speed, 0)
    elif key == "Right" and current_x < canvas_width - 25:
        canvas.move(player, speed, 0)
    elif key == "Up" and coords[1] > 0:
        canvas.move(player, 0, -speed)
    elif key == "Down" and coords[3] < canvas_height:
        canvas.move(player, 0, speed)

# 綁定按鍵
window.bind("<Key>", move_player)

window.mainloop()