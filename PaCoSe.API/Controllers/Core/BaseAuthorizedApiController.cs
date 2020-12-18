using Microsoft.AspNetCore.Mvc;
using PaCoSe.Infra.Context;

namespace PaCoSe.API.Controllers.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAuthorizedApiController : ControllerBase
    {
        protected IRequestContext RequestContext { get; set; }

        public BaseAuthorizedApiController(IRequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }
    }
}
