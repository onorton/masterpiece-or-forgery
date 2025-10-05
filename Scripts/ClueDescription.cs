using Godot;
using System;

public class ClueDescription : RichTextLabel
{

    public override void _Ready()
    {
        BbcodeText = "";
    }

    public void OnClueFound(string description)
    {

        BbcodeText = $"[center]{description}[/center]";
        Visible = true;
    }

    public void OnLeaveClue()
    {
        Visible = false;
    }

}
