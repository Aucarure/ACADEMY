using AcademiaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademiaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Instructor> Instructores => Set<Instructor>();
        public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
        public DbSet<Curso> Cursos => Set<Curso>();
        public DbSet<Matricula> Matriculas => Set<Matricula>();
        public DbSet<Pago> Pagos => Set<Pago>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Instructor 1 --- N Curso
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Cursos)
                .HasForeignKey(c => c.IdInstructor)
                .OnDelete(DeleteBehavior.Restrict);

            // Estudiante 1 --- N Matricula
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Estudiante)
                .WithMany(e => e.Matriculas)
                .HasForeignKey(m => m.IdEstudiante)
                .OnDelete(DeleteBehavior.Restrict);

            // Curso 1 --- N Matricula
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Matriculas)
                .HasForeignKey(m => m.IdCurso)
                .OnDelete(DeleteBehavior.Restrict);

            // Matricula 1 --- N Pago
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Matricula)
                .WithMany(m => m.Pagos)
                .HasForeignKey(p => p.IdMatricula)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
