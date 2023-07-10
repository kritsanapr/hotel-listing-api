using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using HotelListingAPI.VSCode.Models.Hotels;

namespace HotelListingAPI.VSCode.Models.Country
{
    public class GetCountryDto: BaseCountryDto
    {
        public int Id { get; set; }
    }

}