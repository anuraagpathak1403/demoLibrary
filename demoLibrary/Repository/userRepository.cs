using demoLibrary.Data;
using demoLibrary.DTO;
using demoLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace demoLibrary.Repository
{
    public class userRepository
    {
        public readonly dataContext _context;

        public userRepository(dataContext context)
        {
            _context = context;
        }
        public async Task<registration> getUserDTOAsync(string enrollmentId)
        {
            return await _context.registrations
                .SingleOrDefaultAsync(x => x.enrollmentId == enrollmentId);
        }
    }
}