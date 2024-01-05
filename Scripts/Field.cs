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
				Tile til = (Tile)tile.Instantiate();
				til.Position = new Vector2(i * 128, j * 96);
				til.x = i;
				til.y = j;
				til.occupied = false;
				AddChild(til);
			}
		}
		Archer a = new Archer(0, 0, 0, this);
        Archer b = new Archer(1, 2, 0, this);
		Swordsman c = new Swordsman(1, 3, 2, this);
        Swordsman d = new Swordsman(0, 2, 2, this);
    }

	public void CreateCat(int team, int x, int y)
	{
		Archer nw = new(team, x, y, this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
