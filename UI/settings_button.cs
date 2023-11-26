using Godot;

public partial class settings_button : Button
{
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

    private void _on_pressed()
    {
        SettingCalled = !SettingCalled;
    }
}
