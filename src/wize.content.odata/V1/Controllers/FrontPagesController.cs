using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using wize.common.tenancy.Interfaces;
using wize.content.data;
using wize.content.data.v1.Models;

namespace wize.content.odata.V1.Controllers
{
    [ODataRoutePrefix("FrontPages")]
    public partial class FrontPagesController : BaseODataController<int, FrontPage>
    {
        private readonly WizeContext _context;
        private readonly ITenantProvider _tenantProvider;
        public FrontPagesController(ILogger<BaseODataController<int, FrontPage>> logger, IActionDescriptorCollectionProvider actionProvider, WizeContext context, ITenantProvider tenantProvider)
            : base(logger, actionProvider, context, tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }
    }
}