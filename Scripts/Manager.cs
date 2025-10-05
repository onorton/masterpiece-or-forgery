using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Manager : Node2D
{

    private Painting _currentPiece;

    private List<Painting> _pendingPieces;

    private List<Painting> _purchasedPieces;

    private int _totalRealPieces = 0;

    private View _currentView;

    public override void _Ready()
    {
        GD.Randomize();
        GetNode<UI>("UI/UI").SetHint("");
        _purchasedPieces = new List<Painting>();
        _pendingPieces = GetNode("Paintings").GetChildren().OfType<Painting>().ToList();
        foreach (var pendingPainting in _pendingPieces)
        {
            pendingPainting.Visible = false;
        }

        _totalRealPieces = _pendingPieces.Count(p => p.Real);

        _currentView = View.Front;

        NextPainting();

        GetNode<Timer>("MainTimer").Start();
    }


    public void OnBuy()
    {
        GD.Print("Bought painting");
        if (_currentPiece != null)
        {
            _purchasedPieces.Add(_currentPiece);
        }
        NextPainting();
        GetNode<SoundManager>("SoundManager").OnPlaySoundEffect("purchase");
    }

    public void OnReject()
    {
        GD.Print("Rejected painting");
        NextPainting();
        GetNode<SoundManager>("SoundManager").OnPlaySoundEffect("reject");

    }

    public void OnHintTimerExpired()
    {
        if (_currentPiece != null)
        {
            GetNode<UI>("UI/UI").SetHint(_currentPiece.NextHint());
        }
        GetNode<Timer>("HintTimer").Start();
    }

    private async Task NextPainting()
    {
        if (_currentPiece != null)
        {
            _currentPiece.Visible = false;
            _currentPiece = null;
        }

        if (_pendingPieces.Count > 0)
        {
            _currentPiece = _pendingPieces[0];
            _currentPiece.Visible = true;
            GetNode<UI>("UI/UI").SetPieceInfo(_currentPiece);
            GetNode<UI>("UI/UI").SetHint("");
            GetNode<Timer>("HintTimer").Start();
            _pendingPieces.RemoveAt(0);
            OnViewSelected(View.Front.ToString());
        }
        else
        {
            await OnFinished();
        }
    }

    public async Task OnFinished()
    {
        var timeLeft = GetNode<Timer>("MainTimer").TimeLeft;
        GetNode<Timer>("MainTimer").Paused = true;
        GD.Print($"Time left: {timeLeft}");
        var realPurchases = _purchasedPieces.Where(p => p.Real).Count();
        var counterfeitPurchases = _purchasedPieces.Where(p => !p.Real).Count();
        GetNode<UI>("UI/UI").DisableButtons();

        var timer = GetTree().CreateTimer(0.0f);
        await ToSignal(timer, "timeout");

        GetNode<UI>("UI/UI").SetScore(realPurchases, counterfeitPurchases, _totalRealPieces);
        if (realPurchases == _totalRealPieces && counterfeitPurchases == 0)
        {
            GetNode<SoundManager>("SoundManager").OnPlaySoundEffect("victory");
        }
        else
        {
            GetNode<SoundManager>("SoundManager").OnPlaySoundEffect("defeat");
        }

    }

    public void OnTryAgain()
    {
        GetTree().ReloadCurrentScene();
    }

    public void OnQuit()
    {
        GetTree().Quit();
    }

    public void OnViewSelected(string viewString)
    {
        Enum.TryParse<View>(viewString, out var view);
        var texture = _currentPiece.SetView(view);
        _currentView = view;

        GetNode<ViewButtons>("UI/UI/CurrentPainting/View Buttons").OnViewSelected(viewString);
        GetNode<UI>("UI/UI").SetCurrentPainting(texture);

    }

}
