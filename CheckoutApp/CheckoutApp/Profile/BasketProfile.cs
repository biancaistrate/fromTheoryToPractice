using AutoMapper;
using CheckoutApp.DataModels;
using CheckoutApp.Models;

namespace CheckoutApp.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketDTO>();
            CreateMap<Article, ArticleDTO>();
        }
    }
}
