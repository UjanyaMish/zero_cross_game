using Godot;
using System;

public class Neko
{
	public int HP = 10;
	public int x = 0;
	public int y = 0;
	public int damage = 0;
	public int rank = 1;
	public int priority = 0;
	public int created = 0;
	
	public Node2D unit;
	
	//static int CreatedAll = 0;
	
	static PackedScene[] sprites = 
	new PackedScene[4]{ResourceLoader.Load<PackedScene>("res://Scenes/neko_!0.tscn"), 
					   ResourceLoader.Load<PackedScene>("res://Scenes/neko_!x.tscn"), 
					   ResourceLoader.Load<PackedScene>("res://Scenes/neko_)0.tscn"), 
					   ResourceLoader.Load<PackedScene>("res://Scenes/neko_)x.tscn")};
	
	public Neko(int prior, int damag, int spritenum, int x, int y, Node2D place)
	{
		this.x = x;
		this.y = y;
		priority = prior;
		damage = damag;
		unit = (Node2D)sprites[spritenum].Instantiate();
		unit.Position = new Vector2(x*128+16,y*96);
		place.AddChild(unit);
	}
	
	public virtual void Attack()
	{
		
	}

	public virtual void DamageReceived()
	{ 

	}

    public virtual void Death()
    {

    }
}

public class Swordsman : Neko
{
	public Swordsman(int team, int x, int y, Node2D t) : base(1, 2, team, x, y, t)
	{
	}
}

public class Archer : Neko
{
	public Archer(int team, int x, int y, Node2D t) : base(2, 1, 2 + team, x, y, t)
	{
	}
}

public partial class Game : Node2D
{
	
}
