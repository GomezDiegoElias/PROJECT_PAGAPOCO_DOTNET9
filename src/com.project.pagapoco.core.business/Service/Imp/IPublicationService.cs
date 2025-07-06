using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.project.pagapoco.core.entities;
using com.project.pagapoco.core.entities.Dto.Response;

namespace com.project.pagapoco.core.business.Service.Imp
{
    public interface IPublicationService
    {
        Task<PaginatedResponse<Publication>> GetAllPublications(int pageIndex, int pageSize);
        Task<Publication> GetPublicationByCode(long code);
        Task<Publication> SavePublication(Publication publication);
        Task<Publication> UpdatePublication(Publication publication);
        Task<bool> DeletePublication(long code);
    }
}
