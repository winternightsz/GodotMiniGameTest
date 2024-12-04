using Godot;
using System;

public partial class Npc : Area2D
{
    [Export] public string RequiredFaixa = "Branca"; // Faixa necessária para interação

    [Signal]
    
	public delegate void InteragirEventHandler(Jogador player);

    public override void _Ready()
    {
        // Conectar o sinal de colisão
        Connect("body_entered", new Callable(this, nameof(OnPlayerEnter)));

    }

    private void OnPlayerEnter(Node body)
    {
        if (body is Jogador player)
        {
            GD.Print($"Interagindo com NPC... Faixa necessária: {RequiredFaixa}");
            EmitSignal(nameof(Interagir), player);
        }
    }
}
