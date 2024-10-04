using Microsoft.EntityFrameworkCore;
using proyectoEF.Models;
using System.Security.Cryptography.X509Certificates;

namespace proyectoEF
{
    // esta clase representa el contexto de EntityFramework

    public class TareasContext : DbContext // heredamos la clase dbContext que contiene todos componentes para poder crear la configuracion de BBDD
    {
        //creamos un DbSet que es una tabla dentro del contexto de entity framework para ambas tablas

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Tarea> Tareas { get; set; }

        // ahora creamos el metodo base del constructor de EF

        public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }
        
        
            // vamos a sobrescribir para hacer las configuracion de nuestro modelo (fluent Api)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // hacemos una colection para agregar datos iniciales a a BBDD
            List<Categoria> categoriaInit = new List<Categoria>();
            categoriaInit.Add(new Categoria() { CategoriaId = Guid.Parse("54210ee2-e34f-4a73-87ba-5be75a47c3b6"), Name = "Activades pendientes", Cama = "20"  });
            categoriaInit.Add(new Categoria() { CategoriaId = Guid.Parse("54210ee2-e34f-4a73-87ba-5be75a47c3c4"), Name = "Activades personales", Cama = "50" });

            modelBuilder.Entity<Categoria>(categoria =>
            {   //creamos la tabla y por regla de normalizacion de BBDD las tablas tienen que estar en singular
                categoria.ToTable("Categoria");
                categoria.HasKey(p => p.CategoriaId);
                
                categoria.Property(p => p.Name).IsRequired().HasMaxLength(150);
                categoria.Property(p => p.Description).IsRequired(false);
                categoria.Property(p => p.Peso).IsRequired(false);
                categoria.Property(p=>p.Cama);

                categoria.HasData(categoriaInit);


            });

            List<Tarea> tareaInit = new List<Tarea>();
            tareaInit.Add(new Tarea() { TareaId = Guid.Parse("54210ee2-e34f-4a73-87ba-5be75a47c310"), CategoriaId = Guid.Parse("54210ee2-e34f-4a73-87ba-5be75a47c3b6"), PriorityTask = Priority.Media, Title = "pago de servivios pulico", CreatedDate = DateTime.Now }); ;
            tareaInit.Add(new Tarea() { TareaId = Guid.Parse("54210ee2-e34f-4a73-87ba-5be75a47c311"), CategoriaId = Guid.Parse("54210ee2-e34f-4a73-87ba-5be75a47c3c4"), PriorityTask = Priority.Baja, Title = "terminar de ver peliculas", CreatedDate = DateTime.Now }); ;

            modelBuilder.Entity<Tarea>(tarea =>
            {
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaId);
            tarea.HasOne(p => p.Categoria).WithMany(p =>p.Tareas).HasForeignKey(p=> p.CategoriaId);

            tarea.Property(p => p.Title).IsRequired().HasMaxLength(200);
            tarea.Property(p => p.Descripcion).IsRequired(false);
            tarea.Property(p => p.PriorityTask);
            tarea.Property(p => p.CreatedDate);
            tarea.Ignore(p => p.Resumen);
                tarea.HasData(tareaInit);
            });

            

        }
        
        
    }
}
