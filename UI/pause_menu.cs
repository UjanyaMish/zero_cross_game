using Godot;

public partial class pause_menu : Control
{
	
	[Export]
	private game_controll_process gameControll;

	[Export]
	Node help;

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
			SettingCalled = false;
			if(help is Game game)
			{
				game.HelpWindow = false;
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
		GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
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

	private void _on_settings_button_pressed()
	{
		SettingCalled = true;
	}
}
