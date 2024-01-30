using Godot;
using System.Linq;

public partial class settings_menu : Control
{
	//Экспортирует ноду от которой будет приходить сигнал
    [Export]
    private Node control;

	//Экспортирует ресурс в котором хранятся настройки
    [Export]
	public Resource Settings;

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
        if (Settings is SettingStorage settingStorage)
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
		if (Settings is SettingStorage settingStorage)
		{
			settingStorage.Efects = (int)vol;
		}
	}

    public void _on_volume_music_value_changed(float vol)
    {
        if (Settings is SettingStorage settingStorage)
        {
            settingStorage.Muic = (int)vol;
        }
    }

	public void _on_aspect_button_item_selected(int id)
	{
        if (Settings is SettingStorage settingStorage)
        {
			settingStorage.CurrentResolution = id;
        }
    }

    public void _on_language_select_item_selected(int id)
    {
        if (Settings is SettingStorage settingStorage)
        {
            settingStorage.CurrentLanguage = id;
        }
		if (id == 0)
		{
			TranslationServer.SetLocale("en");
		}
		else if (id == 1)
		{
			TranslationServer.SetLocale("ru");
		}
    }
}
