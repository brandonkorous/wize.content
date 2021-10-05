using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using wize.content.data;
using wize.content.data.v1.Models;
using wize.content.data.v1.Interfaces;
using wize.common.use.paging.Models;
using wize.common.use.repository.Operators;
using wize.common.use.paging.Interfaces;
using wize.common.use.repository.Extensions;
using wize.common.use.repository.Models;

namespace wize.content.data.v1.Repositories
{
    public class FrontPageRepository : RepositoryBase<int, FrontPage>, IFrontPageRepository
    {
        public FrontPageRepository(ILogger<IFrontPageRepository> logger, WizeContext context) : base(logger, context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}