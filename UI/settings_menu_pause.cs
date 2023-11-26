using Godot;

public partial class settings_menu_pause : Control
{
    [Export]
    private pause_menu settingCallGame;

    public override void _Ready()
	{
		Hide();
        settingCallGame.SettingsScreenRequsted += _on_settings_button_toggeled;
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

}
