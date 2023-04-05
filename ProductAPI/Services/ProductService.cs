using AccountAPI.Infrastructure.Repository;
using AccountAPI.Models;
using AutoMapper;
using Azure;
using Azure.Core;
using FluentValidation;
using System;
using System.Net;
using AccountAPI.Models.Validators;
using System.Security.Claims;
using AccountAPI.Response;
using System.IdentityModel.Tokens.Jwt;
using ProductAPI.Infrastructure.Repository;
using ProductAPI.Infrastructure.Entity;
using ProductAPI.Exceptions;
using ProductAPI.Controllers;

namespace AccountAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
         
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper; 
        }
        public async Task<ProductResponse> CreateProductAsync(ProductModel model)
        {
            var response = new ProductResponse();
            var validator = new ProductModelValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Product validation failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            var product = _mapper.Map<Product>(model);
            product.CreatedBy = "System";
            int dbResponse = await _productRepository.CreateProductAsync(product);

            if (dbResponse == 1)
            {
                response.Success = true;
                response.Message = "Product created successfully";
            }
            else if (dbResponse == -1)
            {
                response.Success = false;
                response.Message = "Product exists";
            }
            else
            {
                response.Success = false;
                response.Message = "Unable to Create Product. Database delete failed";
            }
            return response;
        }        

        public async Task<ProductResponse> UpdateProductAsync(ProductModel model)
        {
            var response = new ProductResponse();
            var validator = new ProductModelValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Product validation failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                //_logger.LogInformation("User creation failed with email: {email}", request.NewUser.Email);

                return response;
            }

            var product = _mapper.Map<Product>(model);
            product.UpdatedBy = "System";
            int dbResponse = await _productRepository.UpdateProductAsync(product);

            if (dbResponse == 1)
            {
                response.Success = true;
                response.Message = "Product updated successfully";
            }
            else if (dbResponse == -1)
            {
                response.Success = false;
                response.Message = "Product does not exists";
            }
            else
            {
                response.Success = false;
                response.Message = "Unable to update Product. Database update failed";
            }
            return response;
        }

        public async Task<ProductResponse> DeleteProductAsync(int productId)
        {
            var response = new ProductResponse();
            var validator = new ProductModelValidator();
             
            int dbResponse = await _productRepository.DeleteProductAsync(productId);

            if (dbResponse == 1)
            {
                response.Success = true;
                response.Message = "Product deleted successfully";
            }
            else if (dbResponse == -1)
            {
                response.Success = false;
                response.Message = "Product does not exists";
            }
            else
            {
                response.Success = false;
                response.Message = "Unable to delete product. Database delete failed";
            }
            return response;
        }

        public async Task<ProductModel> GetProductByIdAsync(int productId)
        {
            var dbProduct = await _productRepository.GetProductAsync(productId);
            var product = _mapper.Map<ProductModel>(dbProduct);
            if (product == null)
            {
                throw new NotFoundException($"Product with id {productId} not found");
            }
            return product;
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            var dbProducts = await _productRepository.GetAllProductsAsync();
            var products = _mapper.Map<IEnumerable<ProductModel>>(dbProducts);
            if (products == null || products.Count() == 0)
            {
                throw new NotFoundException("Products could not be found.");
            }
            return products;
        }
    }
}
