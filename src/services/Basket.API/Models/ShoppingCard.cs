namespace Basket.API.Models
{
    public class ShoppingCard
    {
        public ShoppingCard(string userName)
        {
            UserName = userName;
        }

        public ShoppingCard()
        {

        }
        public string UserName { get; set; }
        List<ShoppingCardItem> Items { get; set; } = new List<ShoppingCardItem>();
        public decimal TotalPrice { get
            {
                decimal totalPrice = 0;
                foreach(var item in Items)
                {
                    totalPrice += item.Price;
                }

                return totalPrice;
            }
        }
    }
}
