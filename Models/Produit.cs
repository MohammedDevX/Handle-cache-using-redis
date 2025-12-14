using System.ComponentModel.DataAnnotations.Schema;

namespace Products_service.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Libelle { get; set; } = null!;
        public double Prix { get; set; }
        public int Stock { get; set; }
        public int AdminId { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
