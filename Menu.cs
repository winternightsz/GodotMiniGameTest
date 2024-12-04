using Godot;
using System;

public partial class Menu : Control
{
    public override void _Ready()
    {
        // Conecta o botão "Jogar" ao método PlayGame
        var playButton = GetNode<TextureButton>("PlayButton");
        playButton.Connect("pressed", new Callable(this, nameof(OnPlayButtonPressed)));
    }

    private void OnPlayButtonPressed()
    {
        GD.Print("Jogo iniciado!");
        // Troque para a cena do jogo principal
        GetTree().ChangeSceneToFile("res://mundo_1.tscn"); // Altere o caminho para a sua cena principal
    }
}
