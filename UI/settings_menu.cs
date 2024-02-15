using Godot;

public partial class settings_menu : Control
{
	[Export]
	private settings_button settingCallGame;
	
	private AudioStreamPlayer backgroundMusicPlayer;
	private HSlider volumeSlider;
	private ConfigFile config;
	
	public override void _Ready()
	{
		Hide();
		settingCallGame.SettingsScreenRequsted += _on_settings_button_toggeled;
		
		//запуск фоновой музыки
		backgroundMusicPlayer = new AudioStreamPlayer();
		AddChild(backgroundMusicPlayer);
		backgroundMusicPlayer.Stream = (AudioStream)ResourceLoader.Load("res://sounds/Here_Come_The_Raindrops.mp3");
		backgroundMusicPlayer.Play();
		
		//слайдер фоновой музыки
		volumeSlider = GetNode<HSlider>("back_music");

		//конфиг файл
		config = new ConfigFile();

		Error err = config.Load("res://settings.cfg");
		if (err != Error.Ok)//если файла не существует
		{
			config.SetValue("backmusic", "voloume", (int)volumeSlider.Value);
			config.Save("res://settings.cfg");
			backgroundMusicPlayer.VolumeDb = (int)volumeSlider.Value;
		}
		else
		{
			var voloume = (int)config.GetValue("backmusic", "voloume",0);
			backgroundMusicPlayer.VolumeDb = voloume;
			GD.Print(voloume);
			//?????????????
			//var f = (volumeSlider.Value);
			//GD.Print(f);
			//?????????????
		}				
	}

	public override void _Process(double delta)
	{
	}
	private void _on_back_button_pressed()
	{
		Hide();
	}
	public void _on_settings_button_toggeled(bool isSettingCalled)
	{
		if (isSettingCalled)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}
	private void _on_h_slider_value_changed(int value)
	{
		config.SetValue("backmusic", "voloume", (int)value);
		config.Save("res://settings.cfg");
		backgroundMusicPlayer.VolumeDb = value;
	}	
}

