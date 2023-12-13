using Godot;
using System;

public partial class Tile : Node2D { };

public partial class Field : Node2D
{
	PackedScene tile = ResourceLoader.Load<PackedScene>("res://Scenes/Tile.tscn");

	public override void _Ready()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				Tile t = (Tile)tile.Instantiate();
				t.Position = new Vector2(i * 128, j * 96);
				t.x = i;
				t.y = j;
				AddChild(t);
			}
		}
		Archer a = new Archer(1, 0, 0, this);
        Archer b = new Archer(1, 0, 1, this);
    }

	/*public void CreateCat(int team, int x, int y)
	{
		Archer nw = new(team, x, y, this);
	}*/

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
