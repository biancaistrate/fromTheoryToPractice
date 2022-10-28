using AutoMapper;
using CheckoutApp.Data;
using CheckoutApp.DataModels;
using CheckoutApp.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;

namespace CheckoutApp.Controllers
{
    [ApiController]
    [Route("baskets")]
    public class BasketsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public BasketsController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> Create([FromBody] CustomerInfo customerInfo, CancellationToken token)
        {
            //validation
            var response = await _dataContext.Baskets.AddAsync(
                new Basket() { Customer = customerInfo.Name, PaysVAT = customerInfo.PaysVAT }, token);
            await _dataContext.SaveChangesAsync(token);

            var dto = _mapper.Map(response.Entity, typeof(Basket), typeof(BasketDTO));

            return CreatedAtAction(nameof(GetBasket), new { id = response.Entity.Id },dto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDTO>> GetBasket(int id, CancellationToken token)
        {
            var response = await _dataContext.Baskets.FindAsync(new object[] { id }, token);
            if(response == null)
                return NotFound();

            var dto = _mapper.Map(response, typeof(Basket), typeof(BasketDTO));
            return dto is BasketDTO ? (BasketDTO)dto : NotFound();
        }

        [HttpPost("{id}/article-line")]
        public async Task<ActionResult> AddArticle(int id, [FromBody] ArticleDTO article, CancellationToken token)
        {
            var basket = await _dataContext.Baskets.FindAsync(new object[] { id }, token);
            if (basket == null)
                return NotFound();

            //automapper
            basket.AddArticle(new Article(article.Title,article.Price));
            await _dataContext.SaveChangesAsync(token);

            return CreatedAtAction(nameof(GetBasket), new { id = basket.Id }, _mapper.Map(basket, typeof(Basket), typeof(BasketDTO)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBasket(int id, [FromBody] StatusRequest status, CancellationToken token)
        {
            var basket = await _dataContext.Baskets.FindAsync(new object[] { id }, token);
            if (basket == null)
                return NotFound();

            basket.Status = status.BasketStatus;
            await _dataContext.SaveChangesAsync(token);
            return CreatedAtAction(nameof(GetBasket), new { id = basket.Id }, _mapper.Map(basket, typeof(Basket), typeof(BasketDTO)));
        }

    }
}

