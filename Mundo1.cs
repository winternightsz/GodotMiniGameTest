using Godot;
using System;

public partial class Mundo1 : Node2D
{
    public override void _Ready()
    {
        // Conectar NPCs, itens e inimigos ao World
        foreach (Npc npc in GetNode<Node2D>("NPCs").GetChildren())
            npc.Connect("Interagir", new Callable(this, nameof(OnNPCInteract)));

        foreach (Item item in GetNode<Node2D>("Items").GetChildren())
            item.Connect("Coletado", new Callable(this, nameof(OnItemCollected)));

        foreach (Inimigo enemy in GetNode<Node2D>("Enemies").GetChildren())
            enemy.Connect("StartBattle", new Callable(this, nameof(OnEnemyEncounter)));
    }

    private void OnNPCInteract(Jogador player)
    {
        GD.Print("Interação com NPC.");
    }

    private void OnItemCollected(Jogador player)
    {
        GD.Print("Item coletado!");
    }

    private void OnEnemyEncounter(Jogador player)
    {
        GD.Print("Inimigo encontrado! Mudando para a cena de batalha...");
        GetTree().ChangeSceneToFile("res://Battle/Battle.tscn");
    }
}