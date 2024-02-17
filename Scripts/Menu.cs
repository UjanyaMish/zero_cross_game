using Godot;
using System;

public partial class VictoryScreen : Node2D { };
public partial class Menu : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Sprite2D backgroung = GetNode<Sprite2D>("CanvasLayer/ЗаФоном");
        var menuSize = Size;
        backgroung.Scale = new Vector2(1, 1);
        backgroung.Scale *= menuSize / (backgroung.Texture.GetSize());
        backgroung.Position = PivotOffset;
        GD.Print("in menu" + VictoryScreen.restart);
        if (VictoryScreen.restart == true)
        {
            Node2D select = GetNode<Node2D>("CanvasLayer/choice_team");
            select.Show();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void _on_button_game_pressed()
    {
        Node2D select = GetNode<Node2D>("CanvasLayer/choice_team");
        select.Show();
    }
}
