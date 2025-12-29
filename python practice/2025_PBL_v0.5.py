import tkinter as tk
import random

window = tk.Tk()
window.title("Neon Sky Defender v0.5")
window.resizable(False, False)

WIDTH = 600
HEIGHT = 800
canvas = tk.Canvas(window, width=WIDTH, height=HEIGHT, bg="black")
canvas.pack()

# 遊戲變數 
player = canvas.create_polygon(300, 700, 275, 750, 325, 750, fill="cyan", outline="white")
bullets = [] # List 1
enemies = [] # List 2
game_over = False

def move_player(event):
    if game_over: return
    key = event.keysym
    coords = canvas.coords(player)
    if key == "Left" and coords[0] > 0: canvas.move(player, -20, 0)
    elif key == "Right" and coords[0] < WIDTH: canvas.move(player, 20, 0)
    elif key == "space": fire_bullet()

def fire_bullet():
    """自訂函式 1: 發射子彈"""
    coords = canvas.coords(player)
    # 在玩家頭頂生成子彈
    b = canvas.create_rectangle(coords[0]-5, coords[1]-20, coords[0]+5, coords[1], fill="yellow")
    bullets.append(b)

def create_enemy():
    """自訂函式 2: 生成敵人"""
    if random.randint(1, 20) == 1: # 隨機生成
        x = random.randint(30, WIDTH-30)
        e = canvas.create_oval(x-20, -40, x+20, 0, fill="red", outline="magenta")
        enemies.append(e)
    if not game_over:
        window.after(100, create_enemy)

def game_loop():
    """自訂函式 3: 遊戲主迴圈"""
    global game_over
    
    # 移動子彈
    for b in bullets[:]:
        canvas.move(b, 0, -10)
        if canvas.coords(b)[3] < 0:
            canvas.delete(b)
            bullets.remove(b)
            
    # 移動敵人
    for e in enemies[:]:
        canvas.move(e, 0, 5)
        if canvas.coords(e)[1] > HEIGHT:
            canvas.delete(e)
            enemies.remove(e)
            
        # 簡單碰撞偵測 (檢查重疊)
        p_bbox = canvas.bbox(player)
        e_bbox = canvas.bbox(e)
        
        # 檢查兩個矩形是否重疊 (玩家撞敵人)
        if p_bbox and e_bbox:
            if (p_bbox[0] < e_bbox[2] and p_bbox[2] > e_bbox[0] and
                p_bbox[1] < e_bbox[3] and p_bbox[3] > e_bbox[1]):
                print("Crash!")
                game_over = True
                canvas.create_text(WIDTH/2, HEIGHT/2, text="GAME OVER", fill="white", font=("Arial", 40))
                return # 停止迴圈

    if not game_over:
        window.after(50, game_loop)

window.bind("<Key>", move_player)
create_enemy()
game_loop()
window.mainloop()