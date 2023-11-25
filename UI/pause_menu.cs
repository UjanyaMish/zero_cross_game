using Godot;

public partial class pause_menu : Control
{
    private game_controll_process gameControll;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Hide();
        gameControll = new();
        gameControll.toggleGamePaused += _onGameControlToggle_gamePaused;

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
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
        }
    }
}
