using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace Foodie
{
    public partial class PostresPage : ContentPage
    {
        public PostresPage()
        {
            InitializeComponent();
            CargarPostres();
        }

        private void CargarPostres()
        {
            try
            {
                var postres = App.Database.Table<Receta>()
                                    .Where(r => r.Categoria == "Postres")
                                    .ToList();

                postreList.ItemsSource = postres;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "No se pudieron cargar los postres.", "OK");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void OnGuardarClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nombreEntry.Text) &&
                !string.IsNullOrWhiteSpace(ingredientesEditor.Text) &&
                !string.IsNullOrWhiteSpace(preparacionEditor.Text) &&
                !string.IsNullOrWhiteSpace(tiempoEntry.Text))
            {
                var nuevaReceta = new Receta
                {
                    Nombre = nombreEntry.Text.Trim(),
                    Ingredientes = ingredientesEditor.Text.Trim(),
                    Preparacion = preparacionEditor.Text.Trim(),
                    Tiempo = tiempoEntry.Text.Trim(),
                    Categoria = "Postres"
                };

                App.Database.Insert(nuevaReceta);

                nombreEntry.Text = "";
                ingredientesEditor.Text = "";
                preparacionEditor.Text = "";
                tiempoEntry.Text = "";

                DisplayAlert("✅ Guardado", "El postre fue guardado con éxito", "OK");

                CargarPostres();
            }
            else
            {
                DisplayAlert("⚠️ Campos vacíos", "Por favor llena todos los campos antes de guardar.", "OK");
            }
        }

        private void OnEliminarClicked(object sender, EventArgs e)
        {
            try
            {
                var id = (int)((Button)sender).CommandParameter;

                var receta = App.Database.Table<Receta>()
                                         .FirstOrDefault(r => r.Id == id);

                if (receta != null)
                {
                    App.Database.Delete(receta);
                    DisplayAlert("🗑 Eliminado", "Receta eliminada con éxito.", "OK");
                    CargarPostres();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "No se pudo eliminar la receta.", "OK");
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
