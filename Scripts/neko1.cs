using Godot;
using System;

public partial class Tile : Node2D { };
public partial class Field : Node2D { };

public partial class neko1 : Area2D
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
				Tween tween = GetTree().CreateTween();
				dragging = false;
				mouse_free = true;
				if (something_field is not null)
				{
					Tile something_tile = something_field.GetParent() as Tile;
					if (something_tile.occupied)
					{
						;
					}
					else
					{
						anchor_field = something_tile;
                    }
				}
				tween.TweenProperty(this, "position", anchor_field.Position +
									new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out);
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

