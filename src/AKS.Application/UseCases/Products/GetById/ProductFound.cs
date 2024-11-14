using AKS.Application.UseCases.Products.Transport;
using AKS.Domain.Results;

namespace AKS.Application.UseCases.Products.GetById;

public record ProductFound(PersistenceResult<ProductDto> Product);