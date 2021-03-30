using Godot;
using System;

public class LifeIndicator : Node2D
{
    [Export]
    public byte maxLives = 6;
    private byte lives = 0;
    private AnimatedSprite[] heartSprites;

    [Signal]
    public delegate void EndGame();

    public override void _Ready() {
        lives = maxLives;
        heartSprites = new AnimatedSprite[lives / 2];
        for (uint i = 0; i < lives / 2; i++) {
            heartSprites[i] = ((AnimatedSprite)GetNode("Heart" + (i + 1).ToString()));
        }
    }

    public void TakeDamage() {
        if (lives % 2 == 0) {
            heartSprites[lives / 2 - 1].Play("Half");
        } else {
            heartSprites[lives / 2].Play("Empty");
        }
        lives--;

        if (lives == 0) {
            GetTree().Paused = true;
            EmitSignal(nameof(EndGame));
            ((Node2D)GetParent().GetNode("GameOverScreen")).Show();
        }
    }
}
