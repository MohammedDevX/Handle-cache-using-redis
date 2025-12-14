namespace Products_service.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Libelle { get; set; } = null!;
        public List<Produit> Produit { get; set; } = new();
    }
}
