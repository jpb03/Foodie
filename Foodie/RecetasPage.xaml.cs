using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Foodie
{
    public partial class RecetasPage : ContentPage
    {
        List<Receta> todasLasRecetas = new List<Receta>();

        public RecetasPage()
        {
            InitializeComponent();
            CargarRecetas();
        }

        private void CargarRecetas()
        {
            try
            {
                // Obtener recetas desde la base de datos
                todasLasRecetas = App.Database.Table<Receta>()?.ToList() ?? new List<Receta>();

                // Si no hay ninguna, insertar una demo para prueba
                if (!todasLasRecetas.Any())
                {
                    var demo = new Receta
                    {
                        Nombre = "Panqueques",
                        Ingredientes = "Harina, leche, huevo",
                        Preparacion = "Mezclar y freír",
                        Tiempo = "15 min",
                        Categoria = "Desayuno"
                    };
                    App.Database.Insert(demo);
                    todasLasRecetas.Add(demo);
                }

                recetaList.ItemsSource = todasLasRecetas;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "No se pudieron cargar las recetas.", "OK");
                Console.WriteLine(ex);
            }
        }

        private void OnBuscarReceta(object sender, TextChangedEventArgs e)
        {
            try
            {
                string texto = buscarEntry?.Text?.ToLower() ?? "";

                var filtradas = todasLasRecetas
                    .Where(r =>
                        !string.IsNullOrWhiteSpace(r.Nombre) &&
                        r.Nombre.ToLower().Contains(texto)
                    )
                    .ToList();

                recetaList.ItemsSource = filtradas;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Ocurrió un problema al buscar recetas.", "OK");
                Console.WriteLine(ex);
            }
        }
    }
}