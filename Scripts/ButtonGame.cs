using Godot;
using System;

public partial class ButtonGame : Button
{
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
    Control select = GetNode<Control>("CanvasLayer/choice_team");
	select.Show();
	}	
}
