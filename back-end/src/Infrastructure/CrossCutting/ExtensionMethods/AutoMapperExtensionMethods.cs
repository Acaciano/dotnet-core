
using System.Collections.Generic;
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

        public static List<TModel> Map<TModel>(List<TEntity> entities)
        {
            List<TModel> models = new List<TModel>();

            foreach (var entity in entities)
            {
                Mapper.Initialize(expression => { expression.CreateMap<TEntity, TModel>(); });
                TModel model = Mapper.Map<TEntity, TModel>(entity);

                models.Add(model);
            }

            return models;
        }

        public static List<TEntity> Map<TModel>(List<TModel> models)
        {
            List<TEntity> entities = new List<TEntity>();

            foreach (var model in models)
            {
                Mapper.Initialize(expression => { expression.CreateMap<TModel, TEntity>(); });
                TEntity entity = Mapper.Map<TModel, TEntity>(model);

                entities.Add(entity);
            }

            return entities;
        }
    }
}
