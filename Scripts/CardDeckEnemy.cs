/*using Godot;
using System;
using System.Collections.Generic;

public partial class Tile : Node2D { };

public partial class Field : Node2D { };

public partial class Neko : IComparable { };

public partial class CardDeckEnemy : Area2D
{
    Tile cardplace;
    List<int> catlist = new List<int> { 1, 1, 1, 1, 2, 2, 2, 2 };
    List<Neko> cardlist = new List<Neko>();

    public int x = -3;
    public int y = -3;
    public int counter = 0;
    public bool mymove = false;

    private bool mouse_on_top = false;
    private bool lazy = true;

    CardDeck Deck;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        cardplace = GetParent().GetNode<Node>("EmpPlaceEnemy").GetNode<Tile>("EmptyPlaceEnemy"); //взяли сцену
        Deck = GetParent().GetNode<CardDeck>("CardDeck");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (mymove && catlist.Count != 0)
        {
            if (lazy)
            {
                Random rand = new Random();
                Tween tween = GetTree().CreateTween();

                Node2D newcardplace = (Node2D)cardplace.GetParent().Duplicate();
                //newcardplace = GetParent().GetNode<Node>("EmpPlaceEnemy").GetNode<Tile>("EmptyPlaceEnemy");
                newcardplace.Position = ((Node2D)cardplace.GetParent()).Position - new Vector2(150, 0);
                cardplace.GetParent().GetParent().AddChild(newcardplace); //этот абзац добавляет контейнер для карты в рукаве

                int numcat = catlist[rand.Next(0, catlist.Count)];
                catlist.Remove(numcat);
                Neko newNeko = Deck.CreateCat(1, numcat, x, y);
                cardlist.Add(newNeko);
                tween.TweenProperty(newNeko.unit, "position", cardplace.Position + new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out);
                counter++; //здесь мы рандомно добавляем карту в рукав

                ((neko1)newNeko.unit).anchor_field = cardplace; //присваиваем якорь к карте
                lazy = false;
                cardplace = newcardplace.GetNode<Tile>("EmptyPlace");
            }

            PutOnTheField();

            Attacks();
        }
        else
        {
            lazy = true;
        }
    }


    public void PutOnTheField() //добавляем пока рандомно карту на поле
    {
        Random rand = new Random();
        Tween tween = GetTree().CreateTween();
        Tile anchortile;
        Neko nekocard = cardlist[rand.Next(0, cardlist.Count)];

        if (Field.tiles.Count == 16) //если никого нет на поле
        {
            anchortile = Field.tiles[rand.Next(0, Field.tiles.Count)];
        }
        else
        {
            //если мечник
            if (nekocard.damage == 2)
            {
                foreach (Neko neko_enemy in Neko.teams[(nekocard.team + 1) % 2])
                {
                    if (!neko_enemy.notattack)
                    {
                        int disX = Math.Abs(neko_enemy.x - this.x);
                        int disY = Math.Abs(neko_enemy.y - this.y);
                        if (disX <= 1 && disY <= 1)
                        {

                        }
                    }
                }
            }
            //если лучник
            else
            {

            }
        }
        //Field.tiles.Remove(anchortile);
        //tween.TweenProperty(nekocard.unit, "position", anchortile.Position + new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out);
        Neko.teams[nekocard.team].Add(nekocard);
    }


    public void Attacks() //атакуем котов
    {
        if (true) //здесь будет номер команды, которую выбрал игрок
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
        }
    }

    public void _on_button_attack_pressed()
    {
        mymove = true;
    }
}*/