using Godot;
using System;

public class Player : Area2D
{
    [Export]
    public int speed = 200;
    private Vector2 screenSize;
    private int velocity = 0;
    private float width;
    
    public override void _Ready() {
        screenSize = GetViewport().Size;

        Vector2 spriteSize = ((Sprite)GetNode("Sprite")).Texture.GetSize();
        width = spriteSize.x;
    }

    public override void _Process(float delta) {
        this.CheckKeyboardInput();
        this.Move(delta);
    }

    private void CheckKeyboardInput() {
        if (Input.IsActionPressed("ui_right")) {
            velocity += speed;
        }
        if (Input.IsActionPressed("ui_left")) {
            velocity -= speed;
        }
    }

    private void Move(float delta) {
        Position = new Vector2(
            x: Mathf.Clamp(velocity * delta, width / 2, screenSize.x - width / 2),
            y: Position.y
        );
    }
}
