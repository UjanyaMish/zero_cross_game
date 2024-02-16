using Godot;
using System;

public partial class Menu : Control
{
	private ConfigFile config;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Sprite2D backgroung = GetNode<Sprite2D>("CanvasLayer/ЗаФоном");
		var menuSize = Size;
		backgroung.Scale = new Vector2(1, 1);
		backgroung.Scale *= menuSize / (backgroung.Texture.GetSize());
		backgroung.Position = PivotOffset;
		config = new ConfigFile();
		Error err = config.Load("res://settings.cfg");
		if (err != Error.Ok)//если файла не существует
		{
		}
		else
		{
			config.Save("res://settings.cfg");
			var id_res = (int)config.GetValue("resolution", "choice", 0);
			Viewport viewport = GetViewport();

			// Получаем размеры окна просмотра (Viewport)
			Vector2 windowSize = viewport.GetVisibleRect().Size;
			Vector2 screenSize = viewport.GetVisibleRect().Size;


			Window window = this.GetTree().Root;
			if (id_res == 1)
			{
				window.Set("size", new Vector2((float)1280, (float)720));
				screenSize = new Vector2((float)1280, (float)720);

			}
			else if (id_res == 0)
			{
				window.Set("size", new Vector2((float)1920, (float)1080));
				screenSize = new Vector2((float)1920, (float)1080);
			}

			int x = 0;
			int y = 0;
			if (windowSize != screenSize)
			{
				x = (int)(windowSize.X / 2 - screenSize.X / 2);
				y = (int)(windowSize.Y / 2 - screenSize.Y / 2);
			}
			window.Set("position", new Vector2(x, y));
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_button_game_pressed()
	{
		Node2D select = GetNode<Node2D>("CanvasLayer/choice_team");
		select.Show();
	}
}
