using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Neko
{
	public int HP = 10;
	public int x = 0;
	public int y = 0;
	public int damage = 0;
	public int rank = 1;
	public int priority = 0;
	public int created = 0;
	public int team;

	public Node2D unit;


	public static SortedSet<Neko> listNeko_O = new();
    public static SortedSet<Neko> listNeko_X = new();
    //static int CreatedAll = 0;

    static PackedScene[] sprites = 
	new PackedScene[4]{ResourceLoader.Load<PackedScene>("res://Scenes/neko_!0.tscn"), 
					   ResourceLoader.Load<PackedScene>("res://Scenes/neko_!x.tscn"), 
					   ResourceLoader.Load<PackedScene>("res://Scenes/neko_)0.tscn"), 
					   ResourceLoader.Load<PackedScene>("res://Scenes/neko_)x.tscn")};
	
	public Neko(int prior, int damag, int spritenum, int x, int y, Node2D place, int teamm)
	{
		this.team = teamm;
		this.x = x;
		this.y = y;
		priority = prior;
		damage = damag;
		unit = (Node2D)sprites[spritenum].Instantiate();
		unit.Position = new Vector2(x*128+16,y*96);
		place.AddChild(unit);
	}

	public virtual void DamageReceived()
	{ 

	}

    public virtual void Death(Neko neko_killed, int team)
    {

    }
}

public class Swordsman : Neko
{
	public Swordsman(int team, int x, int y, Node2D t) : base(1, 2, team, x, y, t, team)
	{
	}
    public virtual void Attack()
    {
        int HP_enemy = 100;
        Neko neko_attack = null;
        int team_enemy = 0;
        if (this.team == 1)
        {
            team_enemy = 2;
            foreach (Neko neko_enemy in listNeko_X)
            {
                if (neko_enemy.HP < HP_enemy)
                {
                    HP_enemy = neko_enemy.HP;
                    neko_attack = neko_enemy;
                }
            }
        }
        else if (this.team == 2)
        {
            team_enemy = 1;
            foreach (Neko neko_enemy in listNeko_O)
            {
                if (neko_enemy.HP < HP_enemy)
                {
                    HP_enemy = neko_enemy.HP;
                    neko_attack = neko_enemy;
                }
            }
        }
        neko_attack.HP -= this.damage;
        if (neko_attack.HP <= 0)
        {
            Death(neko_attack, team_enemy);
        }
    }
}

public class Archer : Neko
{
	public Archer(int team, int x, int y, Node2D t) : base(2, 1, 2 + team, x, y, t, team)
	{
	}
    public virtual void Attack()
    {
        int sum = 0;
        int minsum = 100;
        int HP_enemy = 100;
        Neko neko_attack = null;
        int team_enemy = 0;
        if (this.team == 1)
        {
            team_enemy = 2;
            foreach (Neko neko_enemy in listNeko_X)
            {
                sum = Math.Abs(this.x - neko_enemy.x) + Math.Abs(this.y - neko_enemy.y);
                if (neko_enemy.HP < HP_enemy)
                {
                    HP_enemy = neko_enemy.HP;
                    neko_attack = neko_enemy;
                }
                else if (neko_enemy.HP == HP_enemy && sum < minsum)
                {
                    minsum = sum;
                    neko_attack = neko_enemy;
                }
            }
        }
        else if (this.team == 2)
        {
            team_enemy = 1;
            foreach (Neko neko_enemy in listNeko_O)
            {
                sum = Math.Abs(this.x - neko_enemy.x) + Math.Abs(this.y - neko_enemy.y);
                if (neko_enemy.HP < HP_enemy)
                {
                    HP_enemy = neko_enemy.HP;
                    neko_attack = neko_enemy;
                }
                else if (neko_enemy.HP == HP_enemy && sum < minsum)
                {
                    minsum = sum;
                    neko_attack = neko_enemy;
                }
            }
        }
        neko_attack.HP -= this.damage;
        if (neko_attack.HP <= 0)
        {
            Death(neko_attack, team_enemy);
        }
    }
}

public partial class Game : Node2D
{
	
}
