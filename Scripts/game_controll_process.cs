using Godot;

public partial class game_controll_process : Node
{
	[Export]
	Node help;

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

    public override void _Input(InputEvent @event)
	{
		//base._Input(@event);
		if (@event.IsActionPressed("ui_cancel"))
		{
			GamePaused = !GamePaused;
		}
	}

	public override void _Ready()
	{
		if (help is Game game)
		{
			game.toggleGameHelp += _on_help_button_toggeled;
		}
	}

	public void _on_help_button_toggeled(bool called)
	{
		Control select = GetNode<Control>("CanvasLayer/HelpScreen");
		if (called)
		{
			select.Show();
			GamePaused = true;
		}
		else { select.Hide(); }
	}
}
