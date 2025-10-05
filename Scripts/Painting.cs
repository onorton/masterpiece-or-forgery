using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


[Tool]
public class Painting : Node2D
{

    [Export]
    public string Title;

    [Export]
    public string Artist;

    [Export]

    public int Year;

    [Export]
    public bool Real = true;

    [Export]
    private List<string> _possibleHints;

    private int lastHintSelected = -1;

    [Export]
    private Texture _front;

    [Export]
    private Texture _back;


    [Export]
    private View _currentView;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _currentView = View.Front;
        SetView(_currentView);
        // GetNode<Sprite>("Sprite").Visible = false;
    }

    public override void _Process(float delta)
    {
        if (Engine.EditorHint)
        {
            SetView(_currentView);
        }
    }

    public Texture SetView(View view)
    {
        _currentView = view;

        Texture currentTexture = _currentView switch
        {
            View.Front => _front,
            View.Back => _back,
        };

        GetNode<Sprite>("Sprite").Texture = currentTexture;
        var clues = GetNode("Clues").GetChildren().OfType<Clue>();

        foreach (var clue in clues)
        {
            clue.Visible = clue.View == _currentView;
        }

        return currentTexture;
    }

    public string NextHint()
    {
        if (_possibleHints == null || _possibleHints.Count == 0)
        {
            return "";
        }

        if (_possibleHints.Count == 1)
        {
            return _possibleHints[0];
        }

        while (true)
        {
            var nextIndex = (int)(GD.Randi() % _possibleHints.Count);
            if (nextIndex != lastHintSelected)
            {
                lastHintSelected = nextIndex;
                return _possibleHints[lastHintSelected];
            }
        }

    }
}
