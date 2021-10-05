using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using wize.common.tenancy.Interfaces;
using wize.content.data;
using wize.content.data.v1.Models;

namespace wize.content.odata.V1.Controllers
{
    [ODataRoutePrefix("Links")]
    public partial class LinksController : BaseODataController<int, Link>
    {
        private readonly WizeContext _context;
        private readonly ITenantProvider _tenantProvider;
        public LinksController(ILogger<BaseODataController<int, Link>> logger, IActionDescriptorCollectionProvider actionProvider, WizeContext context, ITenantProvider tenantProvider)
            : base(logger, actionProvider, context, tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }
    }
}