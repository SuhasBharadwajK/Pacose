using PaCoSe.Models;

namespace PaCoSe.Infra.Context
{
    public class BaseRequestContext : IRequestContext
    {
        private RequestContextBuilder RequestContextBuilder { get; set; }

        public BaseRequestContext(RequestContextBuilder requestContextBuilder)
        {
            this.RequestContextBuilder = requestContextBuilder;
        }

        public User User { get; set; }

        public Device Device { get; set; }

        public string EntityName
        {
            get
            {
                return this.User?.Username ?? $"{this.Device?.Name}-{this.Device?.IdentifierHash}";
            }
        }

        public void Clear(string cacheKey = null)
        {
            this.RequestContextBuilder.Clear(cacheKey);
        }
    }
}
