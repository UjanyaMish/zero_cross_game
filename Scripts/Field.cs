using Godot;
using System;

public partial class Field : Node2D
{
	[Signal]
	public delegate void IndexTileEventHandler(int i, int j);
	 
	PackedScene tile = ResourceLoader.Load<PackedScene>("res://Scenes/Tile.tscn");

	public override void _Ready()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
			Node2D t = (Node2D)tile.Instantiate();
			t.Position = new Vector2(i * 128, j * 96);
			AddChild(t);
			}
		}
		Archer a = new Archer(0, 0, 0, this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
