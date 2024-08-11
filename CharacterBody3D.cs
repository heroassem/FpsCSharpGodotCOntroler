using Godot;
using System;
using System.Diagnostics;

public partial class CharacterBody3D : Godot.CharacterBody3D
{
    [Export] public float speed;
    [Export] public float jump;
    [Export] public float gravity;
    [Export] public float moise_speed;

    Camera3D hade;
    camera camera;

    public override void _Ready()
    {
        hade = GetNode<Camera3D>("Node3D/Camera3D");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("quiq"))
        {
            GetTree().Quit();
        }

        Input.MouseMode = Input.MouseModeEnum.Captured;

        if (@event is InputEventMouseMotion MousePosition)
        {
            RotateY(-MousePosition.Relative.X * Mathf.DegToRad(moise_speed));
            hade.RotateX(-MousePosition.Relative.Y * Mathf.DegToRad(moise_speed));

            Vector3 rot = hade.Rotation;

            rot.X = Mathf.Clamp(rot.X, Mathf.DegToRad(-80), Mathf.DegToRad(80));

            hade.Rotation = rot;
        }

    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 vector = Velocity;

        vector.Y += gravity * -1;

        if (IsOnFloor() == true && Input.IsActionJustPressed("jump"))
        {
            GD.Print("jump");
            vector.Y = jump * (float)delta;
        }

        if (IsOnFloor())
        {
            Vector2 inputs = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
            Vector3 move_direction = (Transform.Basis * new Vector3(inputs.X, 0, inputs.Y)).Normalized();

            if (inputs != Vector2.Zero)
            {
                vector.X = move_direction.X * speed * (float)delta;
                vector.Z = move_direction.Z * speed * (float)delta;
            }

            else
            {
                vector.X = move_direction.X = 0;
                vector.Z = move_direction.Z = 0;
            }

        }
        Velocity = vector;
        MoveAndSlide();
    }
}
