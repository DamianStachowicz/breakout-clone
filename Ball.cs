using Godot;
using System;

public class Ball : KinematicBody2D
{
    [Export]
    public int speed = 500;

    [Signal]
    public delegate void TakeDamage();

    private Vector2 screenSize;
    private Vector2 velocity = new Vector2();
    
    public override void _Ready() {
        screenSize = GetViewport().Size;
        Vector2 spriteSize = ((Sprite)GetNode("Sprite")).Texture.GetSize();
        ShiftToPaddle();
    }

    public override void _PhysicsProcess(float delta) {
        var collision = MoveAndCollide(velocity * delta);

        if (collision != null) {
            velocity = velocity.Bounce(collision.Normal);

            if (collision.Collider.HasMethod("Hit")) {
                collision.Collider.Call("Hit");
            }
        }

        if (Position.y > screenSize.y) {
            EmitSignal(nameof(TakeDamage));
            GetPaddle().ballDropped = true;
            ShiftToPaddle();
        }
    }

    public void BounceOfPaddle(Vector2 v) {
        velocity += new Vector2(v.x, 0).Normalized() * speed;
    }

    public void ShiftToPaddle() {
        Paddle paddle = GetPaddle();
        CapsuleShape2D paddleShape = (CapsuleShape2D)((CollisionShape2D)paddle.GetNode("CollisionShape2D")).Shape;

        Position = new Vector2(x: GetPaddle().Position.x, y: paddle.Position.y - paddleShape.Radius * 2);
        velocity = new Vector2();
    }

    public void StartMoving() {
        velocity = new Vector2(1, 1) * speed;
    }

    private Paddle GetPaddle() {
        return (Paddle)GetParent().GetNode("Paddle");
    }
}
