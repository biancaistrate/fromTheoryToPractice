using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckoutApp.Models
{
    public class BasketDTO
    {
        [BindRequired]
        public int Id { get; set; }
        public IEnumerable<ArticleDTO>? Articles { get; set; } = new List<ArticleDTO>();
        public long TotalNet
        {
            get
            {
                return Articles.Select(x => x.Price).Sum();
            }
        }
        public double TotalGross {
            get
            {
                return Articles.Select(x => x.Price).Sum() * 0.10;
            }
        }
        [Required(AllowEmptyStrings = false)]
        public string? Customer { get; set; }

        public bool PaysVAT { get; set; }

    }
}
