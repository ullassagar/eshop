using System.Collections.Generic;
using System.Linq;
using DataLayer;
using RepositoryLayer;
using RepositoryLayer.Infrastructure;

namespace BusinessLayer
{
    public class CategoryHandler
    {
        private readonly ICategoryRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryHandler(ICategoryRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public List<Category> GetAll()
        {
            return _productRepository.GetAll().ToList();
        }

        public void Add(Category product)
        {
            _productRepository.Add(product);
            _unitOfWork.Commit();
        }

        public Category GetById(int productId)
        {
            return _productRepository.GetById(productId);
        }

        public void Update(Category product)
        {
            _productRepository.Update(product);
        }
    }
}