using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckoutApp.Models
{
    //[Keyless]
    public class ArticleDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }
        [Required]
        public long Price { get; set; }

    }
}
