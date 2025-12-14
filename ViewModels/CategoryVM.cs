using System.ComponentModel.DataAnnotations;

namespace Products_service.ViewModels
{
    public class CategoryVM
    {
        [Required(ErrorMessage = "Le libelle est obligatoire")]
        public string Libelle { get; set; } = null!;
    }
}
