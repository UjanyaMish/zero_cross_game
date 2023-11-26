using Godot;

public partial class game_controll_process : Node
{
    [Signal]
    public delegate void MenuScreenToggledEventHandler(bool isMenueOpend);

    public bool menuScreenToggle = true;

    public bool MenuScreenToggle
    {
        get
        {
            return menuScreenToggle;
        }
        set
        {
            menuScreenToggle = value;
            GetTree().Paused = menuScreenToggle;
            EmitSignal(SignalName.MenuScreenToggled, menuScreenToggle);
        }
    }

    [Signal]
    public delegate void toggleGamePausedEventHandler(bool isPaused);

    public bool gamePaused = false;
    public bool GamePaused
    {
        get
        {
            return gamePaused;
        }
        set
        {
            gamePaused = value;
            GetTree().Paused = gamePaused;
            EmitSignal(SignalName.toggleGamePaused, gamePaused);
        }
    }

    [Signal]
    public delegate void SettingsScreenRequstedEventHandler(bool isSettingCalled);

    public bool settingCalled = false;
    public bool SettingCalled
    {
        get
        {
            return settingCalled;
        }
        set
        {
            settingCalled = value;
            EmitSignal(SignalName.SettingsScreenRequsted, settingCalled);
        }
    }

    public override void _Input(InputEvent @event)
    {
        //base._Input(@event);
        if (@event.IsActionPressed("ui_cancel"))
        {
            if (!menuScreenToggle)
                GamePaused = !GamePaused;
        }
    }

}
