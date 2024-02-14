using Godot;
using System;
using System.Collections.Generic;

public partial class Tile : Node2D { };

public partial class Field : Node2D { };

public partial class Neko : IComparable { };

public partial class CardDeck : Node2D
{
    /*
    //Preloding cards and starting hand
    PackedScene baseCard = ResourceLoader.Load<PackedScene>("res://Card/BaseCard.tscn");
    //Node playerHand = ResourceLoader.Load<Node>("res://Card/PlayerHand.cs");
    Vector2 cardSize = new Vector2(125, 175);
    string unit = "Swordsman";
    string[] cardList = { "Swordsman", "Swordsman", "Archer", "Archer" };
    int deckSize = 0;
    */

    bool isEnemy;
    Tile cardplace;

    List<int> cardlistUsers = new List<int> { 1, 1, 1, 1, 2, 2, 2, 2 };
    List<int> cardlistEnemy = new List<int> { 1, 1, 1, 1, 2, 2, 2, 2 };
   //List<int> listteam[] = { cardlistUsers, cardlistEnemy };

    public static List<Neko> armlistUsers = new List<Neko> { };
    List<Neko> armlistEnemy = new List<Neko> { };

    public int x = -2;
    public int y = -2;
    public int counter = 0;
    int team = 0;
    int teamEnemy = 1;
    public static bool enemymove = true;
    public static bool usersmove = false;
    public static bool flag = false;

    private bool mouse_on_top = false;
    private bool lazy = true;

    Random rand = new Random();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //anim_card = GetNode<AnimatedSprite2D>("AnimatedSprite2D"); //берем сцену
        cardplace = GetParent().GetNode<Node>("EmpPlace").GetNode<Tile>("EmptyPlace"); //взяли сцену
        isEnemy = this.GetParent().IsInGroup("Enemy");
    }

    public void MyReady()
    {
        EnemyMove();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (enemymove && isEnemy)
        {
            EnemyMove();
        }
        if (usersmove && !isEnemy)
        {
            UsersMove();
        }
    }

    public void UsersMove() //ход пользователя
    {
        int count = armlistUsers.Count;
        if (count < 4 && cardlistUsers.Count > 0)
        {
            for (int i = 0; i < 4 - count ; i++)
            {
                GetCard(team);
            }
        }
        if (flag)
        {
            usersmove = false;
            flag = false;
        }
    }

    public void EnemyMove() //ход компьютера
    {
        int count = armlistEnemy.Count;
        if (count < 4 && cardlistEnemy.Count > 0)
        {
            for (int i = 0; i < 4 - count; i++)
            {
                GetCard(teamEnemy);
            }
        }

        if (armlistEnemy.Count > 0)
        {
            Tile anchortile = null;
            Neko nekocard = armlistEnemy[rand.Next(0, armlistEnemy.Count)];

            if (Field.tiles.Count == 24) //если никого нет на поле
            {
                anchortile = Field.tiles[rand.Next(0, Field.tiles.Count)];
            }
            else
            {
                //если мечник
                if (nekocard.damage == 2)
                {
                    bool flag = false;
                    foreach (Tile tile in Field.tiles)
                    {
                        foreach (Neko neko_enemy in Neko.teams[team])
                        {
                            int disX = Math.Abs(neko_enemy.x - tile.x);
                            int disY = Math.Abs(neko_enemy.y - tile.y);
                            if (disX <= 1 && disY <= 1)
                            {
                                anchortile = tile;
                                flag = true;
                                break;
                            }
                        }
                        if (flag) break;
                    }
                    if (!flag)
                    {
                        int ind = rand.Next(0, Field.tiles.Count);
                        anchortile = Field.tiles[ind];
                        GD.Print("swordsman ", anchortile, " ", ind);
                    }
                }
                //если лучник
                else
                {
                    int sum;
                    int minsum = 1000;
                    int maxsum = 0;
                    foreach (Tile tile in Field.tiles)
                    {
                        minsum = 1000;
                        foreach (Neko neko_enemy in Neko.teams[team])
                        {
                            sum = Math.Abs(tile.x - neko_enemy.x) + Math.Abs(tile.y - neko_enemy.y);
                            if (sum < minsum)
                            {
                                minsum = sum;
                            }
                        }
                        if (minsum > maxsum)
                        {
                            maxsum = minsum;
                            anchortile = tile;
                        }
                    }
                    if (maxsum == 0)
                    {
                        int ind = rand.Next(0, Field.tiles.Count);
                        anchortile = Field.tiles[ind];
                        GD.Print("archer ", anchortile, " ", ind);
                    }
                }
            }
            if (anchortile is not null) //добавляем кота к якорю
            {
                nekocard.x = anchortile.x;
                nekocard.y = anchortile.y;
                Tween tween = GetTree().CreateTween();

                Node2D prevParent = (Node2D)nekocard.unit.GetParent();
            
                prevParent.RemoveChild(nekocard.unit); //удаление дочернего узла
                anchortile.GetParent().AddChild(nekocard.unit); //добавление дочернего узла
                nekocard.unit.Position += prevParent.Position - ((Node2D)anchortile.GetParent()).Position;

                nekocard.notattack = false;

                tween.TweenProperty(nekocard.unit, "position", anchortile.Position + new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out);
                Neko.listNeko_X.Add(nekocard);
                Field.tiles.Remove(anchortile);
                armlistEnemy.Remove(nekocard);
                ((neko1)(nekocard.unit)).anim_card.Play("ordinary_cat");
                ((neko1)(nekocard.unit)).GetNode<TextureProgressBar>("HP").Visible = true; //видимое HP
            }
        }
        enemymove = false;
        usersmove = true;
    }

    //public void UsersMove() //ход игрока
    //{
    //    if (Input.IsMouseButtonPressed(MouseButton.Left) && mouse_on_top && cardlistUsers.Count != 0 && !isEnemy) //при нажатии
    //    {
    //        GetCard(team);
    //    }
    //    else
    //    {
    //        lazy = true;
    //    }
    //    usersmove = false;
    //}

    public void GetCard(int team) //добавляяет карту в рукав
    {
        Node2D newcardplace = (Node2D)cardplace.GetParent().Duplicate();
        //newcardplace = GetParent().GetNode<Node>("EmpPlace").GetNode<Tile>("EmptyPlace");
        newcardplace.Position = ((Node2D)cardplace.GetParent()).Position + new Vector2(150, 0);
        cardplace.GetParent().GetParent().AddChild(newcardplace); //этот абзац добавляет контейнер для карты в рукаве

        int numcat = 0;
        if (team == teamEnemy)
        {
            numcat = cardlistEnemy[rand.Next(0, cardlistEnemy.Count)];
            cardlistEnemy.Remove(numcat);

        }
        else
        {
            numcat = cardlistUsers[rand.Next(0, cardlistUsers.Count)];
            cardlistUsers.Remove(numcat);
        }
        //int numcat = listteam[team][rand.Next(0, cardlistUsers.Count)];
        //listteam[team].Remove(numcat);

        Neko newNeko = CreateCat(team, numcat, x, y);
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(newNeko.unit, "position", cardplace.Position + new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out);
        counter++; //здесь мы рандомно добавляем карту в рукав

        ((neko1)newNeko.unit).anchor_field = cardplace; //привязываем якорь к карте
        lazy = false;
        cardplace = newcardplace.GetNode<Tile>("EmptyPlace");

        if (team == teamEnemy)
        {
            armlistEnemy.Add(newNeko);
        }
        else
        {
            armlistUsers.Add(newNeko);
        }
    }

    //Creating a new card on left mouse click
    /*public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("leftclick") && mouse_on_top)
        {
            GetCard(team);
        }
    }*/

    public void _on_mouse_entered()
    {
        mouse_on_top = true;
    }

    public void _on_mouse_exited()
    {
        mouse_on_top = false;
    }

    public Neko CreateCat(int team, int numcat, int x, int y) //создаем котов
    {
        if (numcat == 1)
        {
            Swordsman nw = new(team, x, y, (Node2D)cardplace.GetParent());
            return nw;
        }
        else
        {
            Archer nw = new(team, x, y, (Node2D)cardplace.GetParent());
            return nw;
        }
    }

    /*public void _on_button_attack_pressed()
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
        enemymove = true;
    }*/
}