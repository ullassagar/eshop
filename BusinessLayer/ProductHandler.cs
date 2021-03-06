﻿using System.Collections.Generic;
using System.Linq;
using DataLayer;
using RepositoryLayer;
using RepositoryLayer.Infrastructure;

namespace BusinessLayer
{
    public class ProductHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public List<Product> GetList(bool includeOutOfStock = true)
        {
            return _productRepository.GetList(includeOutOfStock);
        }

        public void Add(Product product)
        {
            _productRepository.Add(product);
            _unitOfWork.Commit();
        }

        public Product GetById(int productId)
        {
            return _productRepository.GetById(productId);
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            _unitOfWork.Commit();
        }
    }
}