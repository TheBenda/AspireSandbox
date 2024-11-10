using AKS.Domain.Results;

namespace AKS.Application.Mapping;

public static class EntityResolverExtensions
{
    public static PersistenceResult<TDto> ToDto<TDto, TEntity>(this PersistenceResult<TEntity> entity, Func<TEntity, TDto> mapper) 
        where TDto : class 
        where TEntity : class
    => entity.Type switch
        {
            ResultType.Result => PersistenceResult<TDto>.Success(
                TypedResult<TDto>.Of(mapper.Invoke(entity.Result!.Value))),
            ResultType.Error => PersistenceResult<TDto>.Failure(entity.Error!),
            _ => throw new ArgumentException("Unable to convert result to entity.")
        };
}