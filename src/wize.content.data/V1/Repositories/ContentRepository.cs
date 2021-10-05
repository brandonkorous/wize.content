using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using wize.content.data;
using wize.content.data.v1.Models;
using wize.content.data.v1.Interfaces;
using Microsoft.Extensions.Logging;
using wize.common.use.paging.Models;
using wize.common.use.repository.Operators;
using wize.common.use.paging.Interfaces;
using Microsoft.EntityFrameworkCore;
using wize.common.use.repository.Extensions;
using wize.common.use.repository.Models;

namespace wize.content.data.v1.Repositories
{
    public class ContentRepository : RepositoryBase<int, Content>, IContentRepository
    {
        private readonly WizeContext _context;
        public ContentRepository(ILogger<IContentRepository> logger, WizeContext context) : base(logger, context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context = context;
        }
        public async Task<dynamic> GetBySafeTitleAsync(string safeTitle, string fields = "")
        {
            var content = await _context.Contents.FirstOrDefaultAsync(c => c.SafeTitle == safeTitle && c.Published);
            var reduced = content.ShapeData<Content>(fields);
            return reduced;
        }
    }
}