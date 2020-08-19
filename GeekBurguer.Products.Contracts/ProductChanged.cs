namespace GeekBurguer.Products.Contracts
{
    public class ProductChanged
    {
        public ProductState State { get; set; }
        public Product Product { get; set; }

    }

    public enum ProductState
    {
        Deleted = 2,
        Modified = 3,
        Added = 4
    }

}
