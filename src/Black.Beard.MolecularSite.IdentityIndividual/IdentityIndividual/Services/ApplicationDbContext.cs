using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bb.IdentityIndividual.Services
{

    public class ApplicationDbContext : IdentityDbContext
    {


        static ApplicationDbContext()
        {
                    
        }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            
            

        }

    }

}
