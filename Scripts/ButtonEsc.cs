using Godot;
using System;

public partial class ButtonEsc : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_pressed()
	{
		GetTree().Quit();
        ConfigFile config = new ConfigFile();
		config.Load("res://settings.cfg");
        config.SetValue("song", "whitch", 1);
		config.Save("res://settings.cfg");
    }
}
