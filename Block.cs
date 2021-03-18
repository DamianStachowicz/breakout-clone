using Godot;
using System;

public class Block : StaticBody2D
{
    [Export] Boolean wall = false;
    [Export] byte durability = 1;
    [Export] UInt32 points = 10;

    [Signal]
    public delegate void ScorePoints(UInt32 points);

    private AnimatedSprite animatedSprite;
    private string[] animationNames;

    public override void _Ready() {
        animatedSprite = ((AnimatedSprite)GetNode("AnimatedSprite"));
        loadAnimationNames();
        animatedSprite.Play(animationNames[durability]);
    }

    public void Hit() {
        if (wall) {
            return;
        }
        
        DecreaseDurability();
    }

    private void DecreaseDurability() {
        if (durability > 0) {
            animatedSprite.Play(animationNames[--durability]);
            EmitSignal(nameof(ScorePoints), points);
        } else {
            QueueFree();
        }
    }

    private void loadAnimationNames() {
        animationNames = animatedSprite.Frames.GetAnimationNames();
    }
}
