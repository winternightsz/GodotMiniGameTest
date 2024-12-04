using Godot;
using System;

public partial class Inimigo : Area2D
{
    [Export] public int Health = 100;

    [Signal]
    public delegate void StartBattleEventHandler(Jogador player);

    public override void _Ready()
    {
        Connect("body_entered", new Callable(this, nameof(OnPlayerEncounter)));
    }

    private void OnPlayerEncounter(Node body)
    {
        if (body is Jogador player)
        {
            GD.Print("Inimigo encontrado! Iniciando batalha...");
            EmitSignal(nameof(StartBattle), player);

            // Opcional: Desativar inimigo do mapa durante a batalha
            QueueFree();
        }
    }
}

