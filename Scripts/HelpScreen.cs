using Godot;
using System;

public partial class HelpScreen : Control
{
	[Export]
	Node node;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_button_pressed()
	{
		if (node is Game game)
		game.HelpWindow = false;
	}
}
