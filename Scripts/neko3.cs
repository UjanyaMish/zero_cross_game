using Godot;
using System;

public partial class Tile : Node2D { };
public partial class Field : Node2D { };

public partial class neko3 : Area2D
{
    static bool mouse_free = true;
    private bool dragging = false;
    private bool dragable = false;

    Node2D something_field;
    Tile anchor_field;

    private Vector2 offset;

    public override void _Process(double delta)
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            if (dragging)
            {
                Vector2 now = GetGlobalMousePosition();
                this.Position += now - offset;
                offset = now;
            }
            else if (dragable && mouse_free)
            {
                mouse_free = false;
                if (anchor_field is not null)
                {
                    anchor_field.occupied = false;
                }
                dragging = true;
                offset = GetGlobalMousePosition();
            }
        }
        else
        {
            if (dragging)
            {
                Tween tween = GetTree().CreateTween(); //годотовская переменная для анимации
                dragging = false;
                mouse_free = true;
                if (something_field is not null)
                {
                    Tile something_tile = something_field.GetParent() as Tile;
                    if (something_tile.occupied)
                    {
                    }
                    else
                    {
                        anchor_field = something_tile;
                    }
                }
                tween.TweenProperty(this, "position", anchor_field.Position +
                                    new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out); //анимация возвращения кота на клетку
                anchor_field.occupied = true;
                //((Field)(anchor_field.GetParent())).CreateCat(1, anchor_field.x, anchor_field.y);
            }
        }
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void _on_mouse_entered()
    {
        dragable = true; //когда мышь нажата и мы можем тащить
    }

    public void _on_mouse_exited()
    {
        dragable = false; //мышь отпущена, не можем тащить
    }

    public void _on_body_entered(Node2D other) //кот зашел в клетку
    {
        if (other.IsInGroup("dropplace")) //проверка кота на группу
        {
            something_field = other; //приравниваем ссылку на кота в клетку
        }
    }

    public void _on_body_exited(Node2D other) //кот вышел из клетки
    {
        if (other == something_field) //если была связь с котом, стираем связь
        {
            something_field = null;
        }
    }
}

