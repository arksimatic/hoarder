using Godot;
using System;

namespace Hoarder.Scripts
{
	public partial class Item : Node2D
	{
		public override void _Ready()
		{
			Resource resource;
			TextureRect child = GetNode<TextureRect>("TextureRect");

			if (new Random().Next(0, 2) == 0)
				resource = ResourceLoader.Load("res://Graphics/Equippable/weapon.png");
			else
				resource = ResourceLoader.Load("res://Graphics/Equippable/pickaxe.png");

			Texture2D texture = (Texture2D)resource;
			child.Texture = texture;
		}
	}
}
