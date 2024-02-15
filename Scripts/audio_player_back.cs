using Godot;
using System;


public partial class audio_player_back : Node2D
{
	[Export]
	Node node;

    public AudioStreamPlayer2D backgroundMusicPlayer;	
	private ConfigFile config;
	public string song_back= "res://sounds/back_music.mp3";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Node node1 = GetNode<Node>(".");
        if (node1 != null)
        {
            //node1.MusicSong += _on_music_changed_song;
            GD.Print("Begin music: ", song_back);  
        }



        backgroundMusicPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        if (node is settings_menu Music)
        {
            Music.MusicVoloume += _on_music_changed;
            backgroundMusicPlayer.Stream = (AudioStream)ResourceLoader.Load(song_back);
            backgroundMusicPlayer.Play();
            backgroundMusicPlayer.Autoplay = true;
        }
		//конфиг файл
		config = new ConfigFile();
		config.Load("res://settings.cfg");
		var voloume = (int)config.GetValue("backmusic", "voloume",0);
		backgroundMusicPlayer.VolumeDb = voloume;
        
        //GD.Print("music voloume" + voloume);		

    }

	public void _on_music_changed(int voloume)
	{
		backgroundMusicPlayer.VolumeDb = voloume;
    }
    public void _on_music_changed_song(string song)
    {
        song_back = song;
    }
}
