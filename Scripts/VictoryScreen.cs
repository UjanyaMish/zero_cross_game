using Godot;
using System;

public partial class VictoryScreen : Node2D
{
    public static bool restart;
    public bool result = false;
    public Label label;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Hide();
        label = GetNode<Label>("Panel/WinOrLose");
        GD.Print("in vc screen" + restart);
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

    public void _on_button_attack_game_over(bool victory)
    {
        bool result = victory;
        GD.Print("signal emited");
        if (result)
        {
            Show();
            label.Text = "Victory!";
        }
        if (!result)
        {
            Show();
            label.Text = "Fail!";
        }
    }
}
