using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcFamilyMeetings.Models;

namespace MvcFamilyMeetings.Models
{
    public class MvcFamilyMeetingsContext : DbContext
    {
        public MvcFamilyMeetingsContext (DbContextOptions<MvcFamilyMeetingsContext> options)
            : base(options)
        {
        }

        public DbSet<MvcFamilyMeetings.Models.Member> Member { get; set; }

        public DbSet<MvcFamilyMeetings.Models.Meeting> Meeting { get; set; }

        public DbSet<MvcFamilyMeetings.Models.Saving> Saving { get; set; }

        public DbSet<MvcFamilyMeetings.Models.Loan> Loan { get; set; }
    }
}
