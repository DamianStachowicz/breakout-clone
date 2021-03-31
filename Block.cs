using Godot;
using System;

public class Block : StaticBody2D
{
    [Export] byte durability = 1;
    [Export] UInt32 points = 10;

    [Signal]
    public delegate void ScorePoints(UInt32 points);

    private AnimatedSprite animatedSprite;
    private string[] animationNames;
    private AudioStreamPlayer hitAudio;
    private AudioStreamPlayer destroyAudio;

    public override void _Ready() {
        animatedSprite = ((AnimatedSprite)GetNode("AnimatedSprite"));
        hitAudio = ((AudioStreamPlayer)GetNode("HitAudio"));
        destroyAudio = ((AudioStreamPlayer)GetNode("DestroyAudio"));
        LoadAnimationNames();
        animatedSprite.Play(animationNames[durability]);
    }

    public void Hit() {       
        hitAudio.Play();
        DecreaseDurability();
    }

    private void DecreaseDurability() {
        if (durability > 0) {
            animatedSprite.Play(animationNames[--durability]);
            EmitSignal(nameof(ScorePoints), points);
        } else {
            EmitSignal(nameof(ScorePoints), points);

            animatedSprite.Play("zbreak");
            animatedSprite.Offset = new Vector2(0, 47);
            destroyAudio.Play();
            ((CollisionShape2D)GetNode("CollisionShape2D")).Disabled = true;
        }
    }

    public void HideShards() {
        if (animatedSprite.Animation == "zbreak") {
            Hide();
        }
    }

    public void Die() {
        if (IsLastOneLeft()) {
            GetTree().Paused = true;
            ((Node2D)GetParent().GetNode("GameOverScreen")).Show();
        }

        QueueFree();
    }

    private void LoadAnimationNames() {
        animationNames = animatedSprite.Frames.GetAnimationNames();
    }

    private bool IsLastOneLeft() {
        return GetTree().GetNodesInGroup("Blocks").Count == 1;
    }
}
