using Products_service.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products_service.ViewModels
{
    public class ProduitVM
    {
        [Required(ErrorMessage = "Le libelle est obligatoire")]
        public string Libelle { get; set; } = null!;

        [Range(1, double.MaxValue)]
        public double Prix { get; set; }
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
        [Range(0, int.MaxValue)]
        public int AdminId { get; set; }
        [Range(0, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
