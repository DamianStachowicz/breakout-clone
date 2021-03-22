using Godot;
using System;

public class LifeIndicator : Node2D
{
    private byte lives = 6;
    private AnimatedSprite[] heartSprites;

    [Signal]
    public delegate void EndGame();

    public override void _Ready() {
        heartSprites = new AnimatedSprite[lives / 2];
        for (uint i = 0; i < lives / 2; i++) {
            heartSprites[i] = ((AnimatedSprite)GetNode("Heart" + (i + 1).ToString()));
        }
    }

    public void takeDamage() {
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
