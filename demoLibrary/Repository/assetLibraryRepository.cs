using demoLibrary.Data;
using demoLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace demoLibrary.Repository
{
    public class assetLibraryRepository
    {
        public readonly dataContext _context;

        public assetLibraryRepository(dataContext context)
        {
            _context = context;
        }
        public async Task<assetLibrary> getAssetLibraryManagementAsync(int assetId)
        {
            return await _context.assetLibraries
                .SingleOrDefaultAsync(x => x.bookId == assetId);
        }
    }
}