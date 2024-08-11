using Godot;
using System;

public partial class camera : Camera3D
{
	// Called when the node enters the scene tree for the first time.
	Vector3 rot;
	public override void _Ready()
	{
		rot = Rotation;
		Rotation = rot;
	}
}
