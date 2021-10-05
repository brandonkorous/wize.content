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
using wize.common.use.repository.Models;

namespace wize.content.data.v1.Repositories
{
    public class ContentArchiveRepository : RepositoryBase<int, ContentArchive>, IContentArchiveRepository
    {
        public ContentArchiveRepository(ILogger<IContentArchiveRepository> logger, WizeContext context) : base(logger, context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}