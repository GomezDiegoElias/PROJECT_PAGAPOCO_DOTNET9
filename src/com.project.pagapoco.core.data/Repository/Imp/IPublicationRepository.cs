using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Response;

namespace com.project.pagapoco.core.data.Repository.Imp
{
    public interface IPublicationRepository
    {
        public Task<PaginatedResponse<Publication>> FindAll(int pageIndex, int pageSize);
        public Task<Publication> FindById(int id);
        public Task<Publication> Save(Publication publication);
        public Task<Publication> Update(Publication publication);
        public Task<bool> DeleteByCode(long code);
    }
}
