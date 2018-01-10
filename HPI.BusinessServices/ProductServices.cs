﻿using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using HPI.BusinessEntities;
using HPI.DataAccessLayer;
using HPI.DataAccessLayer.UnitOfWork;
using HPI.DataAccessLayer.DataModels;

namespace HPI.BusinessServices
{
    public class ProductServices:IProductServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public ProductServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches product details by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductModel GetProductByCode(string productCode)
        {
            var product = _unitOfWork.ProductRepository.GetByID(productCode);
            if (product != null)
            {
                Mapper.Initialize(cfg => {

                    cfg.CreateMap<Product, ProductModel>()
                        .ForMember(d => d.ProductCode,
                            opt => opt.MapFrom(src => src.ProductCode)
                        );
                });
                var productModel = Mapper.Map<Product, ProductModel>(product);
                return productModel;
            }
            return null;
        }

        /// <summary>
        /// Fetches all the products.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductModel> GetAllProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAll().ToList();
            if (products.Any())
            {
                Mapper.Initialize(cfg => {

                    cfg.CreateMap<Product, ProductModel>()
                        .ForMember(d => d.ProductCode,
                            opt => opt.MapFrom(src => src.ProductCode)
                        )
                        .ForMember(d => d.Price,
                            opt => opt.MapFrom(src => src.Price)
                        );
                });
                var productsModel = Mapper.Map<List<Product>, List<ProductModel>>(products);
                return productsModel;
            }
            return null;
        }

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public bool CreateProduct(ProductModel productModel)
        {
            using (var scope = new TransactionScope())
            {
                var product = new Product
                {
                    ProductCode = productModel.ProductCode,
                    Price = productModel.Price
                };
                _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.Save();
                scope.Complete();
                return true;
            }
        }
    }
}
