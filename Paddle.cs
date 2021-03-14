using Godot;
using System;

public class Paddle : KinematicBody2D
{
    [Export]
    public UInt16 Speed = 400; 
    private Vector2 screenSize;

    public override void _Ready()
    {
        screenSize = GetViewport().Size;
    }

    public override void _PhysicsProcess(float delta) {
        var velocity = new Vector2();

        if (Input.IsActionPressed("ui_right")) {
            velocity.x += 1;
        }

        if (Input.IsActionPressed("ui_left")) {
            velocity.x -= 1;
        }

        velocity = velocity.Normalized() * Speed;

        var collision = MoveAndCollide(velocity * delta);
        if (collision != null) {
            if (collision.Collider.HasMethod("BounceOfPaddle")) {
                collision.Collider.Call("BounceOfPaddle", velocity);
            }
        }
    }
}
