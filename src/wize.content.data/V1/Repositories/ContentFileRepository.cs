//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using System.Linq;

//using Microsoft.Extensions.Logging;
//using Microsoft.EntityFrameworkCore;

//using wize.content.data;
//using wize.content.data.v1.Models;
//using wize.content.data.v1.Interfaces;
//using wize.common.use.paging.Models;
//using wize.common.use.repository.Operators;
//using wize.common.use.paging.Interfaces;
//using wize.common.use.repository.Extensions;

//namespace wize.content.data.v1.Repositories
//{
//    public class ContentFileRepository : IContentFileRepository
//    {
//        private readonly ILogger<ContentFileRepository> _logger;
//        private readonly WizeContext _context;
//        public ContentFileRepository(ILogger<ContentFileRepository> logger, WizeContext context)
//        {
//            _logger = logger;
//            _context = context;
//        }

//        public void Add(ContentFile model)
//        {
//            _context.ContentFiles.Add(model);
//            _context.SaveChanges();
//        }

//        public async Task AddAsync(ContentFile model)
//        {
//            await _context.ContentFiles.AddAsync(model);
//            await _context.SaveChangesAsync();
//        }

//        public void Delete(ContentFile model)
//        {
//            _context.ContentFiles.Remove(model);
//            _context.SaveChanges();
//        }

//        public async Task DeleteAsync(ContentFile model)
//        {
//            _context.ContentFiles.Remove(model);
//            await _context.SaveChangesAsync();
//        }

//        public bool Exists(int id)
//        {
//            return _context.ContentFiles.Any(b => b.ContentFileId == id);
//        }
//        public async Task<bool> ExistsAsync(int id)
//        {
//            return await Task.FromResult(_context.ContentFiles.Any(b => b.ContentFileId == id));
//        }

//        public dynamic Get(int id, string fields = null)
//        {
//            return _context.ContentFiles
//                .Where(b => b.ContentFileId == id).Select(LambdaBuilder.BuildSelect<ContentFile>(fields)).FirstOrDefault();
//        }

//        public async Task<dynamic> GetAsync(int id, string fields = null)
//        {
//            return await _context.ContentFiles
//                .Where(b => b.ContentFileId == id).Select(LambdaBuilder.BuildSelect<ContentFile>(fields)).FirstOrDefaultAsync();
//        }

//        public IQueryable<dynamic> GetAll(string fields = null)
//        {
//            return _context.ContentFiles
//                .Select(LambdaBuilder.BuildSelect<ContentFile>(fields));
//        }

//        public async Task<IQueryable<dynamic>> GetAllAsync(string fields = null)
//        {
//            return await Task.FromResult(_context.ContentFiles
//                .Select(LambdaBuilder.BuildSelect<ContentFile>(fields)));
//        }

//        public PagedList<dynamic> GetAll(IPagedModel model)
//        {
//            var total = _context.ContentFiles.FilterBy(model.Filters).Count();
//            var sorted = _context.ContentFiles.OrderBy<ContentFile>(model.Sorts);
//            var filtered = sorted.FilterBy(model.Filters);
//            var paged = filtered/*.Include(e => e.Projects).ThenInclude(p => p.Team)*/.Skip((model.Page - 1) * model.Size).Take(model.Size);
//            var reduced = paged.Select(LambdaBuilder.BuildSelect<ContentFile>(model.Fields));
//            var sculpted = reduced.ShapeData(model.Fields);
//            return new PagedList<dynamic>(sculpted.ToList(), total, model.Page, model.Size);
//        }

//        public async Task<PagedList<dynamic>> GetAllAsync(IPagedModel model)
//        {
//            var total = await _context.ContentFiles.FilterBy(model.Filters).CountAsync();
//            var sorted = _context.ContentFiles.OrderBy<ContentFile>(model.Sorts);
//            var filtered = sorted.FilterBy(model.Filters);
//            var paged = filtered/*.Include(e => e.Projects).ThenInclude(p => p.Team)*/.Skip((model.Page - 1) * model.Size).Take(model.Size);
//            var reduced = paged.Select(LambdaBuilder.BuildSelect<ContentFile>(model.Fields));
//            var sculpted = reduced.ShapeData(model.Fields);
//            return new PagedList<dynamic>(sculpted.ToList(), total, model.Page, model.Size);
//        }

//        public void Update(ContentFile model)
//        {
//            _context.ContentFiles.Update(model);
//            _context.SaveChanges();
//        }

//        public async Task UpdateAsync(ContentFile model)
//        {
//            _context.ContentFiles.Update(model);
//            await _context.SaveChangesAsync();
//        }
//    }
//}