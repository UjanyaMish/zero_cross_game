using Godot;
using System;

public partial class CardDeck : Node2D
{
    public int x = 0;
    public int y = 0;
    public int counter = 0;

    private bool mouse_on_top = false;
    private bool lazy = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left) && mouse_on_top)
        {
            if (lazy)
            {
                Tween tween = GetTree().CreateTween();
                Neko newNeko = CreateCat(1, x, y);
                tween.TweenProperty(newNeko.unit, "position", newNeko.unit.Position +
                    new Vector2(200 + counter * 75, 200), 0.2f).SetEase(Tween.EaseType.Out);
                counter++;
                lazy = false;
            }
        }
        else
        {
            lazy = true;
        }
    }

    public void _on_mouse_entered()
    {
        mouse_on_top = true;
    }

    public void _on_mouse_exited()
    {
        mouse_on_top = false;
    }

    public Neko CreateCat(int team, int x, int y)
    {
        Archer nw = new(team, x, y, this);
        return nw;
    }
}
