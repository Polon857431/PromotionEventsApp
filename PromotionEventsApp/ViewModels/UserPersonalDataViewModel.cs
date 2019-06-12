using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PromotionEventsApp.ViewModels
{
    public class UserPersonalDataViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        [RegularExpression(@"^\d{5}([\-]?\d{4})?$",ErrorMessage = "Zły format kodu poczowego")]
        public string ZipCode { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}

