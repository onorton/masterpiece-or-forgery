using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ViewButtons : HBoxContainer
{
    private List<string> _visibleButtons;
    private List<Button> _buttons;

    public override void _Ready()
    {
        _buttons = GetChildren().OfType<Button>().ToList();
        _visibleButtons = Enum.GetValues(typeof(View)).Cast<View>().Select(v => v.ToString()).ToList();
        OnViewSelected("Front");
    }

    public void OnViewSelected(string view)
    {
        foreach (var button in _buttons.Where(b => _visibleButtons.Contains(b.Name)))
        {
            button.Disabled = button.Name == view;
        }
    }


}
