using Godot;
using System;

public partial class CardDeck : Node2D
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        /*if (Input.IsMouseButtonPressed(MouseButton.Left))
		{
            Tween tween = GetTree().CreateTween();
            CardDeck card_copy = CardDeck.Duplicate();
            GetParent().AddChild(card_copy);
            tween.TweenProperty(this, "position", card_copy.Position + new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out);

			//нужно как-то достать из будущих юнитов инфу, какие они будут (как минимум изображение), чтобы мы смогли
			//показывать игроку, какие юниты у него есть на картах. Плюс нужно добавить нормальную анимацию передвижения
			//с коллоды на планочку снизу (найти их позицию)
        }*/
    }
}
