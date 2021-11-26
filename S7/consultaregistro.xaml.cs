using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using S7.Models;
using System.IO;
using System.Collections.ObjectModel;

namespace S7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class consultaregistro : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Estudiante> TablaEstudiante;

        public ObservableCollection<Estudiante> ListaUsuarios_ItemsSource { get; private set; }

        public consultaregistro()
        {
            InitializeComponent();
            con = DependencyService.Get<DataBase>().GetConnection();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected async override void OnAppearing()
        {
            var ResulRegistros = await con.Table<Estudiante>().ToListAsync();
            TablaEstudiante = new ObservableCollection<Estudiante>(ResulRegistros);
            ListaUsuarios_ItemsSource = TablaEstudiante;

            base.OnAppearing();

        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (Estudiante)e.SelectedItem;
            var item = Obj.Id.ToString();
            int ID = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new Elemento(ID));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}