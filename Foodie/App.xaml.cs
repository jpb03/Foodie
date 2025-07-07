using Microsoft.Maui;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.IO;

namespace Foodie
{
    public partial class App : Application
    {
        public static SQLiteConnection Database;

        public App()
        {
            InitializeComponent();

            // Ruta de la base de datos
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "foodie.db3");
            Database = new SQLiteConnection(dbPath);

            // Crear tabla Receta y Usuario
            Database.CreateTable<Usuario>();
            Database.CreateTable<Receta>();

            // Insertar usuario demo si no existe
            if (Database.Table<Usuario>().FirstOrDefault() == null)
            {
                Database.Insert(new Usuario { Nombre = "admin", Contrasena = "123" });
            }

            // Página de inicio
            MainPage = new NavigationPage(new LoginPage());
        }
    }

    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
    }

    public class Receta
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacion { get; set; }
        public string Tiempo { get; set; }
        public string Categoria { get; set; } // "Desayuno", "Almuerzo", "Postre"
    }
}