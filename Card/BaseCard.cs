using Godot;
using System;

public partial class BaseCard : MarginContainer
{
	//Neko(int prior, int damag, int spritenum, int x, int y, Node2D place, int teamm)
	//Swordsman(int team, int x, int y, Node2D t) : base(1, 2, team, x, y, t, team)
	//Archer(int team, int x, int y, Node2D t) : base(2, 1, 2 + team, x, y, t, team)
	public string typeOfCat = "Archer";
	public int helthOfCat = 10;
	public int rangeOfCat = 2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Sizing the textures
	    Sprite2D border = GetNode<Sprite2D>("CardBorder");
		var cardSize = Size;
		border.Scale *= cardSize / (border.Texture.GetSize());
		Sprite2D back = GetNode<Sprite2D>("CardBackground");
		back.Scale *= cardSize / (back.Texture.GetSize());
		//Changing text on card to values
		Label bars = GetNode<Label>("Bars/CatType");
		bars.Text = typeOfCat;
		Label helth = GetNode<Label>("Bars/Info/Helth");
		helth.Text = helthOfCat.ToString();
		Label range = GetNode<Label>("Bars/Info/Range");
		range.Text = rangeOfCat.ToString();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
