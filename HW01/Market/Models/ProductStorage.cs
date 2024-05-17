namespace Market.Models
{
    public partial class ProductStorage
    {
        public virtual required Product Product { get; set; }
        public virtual required Storage Storage { get; set; }
        public int ProductId { get; set; }
        public int StorageId { get; set; }
        public int Count { get; set; }
    }
}