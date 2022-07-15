using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class QuizContext : IdentityDbContext<IdentityUser>
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options) {}
    }
}