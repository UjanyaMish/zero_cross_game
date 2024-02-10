using Godot;
using System;

public partial class Tile : Node2D { };
public partial class Field : Node2D { };
public partial class Neko : IComparable { };
public partial class CardDeck : Node2D { }

public partial class neko1 : Area2D
{
	public Neko me;
	static bool mouse_free = true;
	private bool dragging = false;
	private bool dragable = false;
	private bool notdrag = false;

	AnimatedSprite2D anim_card;
	Tween tween;
	int usersteam = 0;

	Node2D something_field;
	public Tile anchor_field;

    private Vector2 offset; 

    public override void _Process(double delta)
    {
		if (Input.IsMouseButtonPressed(MouseButton.Left) && me.team == usersteam)
		{
			if (dragging && !notdrag) //уже происходит перетаскивание
			{
				Vector2 now = GetGlobalMousePosition();
				this.Position += now - offset;
				offset = now;
			}
			else if (dragable && mouse_free && (tween is null || !tween.IsRunning())) //проверяем что кота можно тащить, мышь свободна и нет анимации на коте
			{   //здесь мы начинаем тащить кота
				mouse_free = false;
				if (anchor_field is not null)
				{
					anchor_field.occupied = false;
				}
				dragging = true;
				offset = GetGlobalMousePosition();
			}
		}
		else //если не происходит нажатия на кнопку мыши
		{
			if (dragging) //если до этого тащили кота, то сейчас его отпускаем
			{
				dragging = false;
				mouse_free = true;
				if (something_field is not null) //проверка, есть ли кот на клетке поля или нет
				{
					Tile something_tile = something_field.GetParent() as Tile;
					if (something_tile.occupied) //клетка занята
					{

					}
					else if (!notdrag) //клетка свободна, ставим кота и оккупируем якорь
					{
						anchor_field = something_tile;
						me.x = anchor_field.x;
						me.y = anchor_field.y;
						GD.Print(me.x, me.y);
						GD.Print();
                        notdrag = true;
						me.notattack = false;
						anim_card.Play("ordinary_cat");
						GetNode<TextureProgressBar>("HP").Visible = true; //видимое HP
                        Neko.teams[me.team].Add(me);
                    }
				}
				Node2D prevParent = (Node2D)this.GetParent();
				if (anchor_field is not null) //добавляем кота к якорю
				{
					prevParent.RemoveChild(this); //удаление дочернего узла
					anchor_field.GetParent().AddChild(this); //добавление дочернего узла
					this.Position += prevParent.Position - ((Node2D)anchor_field.GetParent()).Position;
				}
				if (tween is not null && tween.IsRunning()) //прекращаем анимацию
				{
					tween.Stop();
				}
				tween = GetTree().CreateTween();
				tween.TweenProperty(this, "position", anchor_field.Position +
									new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out); //анимация возвращения кота на клетку
				anchor_field.occupied = true;
				Field.tiles.Remove(anchor_field);
                CardDeck.cardlist.Remove(me);
            }
		}
    }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anim_card = GetNode<AnimatedSprite2D>("AnimatedSprite2D"); //берем сцену
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

