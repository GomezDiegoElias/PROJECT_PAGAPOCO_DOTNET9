using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.data.Repository.Imp;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Response;
using Microsoft.EntityFrameworkCore;

namespace com.project.pagapoco.core.data.Repository
{
    public class PublicationRepository : IPublicationRepository
    {

        private readonly AppDbContext _dbContenxt;

        public PublicationRepository(AppDbContext dbContext)
        {
            _dbContenxt = dbContext;
        }

        public async Task<PaginatedResponse<Publication>> FindAll(int pageIndex, int pageSize)
        {
            return await _dbContenxt.getPublicationPagination(pageIndex, pageSize);
        }

        public async Task<Publication?> FindByCode(long code)
        {
            return await _dbContenxt.Publications.FirstOrDefaultAsync(p => p.CodePublication == code);
        }

        public async Task<Publication> Save(Publication publication)
        {
            await _dbContenxt.AddAsync(publication);
            await _dbContenxt.SaveChangesAsync();
            return publication;
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
