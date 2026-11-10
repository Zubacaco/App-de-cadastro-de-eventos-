
namespace CadastroEventos
{
    public partial class ResumoPage : ContentPage
    {
        public ResumoPage()
        {
            InitializeComponent();
        }

        private async void Voltar_Clicked(object? sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
