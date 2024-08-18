using Decomposition.Worker.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decomposition.Worker.Services
{
    public class CamundaService
    {
        private readonly CamundaDbContext _context;

        public CamundaService(CamundaDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetNextWoidAsync()
        {
            var maxBusinessKey = await _context.ACT_HI_PROCINST
                .Where(x => x.BUSINESS_KEY_ != null)
                .MaxAsync(x => Convert.ToInt32(x.BUSINESS_KEY_));

            return maxBusinessKey + 1;
        }
    }

}
