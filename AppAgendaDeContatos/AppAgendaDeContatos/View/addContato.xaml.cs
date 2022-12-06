using AppAgendaDeContatos.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAgendaDeContatos.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class addContato : ContentPage
    {
        
        public addContato()
        {

            InitializeComponent();
            txt_nome.Focus();

        }
        protected override void OnAppearing()
        {
            Contato contato = BindingContext as Contato;
            if (contato?.tituloT != null || contato?.empresa != null)
                Button_trabalho_Clicked(null,null);       
            if (contato?.numero != null)
                Button_Clicked(null,null);         
            if (contato?.tituloT != null)
                Button_Clicked_email(null,null);
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                Contato contatoid = BindingContext as Contato;
                if (contatoid?.id == null)
                { 
                    Contato c = new Contato
                    {
                        numero = txt_numero.Text,
                        nome = txt_nome.Text,
                        tpTelefone = txt_tp_numero.SelectedItem.ToString(),
                        email = txt_email.Text,
                        tituloT = txt_tituloT.Text,
                        empresa = txt_empresa.Text
                    };
                    await App.Database.Insert(c);

                    await DisplayAlert("", "Contato adicionado", "OK");

                    await Navigation.PopAsync();
                }
                    else
                    {
                        Contato c = new Contato
                        {
                            id = contatoid.id,
                            numero = txt_numero.Text,
                            nome = txt_nome.Text,
                            tpTelefone = txt_tp_numero.SelectedItem.ToString(),
                            email = txt_email.Text,
                            tituloT = txt_tituloT.Text,
                            empresa = txt_empresa.Text
                        };
                        await App.Database.Update(c);

                        await DisplayAlert("", "Contato atualizado", "OK");

                        await Navigation.PopAsync();
                }
                

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");

            }



        }

        private void lst_contatos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
 

        private void Button_Clicked(object sender, EventArgs e)
        {
            grid_telefone.IsVisible = true;
        }

        private void btn_mini_telefone_Clicked(object sender, EventArgs e)
        {
            grid_telefone.IsVisible = false;
        }

        private void Button_trabalho_Clicked(object sender, EventArgs e)
        {
                infoTrabalho.IsVisible = true;       
        }  
        private void btn_mini_infoT_Clicked(object sender, EventArgs e)
        {
                infoTrabalho.IsVisible = false;       
        }
        private void Button_Clicked_email(object sender, EventArgs e)
        {
            email.IsVisible = true;
        }       
        private void btn_mini_email_Clicked(object sender, EventArgs e)
        {
            email.IsVisible = false;
        }
        
    }
}