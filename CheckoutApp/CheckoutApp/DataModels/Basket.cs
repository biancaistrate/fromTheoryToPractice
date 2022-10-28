using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CheckoutApp.DataModels
{
    public class Basket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<Article>? Articles { get; set; }
        public long TotalNet
        {
            get
            {
                if (Articles==null) return 0;
                return Articles.Select(x => x.Price).Sum();
            }
        }
        public double TotalGross {
            get
            {
                if (Articles == null) return 0;
                return TotalNet + TotalNet * 0.10;
            }
        }
        public string? Customer { get; set; }

        public bool PaysVAT { get; set; }
        [NotMapped]
        public Status Status { get; set; }

        public void AddArticle(Article article)
        {
            if (Articles == null)
                Articles = new List<Article>();
            Articles.Add(article);
        }

    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        [EnumMember(Value ="open")]
        Open,
        [EnumMember(Value ="closed")]
        Closed
        
    }
}
