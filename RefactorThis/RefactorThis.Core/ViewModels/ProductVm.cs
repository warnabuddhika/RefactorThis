using AutoMapper;
using RefactorThis.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RefactorThis.Core.Common.ViewModels
{
    public class ProductVm
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
        public bool IsNew { get; }

        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<Product, ProductVm>().ReverseMap();

        //}
    }
}
