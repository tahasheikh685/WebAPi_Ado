using FluentValidation;

namespace WebAPi_Ado.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }

    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(item => item.Name).NotEmpty().WithMessage("Item Name is required.");
            RuleFor(item => item.Description).NotEmpty().WithMessage("Item Description is required.");
            RuleFor(item => item.Price).NotEmpty().WithMessage("Item Price is required.")
                                      .GreaterThan(1).WithMessage("Item Price must be greater than 1.");
        }
    }
}
