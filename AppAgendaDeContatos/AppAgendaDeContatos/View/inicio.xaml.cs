using AppAgendaDeContatos.Model;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using Xamarin.Forms.Internals;
using DeviceInfo = Xamarin.Essentials.DeviceInfo;

namespace AppAgendaDeContatos.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class inicio : ContentPage
    {
        ObservableCollection<Contato> lista_contatos = new ObservableCollection<Contato>();
        public inicio()
        {

            InitializeComponent();          
            lst_contatos.ItemsSource = lista_contatos;

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new addContato());
        }

        private void lst_contatos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView disparador = (ListView)sender;
            Contato contato_selecionado = (Contato)disparador.SelectedItem;

            Navigation.PushAsync(new addContato
            {
                BindingContext = (Contato)contato_selecionado
            });
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem disparador = (MenuItem)sender;


            Contato contato_selecionado = (Contato)disparador.BindingContext;

            
            bool confirmacao = await DisplayAlert("Tem Certeza?", "Remover o Contato " + contato_selecionado.nome + "?", "Sim", "Não");

            if (confirmacao)
            {
               
                await App.Database.Delete(contato_selecionado.id);


                lista_contatos.Remove(contato_selecionado);
            }
        }
        protected override void OnAppearing()
        {
            
            if (lista_contatos.Count == 0)
            {

                System.Threading.Tasks.Task.Run(async () =>
                {

                    List<Contato> temp = await App.Database.Getall();

                    foreach (Contato item in temp.OrderBy(n => n.nome))
                    {
                        lista_contatos.Add(item);
                    }
                    ref_carregando.IsRefreshing = false;
                });
            }
        }
        private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            string buscou = e.NewTextValue;

            System.Threading.Tasks.Task.Run(async () =>
            {
                List<Contato> temp = await App.Database.Search(buscou);

                /**
                 * Limpando a ObservableCollection antes de add os itens vindos da busca.
                 */
                lista_contatos.Clear();

                foreach (Contato item in temp.OrderBy(n => n.nome))
                {
                    lista_contatos.Add(item);
                }

                ref_carregando.IsRefreshing = false;
            });
        }

        private void ref_carregando_Refreshing(object sender, EventArgs e)
        {

            lista_contatos.Clear();
            System.Threading.Tasks.Task.Run(async () =>
                {

                    List<Contato> temp = await App.Database.Getall();

                    foreach (Contato item in temp)
                    {
                        lista_contatos.Add(item);
                    }

                    ref_carregando.IsRefreshing = false;
                });

        }
    }
}