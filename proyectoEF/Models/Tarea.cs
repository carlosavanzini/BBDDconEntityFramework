using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoEF.Models
{
    //creamos un modelo que vamos a utilizar los cuales seran las tablas en sql
    public class Tarea    {
        //Agregamos cada una de las atributos que va a tener esta clase
        //De esta forma estamos relacionando que tiene Tarea con Categoria

        [Key]
        public Guid TareaId { get; set; }
        [ForeignKey("CategoriaId")] // esto no dice que va a tener una relacion de este modelo de tarea con el otro modelo de Categoria por medio de CategoriaId. (ClaveForanea)
        public Guid CategoriaId { get; set; }

        //[Required]
        //[MaxLength(200)]
        public string Title { get; set; }

        public string Descripcion { get; set; }

        public Priority PriorityTask { get; set; }

        public DateTime CreatedDate { get; set; }

        //creamos esta propiedad lo cual nos va permitir relacionar un modelo con el otro modelo

        public virtual Categoria Categoria { get; set; }

        //[NotMapped] // omita el campo en la BBDD
        public string Resumen { get; set; } // esto lo podemos usar cuando queremos hacer un resumen de la descripcion y a su vez que no se guarde en la BBDD

       
    }
    //agregamos la propiedad de prioridad por medio de numeracion

    public enum Priority
    {
        Baja,
        Media,
        Alta
    }
}
