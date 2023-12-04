/* using Godot;
using System;

public partial class neko1 : Area2D
{
	[Signal]
	public delegate void DragUnitEventHandler(Vector2 DragOffset);
	
	private bool dragging = false;
	private Vector2 offset;
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton && 
			mouseButton.ButtonIndex == Godot.ButtonList.Left)
		{
			if (mouseButton.Pressed && IsMouseHovering())
			{
				dragging = true;
				offset = GlobalPosition - GetGlobalMousePosition();
			}
			else if (mouseButton.Release && dragging)
			{
				EmitSignal(SignalName.toggleGamePaused, offset);
				dragging = false;
			}
		}

		if (dragging && @event is InputEventMouseMotion mouseMotion)
		{
			GlobalPosition = mouseMotion.GlobalPosition + offset;
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
*/
