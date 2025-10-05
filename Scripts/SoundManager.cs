using Godot;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Node
{
    private AudioStreamPlayer2D _mainMusic;
    private AudioStreamPlayer2D _soundEffectPlayer;


    public override void _Ready()
    {
        _mainMusic = GetNode<AudioStreamPlayer2D>("Music");
        _soundEffectPlayer = GetNode<AudioStreamPlayer2D>("SoundEffectPlayer");
    }

    public void OnPlaySoundEffect(string soundName)
    {
        _soundEffectPlayer.Stop();
        _soundEffectPlayer.Stream = GD.Load<AudioStream>($"res://Audio/{soundName}.wav");
        _soundEffectPlayer.Play();
    }

    public void OnMuteToggle(bool mute)
    {
        AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"), mute);
    }
}
