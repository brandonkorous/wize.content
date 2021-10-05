using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using wize.common.tenancy.Interfaces;
using wize.content.data;
using wize.content.data.v1.Models;

namespace wize.content.odata.V1.Controllers
{
    [ODataRoutePrefix("FileTypes")]
    public partial class FileTypesController : BaseODataController<int, FileType>
    {
        private readonly WizeContext _context;
        private readonly ITenantProvider _tenantProvider;
        public FileTypesController(ILogger<BaseODataController<int, FileType>> logger, IActionDescriptorCollectionProvider actionProvider, WizeContext context, ITenantProvider tenantProvider)
            : base(logger, actionProvider, context, tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }
    }
}