using Godot;
using System;

public class GameOverScreen : Node2D
{
    public override void _Ready() {
        Hide();
    }

    public void RestartGame() {
        GetTree().Paused = false;
        GetTree().ChangeScene("Main.tscn");
    }

    public void Quit() {
        GetTree().Quit();
    }
}
