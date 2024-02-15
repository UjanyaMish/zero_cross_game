using Godot;
using System;

public partial class EmptyPlace : CollisionShape2D
{
    public int x = 4;
    public int y = 4;
    public bool occupied;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
