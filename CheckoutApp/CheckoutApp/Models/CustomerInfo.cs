using System.ComponentModel.DataAnnotations;

namespace CheckoutApp.Models
{
    public class CustomerInfo
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public bool PaysVAT { get; set; }
    }
}
