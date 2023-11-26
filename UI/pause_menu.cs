using Godot;

public partial class pause_menu : Control
{
    [Export]
    private game_controll_process gameControll;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Hide();
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

    private void _on_resume_button_pressed()
    {
        gameControll.GamePaused = false;
    }

    private void _on_returm_button_pressed()
    {
        gameControll.GamePaused = false;
        GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
    }


}
