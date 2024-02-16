using Godot;
using System.Linq;

public partial class settings_menu : Control
{	
	private ConfigFile config;
	
	public int Volume_Back_music;
	//Экспортирует ноду от которой будет приходить сигнал
	[Export]
	private Node control;

	//Экспортирует ресурс в котором хранятся настройки
	[Export]
	public Resource Settings;

	[Signal]
	public delegate void MusicVoloumeEventHandler(int newValue);

    public override void _Ready()
	{	

		Hide();
        //Вызывает настройки когда сигнал приходит из меню паузы
        if (control is pause_menu pauseMenu)
			pauseMenu.SettingsScreenRequsted += _on_settings_button_toggeled;
		//Вызывает настройки когда сигнал приходит от кнопки в главном меню
		if (control is settings_button settingsButton)
			settingsButton.SettingsScreenRequsted += _on_settings_button_toggeled;
		//Проверка на какой ресрс прекреплен
		/*if (Settings is SettingStorage settingStorage)
		{
			//Добавление значений при создании меню для консистентности между сценами
			HSlider effect = GetNode<HSlider>("TextureRect/VBoxContainer/HBoxContainer/VolumeEffects");
			effect.Value = settingStorage.Efects;
			HSlider music = GetNode<HSlider>("TextureRect/VBoxContainer/HBoxContainer3/VolumeMusic");
			music.Value = settingStorage.Muic;
			OptionButton resolution = GetNode<OptionButton>("TextureRect/VBoxContainer/BoxContainer/AspectButton");
			for (int i = 0; i < settingStorage.Resolution.Count() ; i++)
			{
				resolution.AddItem(settingStorage.Resolution[i],i);
			}
			resolution.Select(settingStorage.CurrentResolution);
			resolution.AllowReselect = true;
			OptionButton language = GetNode<OptionButton>("TextureRect/VBoxContainer/LanuageSection/LanguageSelect");
			for (int i = 0; i < settingStorage.Resolution.Count(); i++)
			{
				language.AddItem(settingStorage.Language[i], i);
			}
			language.Select(settingStorage.CurrentLanguage);
			language.AllowReselect = true;	
		}*/
		HSlider effect = GetNode<HSlider>("TextureRect/VBoxContainer/HBoxContainer/VolumeEffects");
		
		HSlider music = GetNode<HSlider>("TextureRect/VBoxContainer/HBoxContainer3/VolumeMusic");
		
		OptionButton resolution = GetNode<OptionButton>("TextureRect/VBoxContainer/BoxContainer/AspectButton");
		resolution.AddItem("1920*1080");
		resolution.AddItem("1260*720");
		
		OptionButton language = GetNode<OptionButton>("TextureRect/VBoxContainer/LanuageSection/LanguageSelect");		
		language.AddItem("Русский");
		language.AddItem("English");
			
		config = new ConfigFile();
		Error err = config.Load("res://settings.cfg");
		if (err != Error.Ok)//если файла не существует
		{
            config.SetValue("song", "whitch", 1);
            config.SetValue("backmusic", "voloume", (int)music.Value);
			config.SetValue("effects", "voloume", (int)effect.Value);
			config.SetValue("language", "choice", resolution.Selected);
			config.SetValue("resolution", "choice", language.Selected);		
			config.Save("res://settings.cfg");
		}
		else
		{
            var song = (int)config.GetValue("song", "whitch", 0);
			if (song == 1)
			{
				//EmitSignal(SignalName.ChangeSong, 1);
                config.SetValue("song", "whitch", 0);
            }
			else
			{
				//EmitSignal(SignalName.ChangeSong, 0);
                config.SetValue("song", "whitch", 1);
            }
            config.Save("res://settings.cfg");
            var voloume_ef = (int)config.GetValue("effects", "voloume",0);
			effect.Value = voloume_ef;
			var voloume_back = (int)config.GetValue("backmusic", "voloume",0);
			music.Value = voloume_back;
			var id_lang = (int)config.GetValue("language", "choice",0);
			language.Select(id_lang);
			var id_res = (int)config.GetValue("resolution", "choice",0);
            resolution.Select(id_res);
			if (id_lang == 0)
			{
				TranslationServer.SetLocale("ru");
			}
			else if (id_lang == 1)
			{
				TranslationServer.SetLocale("en");
			}

            Viewport viewport = GetViewport();

            // Получаем размеры окна просмотра (Viewport)
            Vector2 windowSize = viewport.GetVisibleRect().Size;
			Vector2 screenSize= viewport.GetVisibleRect().Size;

            Window window = this.GetTree().Root;
            if (id_res == 1)
            {
                window.Set("size", new Vector2((float)1280, (float)720));
                screenSize = new Vector2((float)1280, (float)720);

            }
            else if (id_res == 0)
            {
                window.Set("size", new Vector2((float)1920, (float)1080));
                screenSize = new Vector2((float)1920, (float)1080);
            }

            int x = 0;
            int y = 0;
            if (windowSize!= screenSize)
			{
                 x = (int)(windowSize.X / 2 - screenSize.X / 2);
                 y = (int)(windowSize.Y / 2 - screenSize.Y / 2);
            }
            window.Set("position", new Vector2(x, y));
		}
	}

	public override void _Process(double delta)
	{
	}

	//Выход из меню по кнопке
	private void _on_back_button_pressed()
	{
		Hide();
	}

	//Проверка состояния по сигналу
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

	//Сохранение значений в ресурс по сигналу измения значения
	public void _on_volume_effects_value_changed(float vol)
	{
		config.SetValue("effects", "voloume", (int)vol);
		config.Save("res://settings.cfg");
		/*if (Settings is SettingStorage settingStorage)
		{
			settingStorage.Efects = (int)vol;
		}*/
	}

	public void _on_volume_music_value_changed(float vol)
	{
		config.SetValue("backmusic", "voloume", (int)vol);
		config.Save("res://settings.cfg");
		Volume_Back_music=(int)vol;
		EmitSignal(SignalName.MusicVoloume, Volume_Back_music);
		/*if (Settings is SettingStorage settingStorage)
		{
			settingStorage.Muic = (int)vol;
		}*/
	}

	public void _on_aspect_button_item_selected(int id)
	{
        Viewport viewport = GetViewport();

        // Получаем размеры окна просмотра (Viewport)
        Vector2 windowSize = viewport.GetVisibleRect().Size;
        Vector2 screenSize = viewport.GetVisibleRect().Size;


        Window window = this.GetTree().Root;
        if (id == 1)
        {
            window.Set("size", new Vector2((float)1280, (float)720));
            screenSize = new Vector2((float)1280, (float)720);

        }
        else if (id == 0)
        {
            window.Set("size", new Vector2((float)1920, (float)1080));
            screenSize = new Vector2((float)1920, (float)1080);
        }

        int x = 0;
        int y = 0;
        if (windowSize != screenSize)
        {
            x = (int)(windowSize.X / 2 - screenSize.X / 2);
            y = (int)(windowSize.Y / 2 - screenSize.Y / 2);
        }
        window.Set("position", new Vector2(x, y));
    }

	public void _on_language_select_item_selected(int id)
	{
		config.SetValue("language", "choice", id);
		config.Save("res://settings.cfg");
		/*if (Settings is SettingStorage settingStorage)
		{
			settingStorage.CurrentLanguage = id;
		}*/
		if (id == 0)
		{
			TranslationServer.SetLocale("ru");
		}
		else if (id == 1)
		{
			TranslationServer.SetLocale("en");
        }
	}
}
