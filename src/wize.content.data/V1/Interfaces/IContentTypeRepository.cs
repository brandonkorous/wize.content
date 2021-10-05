using System;
using System.Collections.Generic;
using System.Text;
using wize.content.data.v1.Models;
using wize.common.use.repository.Interfaces;
using wize.common.use.paging.Interfaces;
using System.Threading.Tasks;

namespace wize.content.data.v1.Interfaces
{
    public interface IContentTypeRepository : IRepositoryBase<int, ContentType>
    {
        Task<IPagedResponse> GetBySafeTitleAsync(string safeTitle, IQueryModel model);
    }
}
