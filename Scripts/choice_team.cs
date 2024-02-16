using Godot;
using System;

public partial class choice_team : Node2D
{
    public static int teamUser;
	public static choice_team me;
	public static bool onCat = false;

    //public bool team = false;
 //   private AudioStreamPlayer backgroundMusicPlayer;
	//private ConfigFile config;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
		//me = this;
		////запуск фоновой музыки
		//backgroundMusicPlayer = new AudioStreamPlayer();
		//AddChild(backgroundMusicPlayer);
		//backgroundMusicPlayer.Stream = (AudioStream)ResourceLoader.Load("res://sounds/Here_Come_The_Raindrops.mp3");
		//backgroundMusicPlayer.Play();
		
		////конфиг файл
		//config = new ConfigFile();
		//config.Load("res://settings.cfg");
		//var voloume = (int)config.GetValue("backmusic", "voloume", 0);
		//backgroundMusicPlayer.VolumeDb = voloume;
		//GD.Print(voloume);	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
