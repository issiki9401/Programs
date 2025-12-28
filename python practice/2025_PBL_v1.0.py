import tkinter as tk
import random

# --- 設定區 ---
WIDTH = 600
HEIGHT = 800
PLAYER_SPEED = 20
BULLET_SPEED = 15
ENEMY_SPEED = 6
WIN_SCORE = 1000  # 勝利條件 
MAX_HP = 3

class Game:
    def __init__(self, root):
        self.root = root
        self.root.title("Neon Sky Defender v1.0")
        self.canvas = tk.Canvas(root, width=WIDTH, height=HEIGHT, bg="#111111")
        self.canvas.pack()

        # 遊戲狀態
        self.score = 0
        self.hp = MAX_HP
        self.is_running = False
        self.bullets = []  # List 結構 
        self.enemies = []
        
        # 介面文字 (HUD)
        self.score_text = self.canvas.create_text(50, 30, text=f"Score: {self.score}", fill="white", font=("Courier", 16), anchor="w")
        self.hp_text = self.canvas.create_text(WIDTH-50, 30, text=f"HP: {self.hp}", fill="#00FF00", font=("Courier", 16), anchor="e")
        self.center_msg = self.canvas.create_text(WIDTH/2, HEIGHT/2, text="Press SPACE to Start", fill="white", font=("Courier", 30))

        # 玩家
        self.player = self.canvas.create_polygon(300, 700, 275, 750, 325, 750, fill="#00FFFF", outline="white", width=2)
        
        # 綁定按鍵
        self.root.bind("<Key>", self.handle_input)
        
        self.root.after(0, self.animation_loop) # 啟動背景動畫(非必要，裝飾用)

    def start_game(self):
        self.is_running = True
        self.score = 0
        self.hp = MAX_HP
        self.canvas.itemconfigure(self.center_msg, state="hidden")
        self.update_ui()
        self.spawn_enemy()
        self.game_loop()

    def handle_input(self, event):
        key = event.keysym
        if not self.is_running:
            if key == "space":
                self.start_game()
            return

        coords = self.canvas.coords(self.player)
        # 玩家移動
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
        # 射擊函式 
        coords = self.canvas.coords(self.player)
        # 現代風格子彈
        b = self.canvas.create_line(coords[0], coords[1]-10, coords[0], coords[1]-30, fill="#FFFF00", width=4)
        self.bullets.append(b)

    def spawn_enemy(self):
        # 隨機生成敵人
        if self.is_running:
            x = random.randint(30, WIDTH-30)
            # 敵人造型
            e = self.canvas.create_oval(x-20, -40, x+20, 0, fill="#FF0055", outline="white", width=2)
            self.enemies.append(e)
            # 隨機延遲生成下一個
            delay = random.randint(800, 1500)
            self.root.after(delay, self.spawn_enemy)

    def check_collision(self):
        """核心碰撞偵測邏輯"""
        # 1. 子彈 vs 敵人
        for b in self.bullets[:]:
            b_box = self.canvas.bbox(b)
            if not b_box: continue
            
            hit = False
            for e in self.enemies[:]:
                e_box = self.canvas.bbox(e)
                if not e_box: continue
                
                # 判斷重疊 [overlapping]
                if (b_box[0] < e_box[2] and b_box[2] > e_box[0] and
                    b_box[1] < e_box[3] and b_box[3] > e_box[1]):
                    # 擊中
                    self.canvas.delete(e)
                    self.canvas.delete(b)
                    self.enemies.remove(e)
                    self.bullets.remove(b)
                    self.score += 100
                    hit = True
                    break
            if not hit and b_box[3] < 0: # 出界移除
                self.canvas.delete(b)
                self.bullets.remove(b)

        # 2. 玩家 vs 敵人
        p_box = self.canvas.bbox(self.player)
        for e in self.enemies[:]:
            e_box = self.canvas.bbox(e)
            if (p_box[0] < e_box[2] and p_box[2] > e_box[0] and
                p_box[1] < e_box[3] and p_box[3] > e_box[1]):
                # 撞擊
                self.canvas.delete(e)
                self.enemies.remove(e)
                self.hp -= 1
                self.flash_screen("red") # 受傷特效

    def update_ui(self):
        self.canvas.itemconfigure(self.score_text, text=f"Score: {self.score}")
        color = "#00FF00" if self.hp > 1 else "#FF0000"
        self.canvas.itemconfigure(self.hp_text, text=f"HP: {self.hp}", fill=color)

    def flash_screen(self, color):
        # 簡單的視覺回饋
        self.canvas.configure(bg=color)
        self.root.after(100, lambda: self.canvas.configure(bg="#111111"))

    def check_game_state(self):
        # 勝利條件 
        if self.score >= WIN_SCORE:
            self.end_game("VICTORY!", "#00FF00")
            return

        # 失敗條件 
        if self.hp <= 0:
            self.end_game("GAME OVER", "#FF0000")
            return

    def end_game(self, message, color):
        self.is_running = False
        self.canvas.itemconfigure(self.center_msg, text=message, fill=color, state="normal")
        # 清除場面
        for x in self.bullets + self.enemies:
            self.canvas.delete(x)
        self.bullets.clear()
        self.enemies.clear()

    def game_loop(self):
        if not self.is_running: return

        # 移動物件
        for b in self.bullets: self.canvas.move(b, 0, -BULLET_SPEED)
        for e in self.enemies: self.canvas.move(e, 0, ENEMY_SPEED)
        
        # 清除超出邊界的敵人 (扣分或不扣分皆可，此處選擇略過)
        for e in self.enemies[:]:
            if self.canvas.coords(e)[1] > HEIGHT:
                self.canvas.delete(e)
                self.enemies.remove(e)

        self.check_collision()
        self.update_ui()
        self.check_game_state()

        self.root.after(50, self.game_loop)

    def animation_loop(self):
        # 裝飾用：如果遊戲沒在跑，讓文字閃爍
        if not self.is_running:
            current_color = self.canvas.itemcget(self.center_msg, "fill")
            new_color = "gray" if current_color == "white" else "white"
            # 只有在非結束狀態才閃爍白字 (避免覆蓋勝利顏色)
            if "Start" in self.canvas.itemcget(self.center_msg, "text"):
                self.canvas.itemconfigure(self.center_msg, fill=new_color)
        self.root.after(500, self.animation_loop)

# 啟動
root = tk.Tk()
app = Game(root)
root.mainloop()