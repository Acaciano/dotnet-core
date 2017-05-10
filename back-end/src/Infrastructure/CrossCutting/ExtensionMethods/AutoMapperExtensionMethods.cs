
using AutoMapper;

namespace Infrastructure.CrossCutting.ExtensionMethods
{
    public static class AutoMapperExtensionMethods<TEntity>
    {
        public static TModel Map<TModel>(TEntity entity)
        {
            Mapper.Initialize(expression => { expression.CreateMap<TEntity, TModel>(); });
            return Mapper.Map<TEntity, TModel>(entity);
        }

        public static TEntity Map<TModel>(TModel model)
        {
            Mapper.Initialize(expression => { expression.CreateMap<TModel, TEntity>(); });
            return Mapper.Map<TModel, TEntity>(model);
        }
    }
}
