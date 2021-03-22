using Godot;
using System;

public class Paddle : KinematicBody2D
{
    [Export]
    public UInt16 speed = 800; 

    public bool ballDropped = true;
    private Vector2 screenSize;
    private Vector2 velocity;

    public override void _Ready()
    {
        screenSize = GetViewport().Size;
    }

    public override void _PhysicsProcess(float delta) {
        velocity = new Vector2();
        ProcessInputs();

        if (ballDropped) {
            GetBall().ShiftToPaddle();
        }

        Move(delta);
    }

    private void Move(float delta) {
        velocity = velocity * speed;

        var collision = MoveAndCollide(velocity * delta);
        if (collision != null) {
            if (collision.Collider.HasMethod("BounceOfPaddle")) {
                collision.Collider.Call("BounceOfPaddle", velocity);
            }
        }
    }

    private void ProcessInputs() {
        if (Input.IsActionPressed("ui_right")) {
            velocity.x += 1;
        }

        if (Input.IsActionPressed("ui_left")) {
            velocity.x -= 1;
        }

        if (ballDropped && Input.IsActionPressed("ui_accept")) {
            ballDropped = false;
            GetBall().StartMoving();
        }
    }

    private Ball GetBall() {
        return (Ball)GetParent().GetNode("Ball");
    }
}
