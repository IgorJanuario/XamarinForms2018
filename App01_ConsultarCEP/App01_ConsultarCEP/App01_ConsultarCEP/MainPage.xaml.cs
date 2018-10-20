using App01_ConsultarCEP.Service;
using App01_ConsultarCEP.Service.Model;
using System;
using Xamarin.Forms;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnBuscaCEP.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = txtCep.Text.Trim();

            if (isValideCEP(cep))
            {
                try {
                    Endereco endereco = ViaCEPService.BuscarEnderecoViaCEP(cep);

                    if (endereco != null)
                    {
                        lblResultado.Text = string.Format("Endereço: {2} de {3} {0}, {1}", endereco.localidade, endereco.uf, endereco.logradouro, endereco.bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "Endereço não encontrado para o CEP informado: " + cep , "OK");
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Erro crítico", ex.Message, "OK");
                }
            }
        }

        private bool isValideCEP(string cep)
        {
            bool valido = true;
            int novoCEP = 0;

            if(cep.Length != 8)
            {
                DisplayAlert("Erro","CEP inválido ! O CEP deve conter 8 caracteres", "OK");
                valido = false;
            }

            if(!int.TryParse(cep,out novoCEP))
            {
                DisplayAlert("Erro", "CEP inválido ! O CEP deve conter apenas números", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
