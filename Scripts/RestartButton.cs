using Godot;
using System;

public partial class VictoryScreen : Node2D { };
public partial class RestartButton : Button
{

    public static bool restart = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
    private void _on_pressed()
    {
        VictoryScreen.restart = true;
        GD.Print("in bt" + VictoryScreen.restart);
        GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
    }
}
