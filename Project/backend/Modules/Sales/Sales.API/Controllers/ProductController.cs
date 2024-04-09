using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using MediatR;
using Sales.Contracts.Product;
using Sales.Application.Commands.RegisterProduct;
using Sales.Application.Commands.AddProductImage.Add;
using Sales.Application.Commands.AddProductImage;
using Sales.Application.Commands.AddProductImage.Delete;
using Sales.Contracts.Product.ProductImage.Delete;
using Sales.Contracts.Product.ProductImage.Add;
using Sales.Contracts.Product.ProductImage.Update;
using Sales.Application.Commands.UpdateProduct;
using Sales.Contracts.Product.Discount;
using Sales.Application.Commands.AddDiscount;
using Sales.Application.Queries.GetProductDetails;


namespace Sales.API.Controllers;
[Route("product")]
public class ProductController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    // Register Product
    [HttpPost("register")]
    public async Task<IActionResult> RegisterProduct(CreateProductRequest request)
    {
        var command = _mapper.Map<RegisterProductCommand>(request);
        var registerProductResult = await _mediator.Send(command);
        return registerProductResult.Match(
           product => Ok(_mapper.Map<CreateProductResponse>(product)),
           errors => Problem(errors));
    }

    // Get Product by Id
    [HttpGet("{productId}/getProductById")]
    public async Task<IActionResult> GetProductById([FromRoute] Guid productId)
    {
        var query = _mapper.Map<GetProductDetailsQuery>(productId);
        var listProductDetailsQuery = await _mediator.Send(query);
        return listProductDetailsQuery.Match(
            productDetails => Ok(_mapper.Map<CreateProductResponse>(productDetails)),
            errors => Problem(errors));
    }

    // Update Product
    [HttpPut("{productId}/updateProduct")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid productId, 
        UpdateProductDetailsRequest request)
    {
        var command = _mapper.Map<UpdateProductCommand>(request);
        command = command with { ProductId = productId };
        var result = await _mediator.Send(command);
        return result.Match(
            productDetails => {
                var response = _mapper.Map<UpdateProductDetailsResponse>(productDetails);
                return Ok(response with { ProductId = productId.ToString(), Success = true});

            },
            errors => Problem(errors));
    }

    // Add Product Image
    [HttpPost("{productId}/AddImage")]
    public async Task<IActionResult> AddProductImage(
        [FromRoute] Guid productId, 
        [FromBody] AddProductImageRequest request)
    {
        var command = _mapper.Map<AddProductImageCommand>(request);
        command = command with { ProductId = productId };
        var addProductImageResult = await _mediator.Send(command);
        return addProductImageResult.Match(
            productImage => {
                var response = _mapper.Map<AddProductImageResponse>(productImage);
                return Ok(response with { ProductId = productId.ToString() });

            },
            errors => Problem(errors));
        
    }

    // Update Product Image
    [HttpPut("{productId}/updateImage/{imageId}")]
    public async Task<IActionResult> UpdateProductImage(Guid productId, Guid imageId, UpdateProductImageRequest request)
    {
        var command = _mapper.Map<UpdateProductImageCommand>(request);
        command = command with { ProductId = productId, ImageId = imageId };
        var result = await _mediator.Send(command);
        return result.Match(
        success => Ok(new UpdateProductImageResponse(productId.ToString(), imageId.ToString(), true)),
        errors => Problem(errors));
    }

    // Delete Product Image
    [HttpDelete("{productId}/deleteImage/{imageId}")]
    public async Task<IActionResult> DeleteProductImage(
       Guid productId, Guid imageId, DeleteProductImageRequest request)
    {
        var command = _mapper.Map<DeleteProductImageCommand>(request);
        command = command with { ProductId = productId, ImageId = imageId };
        var result = await _mediator.Send(command);
        return result.Match(
            success => Ok(new DeleteProductImageResponse(productId.ToString(), imageId.ToString(), true)),
            errors => Problem(errors));
            
    }

    // Add Product Discount
    [HttpPost("addDiscount")]
    public async Task<IActionResult> AddProductDiscount(AddProductDiscountRequest request)
    {
        var command = _mapper.Map<AddProductDiscountCommand>(request);
        var addProductDiscountResult = await _mediator.Send(command);
        return addProductDiscountResult.Match(
            discount =>{
                var response = _mapper.Map<AddProductDiscountResponse>(discount);
                return Ok(response with { ProductId = request.ProductId});
            },
            errors => Problem(errors));
    }
}
