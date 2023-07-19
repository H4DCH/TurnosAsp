using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Models
{
    public class TurnosContext : DbContext
    {
        public DbSet<Especialidad> Especialidades {get; set;}
        public DbSet<Paciente> Pacientes { get; set;}
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<MedicoEspecialidad>? MedicoEspecialidad { get; set; }   
        public DbSet<Turno>?Turnos { get; set; }    
        public DbSet<Login>?Logins { get; set; }    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Especialidad>(entidad => {
                entidad.ToTable("Especialidades");
                entidad.HasKey(x => x.Id);
                entidad.Property(x => x.Descripcion)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);   
                });

            modelBuilder.Entity<Paciente>(entidad =>
            {
                entidad.ToTable("Pacientes");

                entidad.HasKey( p => p.IdPaciente);

                entidad.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entidad.Property(p => p.Apellido)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entidad.Property(p => p.Direccion)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

                entidad.Property(p => p.Telefono)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            });
            modelBuilder.Entity<MedicoEspecialidad>().HasKey(x => new { x.IdMedico, x.IdEspecialidad });

            modelBuilder.Entity<MedicoEspecialidad>().HasOne(x => x.Medicos)
                .WithMany(p => p.MedicoEspecialidad)
                .HasForeignKey(p => p.IdMedico);

            modelBuilder.Entity<MedicoEspecialidad>().HasOne(x => x.especialidades)
               .WithMany(p => p.MedicoEspecialidad)
               .HasForeignKey(p => p.IdEspecialidad);

            modelBuilder.Entity<Turno>().HasOne(x => x.Pacientes)
               .WithMany(p => p.Turnos)
               .HasForeignKey(p => p.IdPaciente);

            modelBuilder.Entity<Turno>().HasOne(x => x.Medicos)
               .WithMany(p => p.Turnos)
               .HasForeignKey(p => p.IdMedico);
            
                
        }





        public TurnosContext(DbContextOptions<TurnosContext> options) : base(options)
        {
        }

        
        

    }
}
