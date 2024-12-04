using Godot;
using System;

public partial class Jogador : CharacterBody2D
{
    [Export] public float Speed = 50.0f; // Velocidade máxima
    private Vector2 movimento;            // Velocidade atual do jogador
    private Vector2 direcao;              // Direção do movimento

    private AnimatedSprite2D animatedSprite;

    public override void _Ready()
    {
        // Obtém o AnimatedSprite2D do jogador
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        // Obtém a direção do Input (WASD ou setas)
        direcao = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

        // Verifica se há movimento
        if (direcao != Vector2.Zero)
        {
            // Atualiza a velocidade de movimento
            movimento = direcao * Speed;

            // Atualiza a animação com base na direção
            if (direcao.X > 0) animatedSprite.Play("walk_right");
            else if (direcao.X < 0) animatedSprite.Play("walk_left");
            else if (direcao.Y > 0) animatedSprite.Play("walk");
            else if (direcao.Y < 0) animatedSprite.Play("walk");
        }
        else
        {
            // Sem movimento, desacelera e para a animação
            movimento = Vector2.Zero;
            animatedSprite.Stop();
        }

        // Aplica a velocidade ao jogador
        Velocity = movimento;

        // Move o jogador com física
        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        // Verifica se a ação de interação foi pressionada
        if (@event.IsActionPressed("interact"))
        {
            var rayCast = GetNode<RayCast2D>("RayCast2D");

            // Verifica se o RayCast está colidindo com algo
            if (rayCast.IsColliding())
            {
                var collider = rayCast.GetCollider() as Node;
                collider?.EmitSignal("Interagir", this);
            }
        }
    }
}
