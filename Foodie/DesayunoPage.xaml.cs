using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace Foodie
{
    public partial class DesayunoPage : ContentPage
    {
        public DesayunoPage()
        {
            InitializeComponent();
            CargarDesayunos();
        }

        private void CargarDesayunos()
        {
            try
            {
                var desayunos = App.Database.Table<Receta>()
                                    .Where(r => r.Categoria == "Desayuno")
                                    .ToList();

                desayunoList.ItemsSource = desayunos;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "No se pudieron cargar los desayunos.", "OK");
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
                    Categoria = "Desayuno"
                };

                App.Database.Insert(nuevaReceta);

                nombreEntry.Text = "";
                ingredientesEditor.Text = "";
                preparacionEditor.Text = "";
                tiempoEntry.Text = "";

                DisplayAlert("✅ Guardado", "El desayuno fue guardado con éxito", "OK");

                CargarDesayunos();
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
                    CargarDesayunos();
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