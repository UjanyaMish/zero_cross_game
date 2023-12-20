using Godot;
using System;

public partial class Tile : Node2D { };
public partial class Field : Node2D { };

public partial class neko1 : Area2D
{
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
			else if (dragable)
			{
				dragging = true;
				offset = GetGlobalMousePosition();
            }
        }
		else
		{
			if (dragging)
            {
                Tween tween = GetTree().CreateTween();
                if (true)//(something_field.occupied == false)
				{
                    dragging = false;
                    if (something_field is not null)
                    {
                        anchor_field = (Tile)something_field.GetParent();
                    }
                    tween.TweenProperty(this, "position", anchor_field.Position +
                                        new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out);
					//((Field)(anchor_field.GetParent())).CreateCat(1, anchor_field.x, anchor_field.y);
					//something_field.occupied == true;
                }
				else
				{
                    tween.TweenProperty(this, "position", anchor_field.Position +
                                        new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out);
                }
            }
		}
    }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public void _on_mouse_entered()
	{
		dragable = true;
	}

	public void _on_mouse_exited()
	{
		dragable = false;
	}

	public void _on_body_entered(Node2D other)
    {
        if (other.IsInGroup("dropplace"))
		{
			something_field = other;
		}
	}

	public void _on_body_exited(Node2D other)
	{
		if (other == something_field)
		{
			something_field = null;
		}
	}
}

