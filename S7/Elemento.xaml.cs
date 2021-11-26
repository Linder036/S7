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
    public partial class Elemento : ContentPage
    {
        public int IdSeleccionado;
        private SQLiteAsyncConnection con;
        IEnumerable<Estudiante> ResultadoDelete;
        IEnumerable<Estudiante> ResultadoUpdate;
        
        
        public Elemento(int iD)
        {
            InitializeComponent();
            con = DependencyService.Get<DataBase>().GetConnection();
            IdSeleccionado = iD;
        }

        public static IEnumerable<Estudiante> Delete(SQLiteConnection db, int id)
        {
            return db.Query<Estudiante>("DELETE FROM Estudiante where ID = ?", id);
        }
        public static IEnumerable<Estudiante> Update(SQLiteConnection db, string nombre, string usuario, string contrasenia, int id)
        {
            return db.Query<Estudiante>("UPDATE Estudiante SET Nombre= ?, Usuario= ?," + "Contrasenia = ? where Id = ?", nombre, usuario, contrasenia, id);
        }

        private void btn_actualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                ResultadoUpdate = Update(db, Nombre.Text, Usuario.Text, Contrasenia.Text, IdSeleccionado);
                DisplayAlert("Alerta", "Se actualizo correctamente", "ok");

            }
            catch(Exception ex)
            {
                DisplayAlert("Alerta", "Error" + ex.Message, "ok");

            }
        }

        private void btn_eliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                ResultadoDelete = Delete(db, IdSeleccionado);
                DisplayAlert("Alerta", "Se elimino correctamente", "ok");

            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "Error" + ex.Message, "ok");
            }
        }
        
    }
}