using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.data.Repository.Imp;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Response;

namespace com.project.pagapoco.core.data.Repository
{
    public class PublicationRepository : IPublicationRepository
    {

        private readonly AppDbContext _dbContenxt;

        public PublicationRepository(AppDbContext dbContext)
        {
            _dbContenxt = dbContext;
        }

        public Task<PaginatedResponse<Publication>> FindAll(int pageIndex, int pageSize)
        {
            return _dbContenxt.getPublicationPagination(pageIndex, pageSize);
        }

        public Task<Publication> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Publication> Save(Publication publication)
        {
            throw new NotImplementedException();
        }

        public Task<Publication> Update(Publication publication)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByCode(long code)
        {
            throw new NotImplementedException();
        }

    }
}
