using Godot;
using System;

public class Ball : KinematicBody2D
{
    [Export]
    public int speed = 400;
    [Export]
    public Vector2 startUpSpeed = new Vector2(200, 200);
    private Vector2 screenSize;
    private Vector2 velocity = new Vector2();
    
    public override void _Ready() {
        screenSize = GetViewport().Size;

        Vector2 spriteSize = ((Sprite)GetNode("Sprite")).Texture.GetSize();

        velocity = startUpSpeed;
    }

    public override void _PhysicsProcess(float delta) {
        var collision = MoveAndCollide(velocity * delta);

        if (collision != null) {
            velocity = velocity.Bounce(collision.Normal);

            if (collision.Collider.HasMethod("Hit")) {
                collision.Collider.Call("Hit");
            }
        }
    }

    public void BounceOfPaddle(Vector2 v) {
        velocity += new Vector2(v.x * 0.6f, 0);
    }
}
