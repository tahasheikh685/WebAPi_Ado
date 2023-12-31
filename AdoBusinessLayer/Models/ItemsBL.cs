﻿using FluentValidation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdoBusinessLayer.Models
{
    public class ItemsBL
    {
        public int Id { get; set; }

        [DisplayName("Product Name")]
        public string? Name { get; set; }

        
        [DisplayName("Product Description")]
        public string? Description { get; set; }

        [DisplayName("Product Price")]
        public decimal Price { get; set; }
    }

    public class ItemsBLValidator : AbstractValidator<ItemsBL>
    {
        public ItemsBLValidator()
        {
            RuleFor(item => item.Name)
                .NotEmpty().WithMessage("Item Name is required.")
                .Length(2, 20).WithMessage("Name must be between 2 and 50 characters.");

            RuleFor(item => item.Description).NotEmpty().WithMessage("Item Description is required.")
                .Length(2, 20).WithMessage("Description must be between 2 and 50 characters."); 

            RuleFor(item => item.Price).NotEmpty().WithMessage("Item Price is required.")
                                      .GreaterThan(1).WithMessage("Item Price must be greater than 1.");
        }
        
    }

    
}
