namespace CustomWebServer.ByTheCakeApplication.Services.Contracts
{
    using ViewModels.Orders;
    using System.Collections.Generic;

    public interface IShoppingService
    {
        void CreateOrder(int userId, IEnumerable<int> productIds);

        OrderListingViewModel GetByOrderId(int orderId);

        IEnumerable<OrderListingViewModel> GetByUserId(int? userId);
    }
}
