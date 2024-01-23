using Godot;
using System;

public partial class CardDeck : Node2D
{
    //Preloding cards and starting hand
    PackedScene baseCard = ResourceLoader.Load<PackedScene>("res://Card/BaseCard.tscn");
    //Node playerHand = ResourceLoader.Load<Node>("res://Card/PlayerHand.cs");
    Vector2 cardSize = new Vector2(125, 175);
    string unit = "Swordsman";
    string[] cardList = { "Swordsman", "Swordsman", "Archer", "Archer" };
    int deckSize = 0;

    Tile cardplace;

    public int x = -2;
    public int y = -2;
    public int counter = 0;

    private bool mouse_on_top = false;
    private bool lazy = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        cardplace = GetParent().GetNode<Node>("EmpPlace").GetNode<Tile>("EmptyPlace");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left) && mouse_on_top)
        {
            if (lazy)
            {
                Tween tween = GetTree().CreateTween();

                Node2D newcardplace = (Node2D)cardplace.GetParent().Duplicate();
                //newcardplace = GetParent().GetNode<Node>("EmpPlace").GetNode<Tile>("EmptyPlace");
                newcardplace.Position = ((Node2D)cardplace.GetParent()).Position + new Vector2(75, 0);
                cardplace.GetParent().GetParent().AddChild(newcardplace);

                deckSize = cardList.Length;
                BaseCard newCard = (BaseCard)baseCard.Instantiate();
                newCard.typeOfCat = unit;

                //Neko newNeko = CreateCat(1, x, y);
                //tween.TweenProperty(newNeko.unit, "position", cardplace.Position + new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out);
                newCard.Position = GetGlobalMousePosition();
                tween.TweenProperty(newCard, "position", cardplace.Position + new Vector2(16, 0), 0.2f).SetEase(Tween.EaseType.Out);
                counter++;

                newCard.Scale = cardSize / newCard.Size;
                Node cards = GetParent().GetNode<Node>("Cards");
                cards.AddChild(newCard);

                //((neko1)newNeko.unit).anchor_field = cardplace;
                newCard.anchor_field = cardplace;
                lazy = false;
                cardplace = newcardplace.GetNode<Tile>("EmptyPlace");
            }
        }
        else
        {
            lazy = true;
        }
    }

    //Creating a new card on left mouse click
    public override void _Input(InputEvent @event)
    {
        /*if (@event.IsActionPressed("leftclick") && mouse_on_top)
        {
            deckSize = cardList.Length;
            BaseCard newCard = (BaseCard)baseCard.Instantiate();
            newCard.typeOfCat = unit;
            newCard.Position = GetGlobalMousePosition();
            newCard.Scale = cardSize / newCard.Size;
            Node cards = GetParent().GetNode<Node>("Cards");
            cards.AddChild(newCard);
        }*/
    }

    public void _on_mouse_entered()
    {
        mouse_on_top = true;
    }

    public void _on_mouse_exited()
    {
        mouse_on_top = false;
    }

    public Neko CreateCat(int team, int x, int y)
    {
        Archer nw = new(team, x, y, (Node2D)cardplace.GetParent());
        return nw;
    }
}
