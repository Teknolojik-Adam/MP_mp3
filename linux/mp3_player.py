#!/usr/bin/env python3
"""Linux MP3 player using Tkinter and ffplay (FFmpeg)."""
import os
import shutil
import signal
import subprocess
import tkinter as tk
from pathlib import Path
from tkinter import filedialog, messagebox, ttk


class Player:
    def __init__(self, root):
        self.root = root
        self.root.title("MP MP3 - Linux")
        self.root.geometry("520x360")
        self.files = []
        self.index = -1
        self.process = None
        self.paused = False

        self.listbox = tk.Listbox(root, selectmode=tk.EXTENDED)
        self.listbox.pack(fill="both", expand=True, padx=12, pady=(12, 6))
        self.listbox.bind("<Double-Button-1>", lambda _: self.play_selected())
        self.listbox.bind("<Delete>", lambda _: self.remove_selected())

        toolbar = ttk.Frame(root)
        toolbar.pack(pady=6)
        for i, (text, action) in enumerate([
            ("Dosya Ekle", self.add_files),
            ("Listeden Sil", self.remove_selected),
            ("Önceki", self.previous),
            ("Oynat", self.play_selected),
            ("Duraklat / Devam", self.toggle_pause),
            ("Durdur", self.stop),
            ("Sonraki", self.next),
        ]):
            ttk.Button(toolbar, text=text, command=action).grid(row=i // 4, column=i % 4, padx=2, pady=2)

        self.status = tk.StringVar(value="MP3 dosyası ekleyin")
        ttk.Label(root, textvariable=self.status).pack(pady=(4, 10))
        root.protocol("WM_DELETE_WINDOW", self.close)
        self.poll()

    def add_files(self):
        paths = filedialog.askopenfilenames(
            title="MP3 dosyalarını seçin", filetypes=[("MP3", "*.mp3"), ("Tüm dosyalar", "*.*")]
        )
        for path in paths:
            if Path(path).suffix.lower() == ".mp3" and path not in self.files:
                self.files.append(path)
                self.listbox.insert(tk.END, Path(path).name)

    def remove_selected(self):
        selected = list(self.listbox.curselection())
        if not selected:
            return
        if self.index in selected:
            self.stop()
            self.index = -1
        else:
            self.index -= sum(i < self.index for i in selected)
        for i in reversed(selected):
            del self.files[i]
            self.listbox.delete(i)

    def play_selected(self):
        selected = self.listbox.curselection()
        if selected:
            self.play(selected[0])
        elif self.files:
            self.play(self.index if self.index >= 0 else 0)

    def play(self, index):
        if not (0 <= index < len(self.files)):
            return
        if not shutil.which("ffplay"):
            messagebox.showerror("Eksik bağımlılık", "FFmpeg/ffplay kurulmalı.")
            return
        self.stop()
        self.index = index
        self.listbox.selection_clear(0, tk.END)
        self.listbox.selection_set(index)
        self.listbox.see(index)
        self.paused = False
        self.process = subprocess.Popen(
            ["ffplay", "-nodisp", "-autoexit", "-loglevel", "error", self.files[index]],
            stdin=subprocess.DEVNULL,
            stdout=subprocess.DEVNULL,
            stderr=subprocess.DEVNULL,
            start_new_session=True,
        )
        self.status.set("Çalıyor: " + Path(self.files[index]).name)

    def toggle_pause(self):
        if not self.process or self.process.poll() is not None:
            return
        os.killpg(self.process.pid, signal.SIGCONT if self.paused else signal.SIGSTOP)
        self.paused = not self.paused
        self.status.set("Duraklatıldı" if self.paused else "Çalıyor: " + Path(self.files[self.index]).name)

    def stop(self):
        if self.process and self.process.poll() is None:
            if self.paused:
                os.killpg(self.process.pid, signal.SIGCONT)
            self.process.terminate()
            try:
                self.process.wait(timeout=1)
            except subprocess.TimeoutExpired:
                self.process.kill()
                self.process.wait()
        self.process = None
        self.paused = False
        self.status.set("Durduruldu")

    def previous(self):
        if self.files:
            self.play(max(0, self.index - 1))

    def next(self):
        if self.files:
            self.play(min(len(self.files) - 1, self.index + 1))

    def poll(self):
        if self.process and self.process.poll() is not None:
            self.process = None
            self.status.set("Oynatma tamamlandı")
        self.root.after(500, self.poll)

    def close(self):
        self.stop()
        self.root.destroy()


if __name__ == "__main__":
    app = tk.Tk()
    Player(app)
    app.mainloop()
