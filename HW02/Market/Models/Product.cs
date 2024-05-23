namespace Market.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual ProductGroup? Group { get; set; }
        public int? GroupId { get; set; }
        public virtual List<ProductStorage> Storages { get; set; }
    }
}
