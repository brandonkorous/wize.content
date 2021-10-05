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
    public class ContentTypeRepository : RepositoryBase<int, ContentType>, IContentTypeRepository
    {
        private readonly WizeContext _context;
        public ContentTypeRepository(ILogger<IContentTypeRepository> logger, WizeContext context) : base(logger, context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context = context;
        }
        public virtual async Task<IPagedResponse> GetAllAsync(IQueryModel model)
        {
            var total = await _context.Set<ContentType>().FilterBy(model.filters).CountAsync();
            var sorted = _context.Set<ContentType>().OrderBy<ContentType>(model.sorts);
            var filtered = sorted.FilterBy(model.filters, model.or);
            if (model.size > 0)
                filtered = filtered.Skip((model.page - 1) * model.size).Take(model.size);

            var reduced = filtered.Select(LambdaBuilder.BuildSelect<ContentType>(model.fields));
            var sculpted = reduced.ShapeData(model.fields);
            return new PagedResponse<dynamic>(sculpted.ToList(), total, model.page, model.size);
        }

        public async Task<IPagedResponse> GetBySafeTitleAsync(string safeTitle, IQueryModel model)
        {
            var contentTypes = await _context.ContentTypes.Include(ct => ct.ContentItems).FirstOrDefaultAsync(c => c.SafeTitle == safeTitle);
            var contents = contentTypes.ContentItems;
            var total = contents.Select(c => 1).Sum();
            var publishedContents = contents.Where(c => c.Published).AsQueryable();
            var filtered = publishedContents.FilterBy(model.filters, model.or);
            if (model.size > 0)
                filtered = filtered.Skip((model.page - 1) * model.size).Take(model.size);

            var reduced = filtered.Select(LambdaBuilder.BuildSelect<Content>(model.fields));
            var sculpted = reduced.ShapeData(model.fields);
            return new PagedResponse<dynamic>(sculpted.ToList(), total, model.page, model.size);
        }
    }
}