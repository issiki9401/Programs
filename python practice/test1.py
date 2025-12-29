import tkinter as tk

window = tk.Tk()
window.title("Neon Sky Defender v0.1")
window.resizable(False, False)

canvas_width = 600
canvas_height = 800
canvas = tk.Canvas(window, width=canvas_width, height=canvas_height, bg="black")
canvas.pack()

window.mainloop()