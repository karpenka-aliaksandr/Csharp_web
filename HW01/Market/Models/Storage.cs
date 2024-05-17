namespace Market.Models
{
    public partial class Storage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<ProductStorage> Products { get; set; }

    }
}
