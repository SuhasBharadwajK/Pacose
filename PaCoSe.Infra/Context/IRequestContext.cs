using PaCoSe.Models;

namespace PaCoSe.Infra.Context
{
    public interface IRequestContext
    {
        User User { get; set; }

        Device Device { get; set; }

        string EntityName { get; }

        void Clear(string cacheKey = null);
    }
}
