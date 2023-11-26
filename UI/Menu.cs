using Godot;
using System;

public partial class Menu : Control
{
    [Export]
    private game_controll_process gameControll;

    public override void _Ready()
	{
        gameControll.MenuScreenToggled += _on_menu_screen_opend;
    }

	public override void _Process(double delta)
	{
	}

    public void _on_menu_screen_opend(bool isMenueOpend)
    {
        if (isMenueOpend)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void _on_button_setting_pressed()
	{
        gameControll.SettingCalled = true;
    }

	private void _on_button_game_pressed()
	{
		gameControll.MenuScreenToggle = false;

    }

	private void _on_button_esc_pressed()
	{
		GetTree().Quit();
	}
}
