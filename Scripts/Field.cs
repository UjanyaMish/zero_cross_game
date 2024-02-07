using Godot;
using System;
using System.Collections.Generic;

public partial class Tile : Node2D { };

public partial class Field : Node2D
{
    PackedScene tile = ResourceLoader.Load<PackedScene>("res://Scenes/Tile.tscn");

    public static List<Tile> tiles = new List<Tile>();

    public override void _Ready()
    {
        for (int i = 0; i < 6; i++) //создаем поле из клеток
        {
            for (int j = 0; j < 4; j++)
            {
                Tile til = (Tile)tile.Instantiate();
                til.Position = new Vector2(i * 164, j * 164);
                til.x = i;
                til.y = j;
                til.occupied = false;
                AddChild(til);
                tiles.Add(til); //добавляем в список каждую клетку поля
            }
        }
        Archer a = new Archer(0, 1, 3, this);
        //Archer b = new Archer(1, 2, 0, this);
        Swordsman c = new Swordsman(0, 2, 1, this); //добавляем на поле юнитов (пока временно)
        //Swordsman d = new Swordsman(0, 2, 2, this);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }
}
