using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class Neko : IComparable
{
    public int HP = 10;
    public int x = 0;
    public int y = 0;
    public int damage = 0;
    public int rank = 0;
    public int priority = 0;
    public int created = 0;
    public int team;

    public Node2D unit;
    public TextureProgressBar HP_bar;

    public static SortedSet<Neko> listNeko_O = new();
    public static SortedSet<Neko> listNeko_X = new();
    public static SortedSet<Neko>[] teams = { listNeko_O, listNeko_X };
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
        unit.Position = new Vector2(x * 128 + 16, y * 96);
        place.AddChild(unit);

        switch (team)
        {
            case 0:
                listNeko_O.Add(this);
                break;
            case 1:
                listNeko_X.Add(this);
                break;
        }

        HP_bar = unit.GetNode<TextureProgressBar>("HP");
    }

    ~Neko()
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void ElevationOfRang()
    {
        int tipe = this.priority;

        foreach (Neko neko in teams[team])
        {
            if (neko.priority == tipe)
            {
                int disX = Math.Abs(neko.x - this.x);
                int disY = Math.Abs(neko.y - this.y);
                if (disX == 1 || disY == 1)
                {
                    this.rank += 1;
                    GD.Print("Rang neko from ", this.x, ",", this.y, " up:", this.rank);
                }
            }
        }
    }

    public virtual void DamageReceived(int damage)
    {
        HP -= damage;
        HP_bar.Value = HP;
        ResourceLoader.Load<PackedScene>("res://Scenes/ClawsAnimated.tscn");
        GD.Print("Neko from ", this.x, ", ", this.y, " get damage ", damage);
        if (HP <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        teams[team].Remove(this);
        unit.QueueFree();//убираем спрайт
        GD.Print("Neko from ", this.x, ", ", this.y, " death");
    }

    public int CompareTo(object obj)
    {
        if (obj is Neko neko) return priority.CompareTo(neko.priority);
        else return 0;
    }
}

public class Swordsman : Neko
{
    public Swordsman(int team, int x, int y, Node2D t) : base(1, 2, team, x, y, t, team)
    {
    }
    public override void Attack()
    {
        int HP_enemy = 100;
        Neko neko_attack = null;
        foreach (Neko neko_enemy in teams[(team + 1) % 2])
        {
            int disX = Math.Abs(neko_enemy.x - this.x);
            int disY = Math.Abs(neko_enemy.y - this.y);
            if (disX == 1 || disY == 1)
            {
                if (neko_enemy.HP < HP_enemy)
                {
                    HP_enemy = neko_enemy.HP;
                    neko_attack = neko_enemy;
                }
            }
        }
        if (neko_attack is not null) neko_attack.DamageReceived(this.damage + this.rank);
    }
}

public class Archer : Neko
{
    public Archer(int team, int x, int y, Node2D t) : base(2, 1, 2 + team, x, y, t, team)
    {
    }
    public override void Attack()
    {
        int sum = 0;
        int minsum = 100;
        int HP_enemy = 100;
        Neko neko_attack = null;
        foreach (Neko neko_enemy in teams[(team + 1) % 2])
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
        if (neko_attack is not null) neko_attack.DamageReceived(this.damage + this.rank);
    }
}

public partial class Game : Node2D
{

}