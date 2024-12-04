using Godot;

public partial class HUD : Control
{
    public void AtualizarHUD(string faixa, int progresso)
    {
        GetNode<Label>("FaixaLabel").Text = $"Faixa: {faixa}";
        GetNode<TextureProgressBar>("ProgressBar").Value = progresso;
    }
}
