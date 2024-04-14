using Godot;
using System;

public partial class CustomSignals : Node2D
{
    [Signal] 
    public delegate void DamageObjectEventHandler();
}
