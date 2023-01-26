using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Укажите как вас зовут")]
        [Display(Name = "Укажите имя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Вставьте адрес доставки")]
        [Display(Name = "Укажите адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        public ShippingDetails(string userName,
                             string address,
                             string city,
                             string country)
        {
            if (string.IsNullOrWhiteSpace(userName))
                userName = string.Empty;

            if (string.IsNullOrWhiteSpace(address))
                address = string.Empty;

            if (string.IsNullOrWhiteSpace(city))
                city = string.Empty;

            if (string.IsNullOrWhiteSpace(country))
                country = string.Empty;

            UserName = userName;
            Address = address;
            City = city;
            Country = country;
        }
        public ShippingDetails() { }
    }
}
