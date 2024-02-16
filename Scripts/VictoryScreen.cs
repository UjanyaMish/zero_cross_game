using Godot;
using System;

public partial class VictoryScreen : Node2D
{
    [Export]
    Node node;

    public bool result = false;
    public Label label;

    //----------------------------------------------------------
    //[Signal]
    //public delegate void GameOverEventHandler(bool game_over);
    //EmitSignal(SignalName.GameOver, true);
    //----------------------------------------------------------

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Hide();
        label = GetNode<Label>("Panel/WinOrLose");
        //if (node is settings_menu game)
        //{
            //game.GameOver += _on_game_itog;
        //}
        //else GD.Print("Node is null");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
    public void _on_game_itog(bool win_or_lose)
    {
        result = win_or_lose;
        GD.Print("signal emited");
        if (result)
        {
            Show();
            label.Text = "Victory!";
        }
        if (!result)
        {
            Show();
            label.Text = "Fail";
        }
    }
}
