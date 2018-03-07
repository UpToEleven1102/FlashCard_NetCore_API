using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlashCard.Models;

namespace FlashCard.Infrastructures
{
    public class FlashCardContext : DbContext
    {
        public FlashCardContext (DbContextOptions<FlashCardContext> options)
            : base(options)
        {
        }

        public DbSet<FlashCard.Models.CardEntity> CardEntity { get; set; }

        public DbSet<FlashCard.Models.CardSetEntity> CardSetEntity { get; set; }
    }
}
