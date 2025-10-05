using Godot;
using System;

public class UITimer : RichTextLabel
{
    private Timer _timer;

    public override void _Ready()
    {
        _timer = GetParent().GetParent().GetParent().GetParent().GetParent().GetParent().GetParent().GetNode<Timer>("MainTimer");
    }

    public override void _Process(float delta)
    {
        var minutesLeft = Mathf.Floor(_timer.TimeLeft / 60);
        var secondsLeft = (int)MathF.Floor(_timer.TimeLeft) % 60;
        Text = $"{minutesLeft.ToString("00")}:{secondsLeft.ToString("00")}";
    }
}
