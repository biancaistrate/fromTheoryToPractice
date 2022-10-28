using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckoutApp.DataModels
{
    //[Keyless]
    public class Article
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public long Price { get; set; }

        public Article()
        {

        }
        public Article(string title, long price)
        {
            Title = title;
            Price = price;
        }

    }
}
