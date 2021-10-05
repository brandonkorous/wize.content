using System;
using System.Collections.Generic;
using System.Text;
using wize.content.data.v1.Models;
using wize.common.use.repository.Interfaces;
using System.Threading.Tasks;

namespace wize.content.data.v1.Interfaces
{
    public interface IContentRepository : IRepositoryBase<int, Content>
    {
        Task<dynamic> GetBySafeTitleAsync(string safeTitle, string fields = "");
    }
}
