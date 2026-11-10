
using CadastroEventos.Models;
using System.Globalization;

namespace CadastroEventos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            dt_termino.MinimumDate = dt_inicio.Date ?? DateTime.Today;
        }

        private void DataInicio_Alterada(
            object? sender, DateChangedEventArgs e)
        {
            DateTime inicio = dt_inicio.Date ?? DateTime.Today;

            if (dt_termino.Date == null ||
                dt_termino.Date < inicio)
            {
                dt_termino.Date = inicio;
            }

            dt_termino.MinimumDate = inicio;
        }

        private async void Button_Clicked(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_nome.Text) ||
                    string.IsNullOrWhiteSpace(txt_local.Text))
                {
                    await DisplayAlertAsync("Atenção",
                        "Preencha o nome e o local do evento.", "OK");
                    return;
                }

                if (!int.TryParse(txt_participantes.Text,
                    out int participantes) || participantes <= 0)
                {
                    await DisplayAlertAsync("Atenção",
                        "Digite uma quantidade válida de participantes.", "OK");
                    return;
                }

                if (!decimal.TryParse(txt_custo.Text,
                    NumberStyles.Number,
                    CultureInfo.GetCultureInfo("pt-BR"),
                    out decimal custo) || custo < 0)
                {
                    await DisplayAlertAsync("Atenção",
                        "Digite um custo válido. Exemplo: 25,50", "OK");
                    return;
                }

                if (dt_inicio.Date == null || dt_termino.Date == null)
                {
                    await DisplayAlertAsync("Atenção",
                        "Selecione as datas do evento.", "OK");
                    return;
                }

                if (dt_termino.Date < dt_inicio.Date)
                {
                    await DisplayAlertAsync("Atenção",
                        "A data de término não pode ser anterior ao início.", "OK");
                    return;
                }

                Evento evento = new Evento
                {
                    Nome = txt_nome.Text.Trim(),
                    Local = txt_local.Text.Trim(),
                    DataInicio = dt_inicio.Date.Value,
                    DataTermino = dt_termino.Date.Value,
                    NumeroParticipantes = participantes,
                    CustoPorParticipante = custo
                };

                await Navigation.PushAsync(new ResumoPage()
                {
                    BindingContext = evento
                });
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Erro", ex.Message, "OK");
            }
        }
    }
}
