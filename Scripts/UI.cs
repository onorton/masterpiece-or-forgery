using Godot;
using System;

public class UI : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Control>("Score").Visible = false;
        GetNode<Control>("CurrentPainting").Visible = true;
        Visible = true;

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public void SetPieceInfo(Painting painting)
    {
        GetNode<RichTextLabel>("MarginContainer/Bottom Panel/Information Panel/VBoxContainer/Title/Value").Text = painting.Title;
        GetNode<RichTextLabel>("MarginContainer/Bottom Panel/Information Panel/VBoxContainer/Artist/Value").Text = painting.Artist;
        GetNode<RichTextLabel>("MarginContainer/Bottom Panel/Information Panel/VBoxContainer/Year/Value").Text = painting.Year.ToString();
    }

    public void SetScore(int realPurchases, int counterfeitPurchases, int totalRealPieces)
    {
        GetNode<Control>("Score").Visible = true;
        GetNode<RichTextLabel>("Score/VBoxContainer/Real Items Purchased").BbcodeText = $"[center]Real Items Purchased: {realPurchases}/{totalRealPieces}[/center]";
        GetNode<RichTextLabel>("Score/VBoxContainer/Counterfeits Purchased").BbcodeText = $"[center]Counterfeit Purchases: {counterfeitPurchases}[/center]";

        var scoreSummary = "";
        if (realPurchases == totalRealPieces && counterfeitPurchases == 0)
        {
            scoreSummary = "Perfect!";
        }
        else if (realPurchases > totalRealPieces / 2)
        {
            scoreSummary = "Not bad";
        }
        else
        {
            scoreSummary = "There's room for improvement";
        }

        GetNode<RichTextLabel>("Score/VBoxContainer/Score Summary").BbcodeText = $"[center]{scoreSummary}[/center]";

    }

    public void SetHint(string hint)
    {
        if (hint == "")
        {
            GetNode<Control>("HintContainer").Visible = false;
        }
        else
        {
            GetNode<Control>("HintContainer").Visible = true;
            GetNode<RichTextLabel>("HintContainer/Hint").Text = $"Hint: {hint}";
        }
    }

    public void SetCurrentPainting(Texture texture)
    {
        GetNode<TextureRect>("CurrentPainting").Texture = texture;

    }

    public void DisableButtons()
    {
        foreach (var button in GetNode<Control>("CurrentPainting/View Buttons").GetChildren())
        {
            (button as Button).Disabled = true;
        }

        foreach (var button in GetNode<Control>("MarginContainer/Bottom Panel/HBoxContainer").GetChildren())
        {
            (button as Button).Disabled = true;
        }
    }



}
