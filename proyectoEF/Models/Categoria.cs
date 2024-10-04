using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace proyectoEF.Models
{
    //creamos un modelo que vamos a utilizar
    public class Categoria
    {
        //Agregamos cada una de las atributos que va a tener esta clase y se terminaran tranformando en tablas en la BBDD

        //DataAnnotations
       // [Key]  //clave principal cuando se crea la BBDD
        public Guid CategoriaId { get; set; }
       // [Required]// Esta propiedad sera de forma obligatoria cuando nostros querramos insertar datos a la tabla
        //[MaxLength(150)] //que no sobrepasa es cantida de caracteres
        public string Name { get; set; }

        public string Description { get; set; }

        public string Peso { get; set; }

        public string Cama { get; set; }


        [JsonIgnore]
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
