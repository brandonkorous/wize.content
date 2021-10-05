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
    public class LinkGroupRepository : RepositoryBase<int, LinkGroup>, ILinkGroupRepository
    {
        private readonly WizeContext _context;
        public LinkGroupRepository(ILogger<ILinkGroupRepository> logger, WizeContext context) : base(logger, context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context = context;
        }
        public async Task<IList<dynamic>> GetLinksByTitle(string title, string fields = "")
        { 
            var linkGroup = await _context.LinkGroups.FirstOrDefaultAsync(lg => lg.Title == title);
            var links = _context.Links.Include(l => l.Children).Where(l => l.LinkGroupId == linkGroup.LinkGroupId && l.ParentLinkId == null && l.Published);
            var reduced = links.Select(LambdaBuilder.BuildSelect<Link>(fields));
            var sculpted = reduced.ShapeData(fields);
            return sculpted.ToList();
        }
    }
}