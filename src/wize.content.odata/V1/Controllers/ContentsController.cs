using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using wize.common.tenancy.Interfaces;
using wize.content.data;
using wize.content.data.v1.Models;
using static Microsoft.AspNet.OData.Query.AllowedQueryOptions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace wize.content.odata.V1.Controllers
{
    [ODataRoutePrefix("Contents")]
    [ApiVersion("1.0")]
    public partial class ContentsController : BaseODataController<int, Content>
    {
        private readonly WizeContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly ILogger<BaseODataController<int, Content>> _logger;
        public ContentsController(ILogger<BaseODataController<int, Content>> logger, IActionDescriptorCollectionProvider actionProvider, WizeContext context, ITenantProvider tenantProvider)
            : base(logger, actionProvider, context, tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
            _logger = logger;
        }
        /// <summary>
        /// OData based PUT operation.
        /// This method receives a key value and a data model and attempts to apply the updated model to the existing record.
        /// </summary>
        /// <param name="ContentId">Key value</param>
        /// <param name="model">Data model</param>
        /// <returns>Data model</returns>
        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public override IActionResult Put([FromODataUri] int ContentId, [FromBody] Content model)
        //{
        //    _logger.LogError("test put2");
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        _context.Attach(model);
        //        _context.Update<Content>(model);
        //        _context.SaveChanges();

        //        return Updated(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error: Put(id):{0}", typeof(Content).Name);
        //        return new StatusCodeResult(500);
        //    }
        //}
    }
}