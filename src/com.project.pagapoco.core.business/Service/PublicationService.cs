using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.business.Service.Imp;
using com.project.pagapoco.core.data.Repository.Imp;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Response;

namespace com.project.pagapoco.core.business.Service
{
    public class PublicationService : IPublicationService
    {

        private readonly IPublicationRepository _publicationRepository;

        public PublicationService(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }
        
        public Task<PaginatedResponse<Publication>> GetAllPublications(int pageIndex, int pageSize)
        {
            return _publicationRepository.FindAll(pageIndex, pageSize);
        }

        public Task<Publication?> GetPublicationByCode(long code)
        {
            return _publicationRepository.FindByCode(code);
        }

        public Task<Publication> SavePublication(Publication publication)
        {
            throw new NotImplementedException();
        }

        public Task<Publication> UpdatePublication(Publication publication)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePublication(long code)
        {
            throw new NotImplementedException();
        }

    }
}
