using Godot;

public partial class pause_menu : Control
{
    
    [Export]
    private game_controll_process gameControll;

    public override void _Ready()
    {
        Hide();
        gameControll.toggleGamePaused += _onGameControlToggle_gamePaused;

    }

    public override void _Process(double delta)
    {
    }

    public void _onGameControlToggle_gamePaused(bool isPaused)
    {
        if (isPaused)
        {
            Show();
        }
        else
        {
            Hide();
            if (gameControll.settingCalled)
            {
                gameControll.SettingCalled = false;
            }
        }
    }

    private void _on_resume_button_pressed()
    {
        gameControll.GamePaused = false;
    }

    private void _on_returm_button_pressed()
    {
        gameControll.GamePaused = false;
        gameControll.MenuScreenToggle = true;
    }

    private void _on_settings_button_pressed()
    {
        gameControll.SettingCalled = !gameControll.SettingCalled;
    }
}
