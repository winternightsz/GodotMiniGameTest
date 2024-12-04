using Godot;
using System.Collections.Generic;

public partial class BattleManager : Node
{
    private HBoxContainer cardContainer;
    private Carta enemyCard;
    private Label resultLabel;
    private Button playButton;

    private List<Carta> playerDeck = new List<Carta>();
    private Carta selectedCard;

    public override void _Ready()
    {
        cardContainer = GetNode<HBoxContainer>("PlayerArea/CardContainer");
        enemyCard = GetNode<Carta>("EnemyArea/EnemyCard");
        resultLabel = GetNode<Label>("ResultLabel");
        playButton = GetNode<Button>("PlayButton");

        playButton.Connect("pressed", new Callable(this, nameof(OnPlayButtonPressed)));

        GerarDeck();
        MostrarCartas();
    }

private void GerarDeck()
{
    string[] elementos = { "Fogo", "Água", "Gelo" };
    string[] cores = { "Vermelho", "Azul", "Verde" };
    var random = new RandomNumberGenerator();

    PackedScene cardScene = GD.Load<PackedScene>("res://Battle/Card.tscn");

    for (int i = 0; i < 5; i++)
    {
        string elemento = elementos[random.RandiRange(0, elementos.Length - 1)];
        int valor = random.RandiRange(2, 12);
        string cor = cores[random.RandiRange(0, cores.Length - 1)];

        var carta = cardScene.Instantiate<Carta>();
        carta.SetCardData(elemento, valor, cor);
        carta.Connect("CardSelected", new Callable(this, nameof(OnCardSelected)));

        cardContainer.AddChild(carta);
        playerDeck.Add(carta);
    }
}


    private void MostrarCartas()
    {
        foreach (var carta in playerDeck)
        {
            cardContainer.AddChild(carta);
        }
    }

    private void OnCardSelected(Carta card)
    {
        // Destaque a carta selecionada
        foreach (Carta child in cardContainer.GetChildren())
        {
            child.Modulate = new Color(1, 1, 1); // Resetar cor
        }

        card.Modulate = new Color(0.8f, 1, 0.8f); // Destaque
        selectedCard = card;
        GD.Print($"Carta selecionada: {card.Elemento}, Valor: {card.Valor}");
    }

    private void OnPlayButtonPressed()
    {
        if (selectedCard == null)
        {
            GD.Print("Nenhuma carta selecionada!");
            return;
        }

        // Gerar carta do inimigo
        var cartaInimigo = GerarCartaInimigo();
        MostrarCartaInimigo(cartaInimigo);

        // Determinar vencedor
        int resultado = DeterminarVencedor(selectedCard, cartaInimigo);
        MostrarResultado(resultado);

        // Remover carta jogada do deck
        playerDeck.Remove(selectedCard);
        cardContainer.RemoveChild(selectedCard);
        selectedCard.QueueFree();
    }

    private Carta GerarCartaInimigo()
    {
        var carta = GD.Load<PackedScene>("res://Battle/Card.tscn").Instantiate<Carta>();
        string[] elementos = { "Fogo", "Água", "Gelo" };
        var random = new RandomNumberGenerator();
        string elemento = elementos[random.RandiRange(0, elementos.Length - 1)];
        int valor = random.RandiRange(2, 12);

        carta.SetCardData(elemento, valor, "Cinza");
        return carta;
    }

    private void MostrarCartaInimigo(Carta carta)
    {
        enemyCard.SetCardData(carta.Elemento, carta.Valor, carta.Cor);
        GD.Print($"Inimigo jogou: {carta.Elemento}, Valor: {carta.Valor}");
    }

    private int DeterminarVencedor(Carta cartaJogador, Carta cartaInimigo)
    {
        if (cartaJogador.Elemento == "Fogo" && cartaInimigo.Elemento == "Gelo") return 1;
        if (cartaJogador.Elemento == "Gelo" && cartaInimigo.Elemento == "Água") return 1;
        if (cartaJogador.Elemento == "Água" && cartaInimigo.Elemento == "Fogo") return 1;

        if (cartaInimigo.Elemento == "Fogo" && cartaJogador.Elemento == "Gelo") return -1;
        if (cartaInimigo.Elemento == "Gelo" && cartaJogador.Elemento == "Água") return -1;
        if (cartaInimigo.Elemento == "Água" && cartaJogador.Elemento == "Fogo") return -1;

        if (cartaJogador.Elemento == cartaInimigo.Elemento)
        {
            return cartaJogador.Valor.CompareTo(cartaInimigo.Valor);
        }

        return 0;
    }

    private void MostrarResultado(int resultado)
    {
        if (resultado > 0)
        {
            resultLabel.Text = "Você venceu!";
        }
        else if (resultado < 0)
        {
            resultLabel.Text = "Você perdeu!";
        }
        else
        {
            resultLabel.Text = "Empate!";
        }
    }
}
