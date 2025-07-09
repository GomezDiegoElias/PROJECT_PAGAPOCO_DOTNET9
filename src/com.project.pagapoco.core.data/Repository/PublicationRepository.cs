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

        public async Task<Publication> Update(Publication publication)
        {
            
            var publicationUpdate = await this.FindByCode(publication.CodePublication)
                ?? throw new KeyNotFoundException($"Publication with code {publication.CodePublication} not found");

            publicationUpdate.Title = publication.Title;
            publicationUpdate.Description = publication.Description;
            publicationUpdate.Price = publication.Price;
            
            publicationUpdate.Brand = publication.Brand;
            publicationUpdate.Model = publication.Model;
            publicationUpdate.Year = publication.Year;
            publicationUpdate.UpdatedAt = DateTime.UtcNow;

            await _dbContenxt.SaveChangesAsync();

            return publicationUpdate;

        }

        public async Task<bool> DeleteByCode(long code)
        {
            var publication = await this.FindByCode(code);

            if (publication == null) return false;

            _dbContenxt.Remove(publication);
            await _dbContenxt.SaveChangesAsync();

            return true;
        
        }

    }
}
