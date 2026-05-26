using StockControl.Domain.Interfaces;
using StockControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Application.Services
{
    public class CompositeProductService
    {
        private readonly ICompositeProductRepository _repository;

        public CompositeProductService(ICompositeProductRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CompositeProduct> GetAllProducts()
        {
            return _repository.GetAll();
        }

        public CompositeProduct GetProductById(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateProduct(CompositeProduct product)
        {
            ValidateProduct(product);
            _repository.Add(product);
        }

        public void UpdateProduct(CompositeProduct product)
        {
            ValidateProduct(product);
            _repository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            _repository.Delete(id);
        }
        private void ValidateProduct(CompositeProduct product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            if (product.Name.Length < 3)
                throw new System.Exception("Product name must be at least 3 characters long.");

            if (product.SalePrice <= 0)
                throw new System.Exception("Sale price must be greater than zero.");

            if (product.Components == null || product.Components.Count == 0)
                throw new System.Exception("Composite product must have at least one component.");

            foreach (var component in product.Components)
            {
                if(component.Quantity <= 0)
                    throw new System.Exception("Component quantity must be greater than zero.");
            }
        }
    }
}
