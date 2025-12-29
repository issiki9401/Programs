import tkinter as tk
import random

# --- v0.9 參數微調區 ---
WIDTH = 600
HEIGHT = 800
PLAYER_SPEED = 20
BULLET_SPEED = 15
ENEMY_SPEED = 5      # 微調：比 v1.0 稍慢，方便測試
WIN_SCORE = 1000     # 勝利分數
MAX_HP = 3           # 最大生命值

class Game:
    def __init__(self, root):
        self.root = root
        self.root.title("Neon Sky Defender v0.9 (Optimization)")
        self.canvas = tk.Canvas(root, width=WIDTH, height=HEIGHT, bg="#111111")
        self.canvas.pack()

        # 遊戲變數初始化
        self.score = 0
        self.hp = MAX_HP
        self.game_over = False # 取代 is_running，直接預設為開始狀態
        self.bullets = [] 
        self.enemies = []
        
        # 介面文字 (HUD)
        self.score_text = self.canvas.create_text(50, 30, text=f"Score: {self.score}", fill="white", font=("Courier", 16), anchor="w")
        self.hp_text = self.canvas.create_text(WIDTH-50, 30, text=f"HP: {self.hp}", fill="#00FF00", font=("Courier", 16), anchor="e")
        
        # 中央訊息 (僅在結束時顯示)
        self.center_msg = self.canvas.create_text(WIDTH/2, HEIGHT/2, text="", fill="white", font=("Courier", 30), state="hidden")

        # 建立玩家
        self.player = self.canvas.create_polygon(300, 700, 275, 750, 325, 750, fill="#00FFFF", outline="white", width=2)
        
        # 綁定按鍵
        self.root.bind("<Key>", self.handle_input)
        
        # === v0.9 改動：直接啟動遊戲 ===
        self.spawn_enemy()
        self.game_loop()

    def handle_input(self, event):
        if self.game_over: return # 遊戲結束後鎖定操作

        key = event.keysym
        coords = self.canvas.coords(self.player)
        
        # 玩家移動控制
        if key == "Left" and coords[0] > 0:
            self.canvas.move(self.player, -PLAYER_SPEED, 0)
        elif key == "Right" and coords[0] < WIDTH:
            self.canvas.move(self.player, PLAYER_SPEED, 0)
        elif key == "Up" and coords[1] > 0:
            self.canvas.move(self.player, 0, -PLAYER_SPEED)
        elif key == "Down" and coords[3] < HEIGHT:
            self.canvas.move(self.player, 0, PLAYER_SPEED)
        elif key == "space":
            self.shoot()

    def shoot(self):
        coords = self.canvas.coords(self.player)
        # v0.9 優化：調整子彈樣式
        b = self.canvas.create_line(coords[0], coords[1]-10, coords[0], coords[1]-30, fill="#FFFF00", width=4)
        self.bullets.append(b)

    def spawn_enemy(self):
        if not self.game_over:
            x = random.randint(30, WIDTH-30)
            e = self.canvas.create_oval(x-20, -40, x+20, 0, fill="#FF0055", outline="white", width=2)
            self.enemies.append(e)
            
            # v0.9 微調：生成間隔稍微拉長 (1000-2000ms)，讓節奏更穩
            delay = random.randint(1000, 2000)
            self.root.after(delay, self.spawn_enemy)

    def check_collision(self):
        # 1. 子彈擊中敵人
        for b in self.bullets[:]:
            b_box = self.canvas.bbox(b)
            if not b_box: continue
            
            hit = False
            for e in self.enemies[:]:
                e_box = self.canvas.bbox(e)
                if not e_box: continue
                
                if (b_box[0] < e_box[2] and b_box[2] > e_box[0] and
                    b_box[1] < e_box[3] and b_box[3] > e_box[1]):
                    
                    self.canvas.delete(e)
                    self.canvas.delete(b)
                    self.enemies.remove(e)
                    self.bullets.remove(b)
                    self.score += 100
                    hit = True
                    break
            
            if not hit and b_box[3] < 0:
                self.canvas.delete(b)
                self.bullets.remove(b)

        # 2. 敵人撞擊玩家
        p_box = self.canvas.bbox(self.player)
        for e in self.enemies[:]:
            e_box = self.canvas.bbox(e)
            if (p_box[0] < e_box[2] and p_box[2] > e_box[0] and
                p_box[1] < e_box[3] and p_box[3] > e_box[1]):
                
                self.canvas.delete(e)
                self.enemies.remove(e)
                self.hp -= 1
                self.update_ui()
                self.flash_screen("red")

    def update_ui(self):
        self.canvas.itemconfigure(self.score_text, text=f"Score: {self.score}")
        color = "#00FF00" if self.hp > 1 else "#FF0000"
        self.canvas.itemconfigure(self.hp_text, text=f"HP: {self.hp}", fill=color)

    def flash_screen(self, color):
        self.canvas.configure(bg=color)
        self.root.after(100, lambda: self.canvas.configure(bg="#111111"))

    def check_game_state(self):
        if self.score >= WIN_SCORE:
            self.end_game("VICTORY!", "#00FF00")
        elif self.hp <= 0:
            self.end_game("GAME OVER", "#FF0000")

    def end_game(self, message, color):
        self.game_over = True
        self.canvas.itemconfigure(self.center_msg, text=message, fill=color, state="normal")
        # 清除所有動態物件
        for x in self.bullets + self.enemies:
            self.canvas.delete(x)
        self.bullets.clear()
        self.enemies.clear()

    def game_loop(self):
        if self.game_over: return

        # 移動邏輯
        for b in self.bullets: self.canvas.move(b, 0, -BULLET_SPEED)
        for e in self.enemies: self.canvas.move(e, 0, ENEMY_SPEED)
        
        # 移除超出邊界的敵人 (不扣分)
        for e in self.enemies[:]:
            if self.canvas.coords(e)[1] > HEIGHT:
                self.canvas.delete(e)
                self.enemies.remove(e)

        self.check_collision()
        self.update_ui()
        self.check_game_state()

        self.root.after(50, self.game_loop)

# 啟動程式
if __name__ == "__main__":
    root = tk.Tk()
    app = Game(root)
    root.mainloop()