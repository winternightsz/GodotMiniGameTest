using Godot;
using System;

public partial class Carta : Node2D
{
    public string Elemento { get; private set; } // "Fogo", "Água", "Gelo"
    public int Valor { get; private set; }       // 2 a 12
    public string Cor { get; private set; }      // Ex: "Vermelho", "Azul", "Verde"

    [Signal]
    public delegate void CardSelectedEventHandler(Carta card);
	
	    public override void _Ready()
    {
        // Conecta o sinal "pressed" a um método local
        Connect("pressed", new Callable(this, nameof(OnCardPressed)));
    }
    public void SetCardData(string elemento, int valor, string cor)
    {
        Elemento = elemento;
        Valor = valor;
        Cor = cor;

        // Atualizar interface da carta
        GetNode<Label>("ValueLabel").Text = Valor.ToString();
        GetNode<Sprite2D>("Icon").Texture = GD.Load<Texture2D>($"res://Assets/Cards/{Elemento}.png");
        GetNode<Sprite2D>("Background").Modulate = ColorFromString(cor);
    }

    private void OnCardPressed()
    {
        EmitSignal(nameof(CardSelected), this); // Emite o sinal CardSelected ao clicar
    }

    private Color ColorFromString(string color)
    {
        switch (color.ToLower())
        {
            case "vermelho": return new Color(1, 0, 0);
            case "azul": return new Color(0, 0, 1);
            case "verde": return new Color(0, 1, 0);
            default: return new Color(1, 1, 1);
        }
    }
}
