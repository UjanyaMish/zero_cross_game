using Godot;
using System;

public partial class choice_team : Node2D {};

public partial class cat_o_choose : TextureRect
{
	choice_team team;
	
	public bool if_entered=false;
	public bool if_pressed=false;
	//check of mouse on texturerect
	
	private Vector2 newPosition  = new Vector2(610, 250);
	private Vector2 newSize  = new Vector2(150, 150);
	//position and size of zoom caat
	
	private Vector2 oldPosition  = new Vector2(628, 271);
	private Vector2 oldSize  = new Vector2(97, 97);
	//position and zoom of normal cat
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!if_pressed)
		{
			if(if_entered)
			{
				this.SetSize(newSize);
				this.Position=newPosition;
			}
			else
			{
				this.SetSize(oldSize);
				this.Position=oldPosition;
			}
		}
		if (Input.IsMouseButtonPressed(MouseButton.Left))
		{
			if_pressed=!if_pressed;
			//if(if_pressed) team.team=false;
		}
	}
	
	private void _on_mouse_entered()
	{
		if_entered=true;
	}

	private void _on_mouse_exited()
	{
		if_entered=false;
	}
	
}



