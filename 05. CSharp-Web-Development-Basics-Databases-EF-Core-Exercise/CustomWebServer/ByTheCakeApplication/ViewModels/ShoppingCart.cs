namespace CustomWebServer.ByTheCakeApplication.ViewModels
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public const string SessionKey = "$%Current_Shopping_Cart$%";

        public ICollection<int> ProductIds { get; private set; } = new List<int>();
    }
}
