using StockControl.Domain.Interfaces;
using StockControl.Domain.Entities;
using System.Collections.Generic;

namespace StockControl.Application.Services
{
    public class SimpleProductServices
    {
        private readonly ISimpleProductRepository _repository;

        public SimpleProductServices(ISimpleProductRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SimpleProduct> GetAllProducts()
        {
            return _repository.GetAll();
        }

        public SimpleProduct GetProductById(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateProduct(SimpleProduct product)
        {
            ValidateProduct(product);
            _repository.Add(product);
        }

        public void UpdateProduct(SimpleProduct product)
        {
            ValidateProduct(product);
            _repository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            _repository.Delete(id);
        }

        private void ValidateProduct(SimpleProduct product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            if (product.CostPrice <= 0)
            {
                throw new ArgumentException("Cost price cannot be negative and be greater than zero.");
            }

            if(product.SalePrice <= 0)
            {
                throw new ArgumentException("Sale price cannot be negative and be greater than zero.");
            }

            if (product.SalePrice < product.CostPrice)
                {
                    throw new ArgumentException("Sale price cannot be less than cost price.");
            }
        }
    }
}
