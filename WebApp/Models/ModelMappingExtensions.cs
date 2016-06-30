using AutoMapper;

namespace WebApp.Models
{
    public static class ModelMappingExtensions
    {
        /// <summary>
        /// This is just an 'alias' for ToViewModel for semantic correctness :-)
        /// </summary>       
        public static TTo ToEntity<TFrom, TTo>(this TFrom from) where TTo : new()
        {
            return from.ToViewModel<TFrom, TTo>();
        }

        public static TTo ToViewModel<TFrom, TTo>(this TFrom from) where TTo : new()
        {
            var model = new TTo();
            model = Mapper.Map<TFrom, TTo>(from);
            return model;
        }
    }
}