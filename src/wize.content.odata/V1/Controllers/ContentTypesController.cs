using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using wize.common.tenancy.Interfaces;
using wize.content.data;
using wize.content.data.v1.Models;

namespace wize.content.odata.V1.Controllers
{
    [ODataRoutePrefix("ContentTypes")]
    public partial class ContentTypesController : BaseODataController<int, ContentType>
    {
        private readonly WizeContext _context;
        private readonly ITenantProvider _tenantProvider;
        public ContentTypesController(ILogger<BaseODataController<int, ContentType>> logger, IActionDescriptorCollectionProvider actionProvider, WizeContext context, ITenantProvider tenantProvider)
            : base(logger, actionProvider, context, tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }
    }
}