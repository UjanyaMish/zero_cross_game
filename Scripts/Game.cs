using Godot;
using static Godot.GodotThread;
using System.Collections.Generic;
using System;
using static Godot.HttpRequest;
using System.Reflection.Emit;

public partial class Neko : IComparable //описание класса юнитов
{
    public int HP = 10;
    public int x = 0;
    public int y = 0;
    public int damage = 0;
    public int rank = 0;
    public int priority = 0;
    public int created = 0;
    public int team;
    public bool notattack = true;

    public Node2D unit;
    public TextureProgressBar HP_bar;
    //AnimatedSprite2D damage_anim;

    public static List<Neko> listNeko_O = new();
    public static List<Neko> listNeko_X = new();
    public static List<Neko>[] teams = { listNeko_O, listNeko_X };
    //static int CreatedAll = 0;

    static PackedScene[] sprites =
    new PackedScene[4]{ResourceLoader.Load<PackedScene>("res://Scenes/neko_!0.tscn"),
                       ResourceLoader.Load<PackedScene>("res://Scenes/neko_!x.tscn"),
                       ResourceLoader.Load<PackedScene>("res://Scenes/neko_)0.tscn"),
                       ResourceLoader.Load<PackedScene>("res://Scenes/neko_)x.tscn")};

    public Neko(int prior, int damag, int spritenum, int x, int y, Node2D place, int teamm) //описание класса юнитов
    {
        this.team = teamm;
        this.x = x;
        this.y = y;
        priority = prior;
        damage = damag;
        unit = (Node2D)sprites[spritenum].Instantiate();
        unit.Position = new Vector2(x * 128 + 16, y * 96);
        ((neko1)unit).me = this;
        place.AddChild(unit);

        /*switch (team)
        {
            case 0:
                listNeko_O.Add(this);
                break;
            case 1:
                listNeko_X.Add(this);
                break;
        }*/

        HP_bar = unit.GetNode<TextureProgressBar>("HP");
    }

    ~Neko()
    {

    }

    public virtual void Attack() //функция атаки
    {

    }

    public virtual void ElevationOfRang() //функция возведения в ранг
    {
        int countRank = CountNekoRank(this);

        if (countRank <= 0)
        {
            this.rank = 1;
        }
        else
        {
            this.rank = countRank;
            GD.Print("Rang neko from ", this.x, ",", this.y, " up:", this.rank);
        }
    }

    public virtual int CountNekoRank(Neko thisNeko)
    {
        int coutcat = 0;
        int coutcatX = 0;
        int coutcatY = 0;
        int coutcatZ = 0;
        int tipe = thisNeko.priority;
        bool flag = false;

        //проверка по Х
        for (int i = thisNeko.x - 1; i >= 0; i--)
        {
            if (flag)
            {
                break;
            }
            foreach (Neko neko in teams[team])
            {
                if (neko.x == i && neko.y == thisNeko.y)
                {
                    if (!neko.notattack && neko.priority == tipe)
                    {
                        coutcatX += 1;
                    }
                }
                else
                {
                    flag = true;
                    break;
                }
            }
        }
        for (int i = thisNeko.x + 1; i <= 5; i++)
        {
            if (flag)
            {
                break;
            }
            foreach (Neko neko in teams[team])
            {
                if (neko.x == i && neko.y == thisNeko.y)
                {
                    if (!neko.notattack && neko.priority == tipe)
                    {
                        coutcatX += 1;
                    }
                }
                else
                {
                    flag = true;
                    break;
                }
            }
        }


        //проверка по Y
        for (int i = thisNeko.y - 1; i >= 0; i--)
        {
            if (flag)
            {
                break;
            }
            foreach (Neko neko in teams[team])
            {
                if (neko.y == i && neko.x == thisNeko.x)
                {
                    if (!neko.notattack && neko.priority == tipe)
                    {
                        coutcatY += 1;
                    }
                }
                else
                {
                    flag = true;
                    break;
                }
            }
        }
        for (int i = thisNeko.y + 1; i <= 3; i++)
        {
            if (flag)
            {
                break;
            }
            foreach (Neko neko in teams[team])
            {
                if (neko.y == i && neko.x == thisNeko.x)
                {
                    if (!neko.notattack && neko.priority == tipe)
                    {
                        coutcatY += 1;
                    }
                }
                else
                {
                    flag = true;
                    break;
                }
            }
        }

        for (int i = thisNeko.x - 1; i >= 0; i--)
        {
            int j = thisNeko.y - 1;

            if (flag)
            {
                break;
            }
            foreach (Neko neko in teams[team])
            {
                if (neko.x == i && neko.y == j)
                {
                    if (!neko.notattack && neko.priority == tipe)
                    {
                        coutcatZ += 1;
                    }
                }
                else
                {
                    flag = true;
                    break;
                }
            }
        }
        for (int i = thisNeko.x + 1; i <= 5; i++)
        {
            int j = thisNeko.y + 1;

            if (flag)
            {
                break;
            }
            foreach (Neko neko in teams[team])
            {
                if (neko.x == i && neko.y == j)
                {
                    if (!neko.notattack && neko.priority == tipe)
                    {
                        coutcatZ += 1;
                    }
                }
                else
                {
                    flag = true;
                    break;
                }
            }
        }
        coutcat += (coutcatX - 1) + (coutcatY - 1) + (coutcatZ - 1);

        return coutcat;
    }

    public virtual void DamageReceived(int damage) //функция получения урона
    {
        ((neko1)(this.unit)).anim_card.Play("claws");
        HP -= damage;
        HP_bar.Value = HP;
        GD.Print("Neko from ", this.x, ", ", this.y, " get damage ", damage);
        if (HP <= 0)
        {
            Death();
        }
    }

    public bool dead = false;

    public virtual void Death() //функция смерти кота
    {
        this.notattack = true;
        teams[team].Remove(this);
        GD.Print("Neko from ", this.x, ", ", this.y, " death");
        ((neko1)(this.unit)).anchor_field.occupied = false;
        Field.tiles.Add(((neko1)(this.unit)).anchor_field);

        dead = true;
    }

    public int CompareTo(object obj) //сравнивает котов, используется в SprtedSet
    {
        if (obj is Neko neko) return priority.CompareTo(neko.priority);
        else return 0;
    }
}

public class Swordsman : Neko //описание мечника
{
    public Swordsman(int team, int x, int y, Node2D t) : base(1, 2, team, x, y, t, team)
    {
    }
    public override void Attack()
    {
        int HP_enemy = 1000;
        Neko neko_attack = null;
        foreach (Neko neko_enemy in teams[(team + 1) % 2])
        {
            if (!neko_enemy.notattack)
            {
                int disX = Math.Abs(neko_enemy.x - this.x);
                int disY = Math.Abs(neko_enemy.y - this.y);
                if ((disX <= 1) && (disY <= 1))
                {
                    if (neko_enemy.HP < HP_enemy)
                    {
                        HP_enemy = neko_enemy.HP;
                        neko_attack = neko_enemy;
                    }
                }
            }
        }
        if (neko_attack is not null) neko_attack.DamageReceived(this.damage + this.rank);
    }
}

public class Archer : Neko //описание лучника
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
            if (!neko_enemy.notattack)
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
        if (neko_attack is not null) neko_attack.DamageReceived(this.damage + this.rank);
    }
}

public partial class Game : Node2D
{
    private AudioStreamPlayer backgroundMusicPlayer;
	private ConfigFile config;
	
	public override void _Ready()	
	{
        this.GetNode<mian_card_place>("MianCardPlace2").GetNode<CardDeck>("CardDeck").MyReady();
		//запуск фоновой музыки
		//backgroundMusicPlayer = new AudioStreamPlayer();
		//AddChild(backgroundMusicPlayer);
		//backgroundMusicPlayer.Stream = (AudioStream)ResourceLoader.Load("res://sounds/Here_Come_The_Raindrops.mp3");
		//backgroundMusicPlayer.Play();
		
		//конфиг файл
		//config = new ConfigFile();
		//config.Load("res://settings.cfg");
		//var voloume = (int)config.GetValue("backmusic", "voloume", 0);
		//backgroundMusicPlayer.VolumeDb = voloume;
		//GD.Print(voloume);				
	}
}
