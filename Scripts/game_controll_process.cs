using Godot;
using System;

[GlobalClass]
public partial class game_controll_process : Node
{
    [Signal]
    public delegate void toggleGamePausedEventHandler(bool isPaused);

    [Export]
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
}
