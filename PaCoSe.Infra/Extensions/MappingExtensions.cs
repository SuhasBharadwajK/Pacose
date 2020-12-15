using AutoMapper;
using System.Collections.Generic;

namespace PaCoSe.Infra.Extensions
{
    public static class MappingExtensions
    {
        public static T MapTo<T>(this IMapper mapper, object o)
        {
            if (o == null)
            {
                return default(T);
            }

            return mapper.Map<T>(o);
        }

        public static IList<TDestination> MapCollectionTo<TSource, TDestination>(this IMapper mapper, IList<TSource> o)
        {
            if (o == null)
            {
                return null;
            }

            return mapper.Map<IList<TSource>, IList<TDestination>>(o);
        }

        public static IEnumerable<TDestination> MapCollectionTo<TSource, TDestination>(this IMapper mapper, IEnumerable<TSource> o)
        {
            if (o == null)
            {
                return null;
            }

            return mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(o);
        }
    }
}
