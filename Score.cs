using Godot;
using System;

public class Score : Label
{
    private UInt32 score = 0;

    public void IncreaseScore(UInt32 amount) {
        score += amount;
        Text = score.ToString();
    }
}
