using RentWebProj.ViewModels;
using System.Collections.Generic;

namespace RentWebProj.Interfaces
{
    public interface IProductService
    {
        IEnumerable<CardsViewModel> GetCategoryData();
        string GetCategoryName(string categoryID);
        IEnumerable<CardsViewModel> GetCheapestProductCardData();
        IEnumerable<CardsViewModel> GetMostPopularProductCardData(int amongDays);
        ProductDetailToCart GetProductDetail(string PID);
        List<CardsViewModel> GetSelectedProductData(string categoryID);
        IEnumerable<CardsViewModel> GetSubCategoryOptions(string catID);
        IEnumerable<CardsViewModel> ProductDataWithStars();
        List<CartIndex> ProductToCheckout(string PID, string startDate, string expirationDate);
        List<CardsViewModel> SearchProductCards(FilterSearchViewModel filterFormList);
    }
}