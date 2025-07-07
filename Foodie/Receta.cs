using SQLite;

public class Receta
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Ingredientes { get; set; }
    public string Preparacion { get; set; }
    public string Tiempo { get; set; }
    public string Categoria { get; set; }  // <- MUY IMPORTANTE para filtrar
}