using Godot;
using System;

public partial class choice_team : Node2D {};
public partial class Button_play : Button
{
	choice_team team;
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
		//if(team.team)
		GD.Print(choice_team.teamUser);
		if (choice_team.teamUser != -1)GetTree().ChangeSceneToFile("res://Scenes/gameControllProcess.tscn");
	}
	
}



