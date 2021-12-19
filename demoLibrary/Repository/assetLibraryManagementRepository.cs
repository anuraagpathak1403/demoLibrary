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
    public class assetLibraryManagementRepository
    {
        public readonly dataContext _context;

        public assetLibraryManagementRepository(dataContext context)
        {
            _context = context;
        }
        public async Task<assetLibraryManagement> getAssetLibraryManagementAsync(int assetId)
        {
            return await _context.assetLibraryManagements
                .SingleOrDefaultAsync(x => x.assetId == assetId);
        }
        public async Task<assetLibraryManagement> getBookAssetLibraryManagementAsync(string enrollmentId)
        {
            return await _context.assetLibraryManagements
                .SingleOrDefaultAsync(x => x.enrollmentId == enrollmentId);
        }

        internal void update(assetLibraryManagement assetLibraryManagement)
        {
            _context.Entry(assetLibraryManagement).State = EntityState.Modified;
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}