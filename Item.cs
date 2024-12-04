using Godot;

public partial class Item : Area2D
{
    [Signal]
    public delegate void ColetadoEventHandler(Jogador player);

    public override void _Ready()
    {
        Connect("body_entered", new Callable(this, nameof(OnPlayerCollect)));
    }

    private void OnPlayerCollect(Node body)
    {
        if (body is Jogador player)
        {
            GD.Print("Item coletado!");
            EmitSignal(nameof(Coletado), player);
            QueueFree(); // Remove o item após a coleta
        }
    }
}
