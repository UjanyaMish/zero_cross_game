using Godot;
using System;

public partial class SettingStorage : Resource
{
    //Создание полей для будущего ресурса
	[ExportGroup("Settings")]
	[Export]
	public int Volume { get; set; }
	[Export]
    public int Muic { get; set; }
	[Export]
    public int Efects { get; set; }
	[Export]
    public string[] Language { get; set; }
    [Export]
    public int CurrentLanguage {  get; set; }
    [Export]
    public string[] Resolution { get; set; }
    [Export]
    public int CurrentResolution { get; set; }

    //Дефолтные значения для Godot
    public SettingStorage() : this(0,0,0,null,0,null,0) { }

    //Конструктор для вызовов данных
	public SettingStorage(int volume, int muic, int efects, string[] language,int currentLanguage, string[] resolution,int currentResolution)
    {
        Volume = volume;
        Muic = muic;
        Efects = efects;
        Language = language ?? System.Array.Empty<string>();
        CurrentLanguage = currentLanguage;
        Resolution = resolution ?? System.Array.Empty<string>();
        CurrentResolution = currentResolution;
    }

}
