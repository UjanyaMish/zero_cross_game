using Godot;
using System;

public partial class Menu : Control
{
    [Export]
    private game_controll_process gameControll;

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	
	private void _on_settings_button_pressed()
	{
        gameControll.SettingCalled = !gameControll.SettingCalled;
    }
}
