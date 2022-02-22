using Ass_10.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ass_10.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Student>? Students { get; set; }
}