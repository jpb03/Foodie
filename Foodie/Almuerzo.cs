using SQLite;

namespace Foodie
{
    public class Almuerzo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacion { get; set; }
        public string Tiempo { get; set; }
    }
}