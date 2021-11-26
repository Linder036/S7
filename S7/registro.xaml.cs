using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using S7.Models;

namespace S7
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class registro : ContentPage
    {
        private SQLiteAsyncConnection con;
        public registro()
        {
            InitializeComponent();
            con = DependencyService.Get<DataBase>().GetConnection();
        }

        private void btn_agregar_Clicked(object sender, EventArgs e)
        {
            var DatosRegistro = new Estudiante { Nombre = Nombre.Text, Usuario = Usuario.Text, Contrasenia = Contrasenia.Text };
            con.InsertAsync(DatosRegistro);
            limpiarFormulario();
        }

        void limpiarFormulario()
        {
            Nombre.Text = "";
            Usuario.Text = "";
            Contrasenia.Text = "";
            DisplayAlert("Alerta", "Se agrego correctamente", "ok");
        }

    }
}