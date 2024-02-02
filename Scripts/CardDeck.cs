using Godot;
using System;
using System.Collections.Generic;

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
    List<int> catlist = new List<int> { 1, 1, 1, 1, 2, 2, 2, 2 };

    public int x = -2;
    public int y = -2;
    public int counter = 0;
    int team = 0;

    private bool mouse_on_top = false;
    private bool lazy = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        cardplace = GetParent().GetNode<Node>("EmpPlace").GetNode<Tile>("EmptyPlace"); //взяли сцену
        isEnemy = this.GetParent().IsInGroup("Enemy");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (isEnemy)
        {

        }
        else
        {
            
        }
    }


    public void EnemyMove()
    {

    }

    public void UsersMove()
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left) && mouse_on_top && catlist.Count != 0) //при нажатии
        {
            GetCard(team);
        }
        else
        {
            lazy = true;
        }
    }

    public void GetCard(int team) //добавляяет карту в рукав
    {
        if (lazy)
        {
            Random rand = new Random();
            Tween tween = GetTree().CreateTween();

            Node2D newcardplace = (Node2D)cardplace.GetParent().Duplicate();
            //newcardplace = GetParent().GetNode<Node>("EmpPlace").GetNode<Tile>("EmptyPlace");
            newcardplace.Position = ((Node2D)cardplace.GetParent()).Position + new Vector2(150, 0);
            cardplace.GetParent().GetParent().AddChild(newcardplace); //этот абзац добавляет контейнер для карты в рукаве

            int numcat = catlist[rand.Next(0, catlist.Count)];
            catlist.Remove(numcat);
            Neko newNeko = CreateCat(team, numcat, x, y);
            tween.TweenProperty(newNeko.unit, "position", cardplace.Position + new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out);
            counter++; //здесь мы рандомно добавляем карту в рукав

            ((neko1)newNeko.unit).anchor_field = cardplace; //привязываем якорь к карте
            lazy = false;
            cardplace = newcardplace.GetNode<Tile>("EmptyPlace");
        }
    }

    //Creating a new card on left mouse click
    /*public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("leftclick") && mouse_on_top)
        {
            deckSize = cardList.Length;
            BaseCard newCard = (BaseCard)baseCard.Instantiate();
            newCard.typeOfCat = unit;
            newCard.Position = GetGlobalMousePosition();
            newCard.Scale = cardSize / newCard.Size;
            Node cards = GetParent().GetNode<Node>("Cards");
            cards.AddChild(newCard);
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

    public void _on_button_attack_pressed()
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