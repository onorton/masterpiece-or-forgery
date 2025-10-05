using Godot;
using System;
using System.Diagnostics;

public class Clue : Node2D
{

    [Signal]
    public delegate void ClueFoundEventHandler(string description);
    [Signal]
    public delegate void LeaveClueHandler();


    [Export]
    private string _clueDescription;

    [Export]
    public View View;



    private ClueDescription _clueDescriptionUi;

    public override void _Ready()
    {
        _clueDescriptionUi = GetNode<ClueDescription>("/root/Root/UI/UI/MarginContainer/Bottom Panel/Clue Description");
    }


    public void OnMouseEnter()
    {
        _clueDescriptionUi.OnClueFound(_clueDescription);
    }

    public void OnMouseExit()
    {
        _clueDescriptionUi.OnLeaveClue();
    }
}


