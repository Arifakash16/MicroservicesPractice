﻿using Catalog.API.Interfaces.Manager;
using Catalog.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        IProductManager _productManager;

        public CatalogController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        [ResponseCache(Duration =30)]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productManager.GetAll();
                return CustomResult("Data Loaded Successfully.", products, HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 30)]
        public IActionResult GetById(String id)
        {
            try
            {
                var product = _productManager.GetById(id);
                return CustomResult("Data Loaded Successfully.", product, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 30)]
        public IActionResult GetByCategory(string category) 
        {
            try
            {
                var products = _productManager.GetByCategory(category);
                return CustomResult("Data Loaded Successfully.", products, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                product.Id = ObjectId.GenerateNewId().ToString();
                bool isSaved = _productManager.Add(product);

                if (isSaved)
                {
                    return CustomResult("Product save successfully", product, HttpStatusCode.Created);
                }

                return CustomResult("Product save failed", HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                if(string.IsNullOrEmpty(product.Id))
                {
                    return CustomResult("Product not found", HttpStatusCode.NotFound);
                }
                
                bool isUpdated = _productManager.Update(product.Id,product);

                if (isUpdated)
                {
                    return CustomResult("Product has been modified successfully", product, HttpStatusCode.OK);
                }

                return CustomResult("Product modified failed", product, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult DeleteProduct(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return CustomResult("Product not found", HttpStatusCode.NotFound);
                }

                bool isDeleted = _productManager.Delete(id);

                if (isDeleted)
                {
                    return CustomResult("Product has been deleted successfully", HttpStatusCode.OK);
                }

                return CustomResult("Product deleted failed", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
