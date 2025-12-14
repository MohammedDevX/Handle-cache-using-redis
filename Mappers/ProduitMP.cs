using Products_service.DTOS;
using Products_service.Models;
using Products_service.ViewModels;

namespace Products_service.Mappers
{
    public class ProduitMP
    {
        public static ProduitDTO ToProduitDTO(Produit produit, UserDTO user)
        {
            return new ProduitDTO
            {
                Id = produit.Id,
                Libelle = produit.Libelle,
                Prix = produit.Prix,
                Stock = produit.Stock,
                AdminId = produit.AdminId,
                NomAdmin = user.NomUser,
                CategoryId = produit.CategoryId,
                LibelleCategory = produit.Category.Libelle
            };
        }

        public static Produit ProduitVMToProduit(ProduitVM produit)
        {
            return new Produit
            {
                Libelle = produit.Libelle,
                Prix = produit.Prix,
                Stock = produit.Stock,
                AdminId = produit.AdminId,
                CategoryId = produit.CategoryId
            };
        }

        public static Category CategoryVMToCategory(CategoryVM category)
        {
            return new Category
            {
                Libelle = category.Libelle
            };
        }
    }
}
