using Godot;
using System;

public partial class CardDeck : Node2D { };

public partial class ButtonAttack : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_pressed()
	{
        if (true)
        {
            foreach (Neko neko in Neko.listNeko_O)
            {
                neko.ElevationOfRang();
                neko.Attack();
            }
            foreach (Neko neko in Neko.listNeko_X)
            {
                neko.ElevationOfRang();
                neko.Attack();
            }
            CardDeck.enemymove = true;
            this.GetParent().GetNode<mian_card_place>("MianCardPlace2").GetNode<CardDeck>("CardDeck");
        }
    }
}
