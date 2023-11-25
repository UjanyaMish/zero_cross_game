using Godot;
using System;

public partial class Field : Node2D
{
PackedScene tile = ResourceLoader.Load<PackedScene>("res://Scenes/Tile.tscn");

public override void _Ready()
{
for (int i = 0; i < 4; i++)
{
for (int j = 0; j < 4; j++)
{
Node2D t = (Node2D)tile.Instantiate();
t.Position = new Vector2(i * 64, j * 64);
AddChild(t);
}
}
}

// Called every frame. 'delta' is the elapsed time since the previous frame.
public override void _Process(double delta)
{
}
}
