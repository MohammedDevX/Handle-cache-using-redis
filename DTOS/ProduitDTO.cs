namespace Products_service.DTOS
{
    public class ProduitDTO
    {
        public int Id { get; set; }
        public string Libelle { get; set; } = null!;
        public double Prix { get; set; }
        public int Stock { get; set; }
        public int AdminId { get; set; }
        public string NomAdmin { get; set; }
        public int CategoryId { get; set; }
        public string LibelleCategory { get; set; } = null!;
    }
}
