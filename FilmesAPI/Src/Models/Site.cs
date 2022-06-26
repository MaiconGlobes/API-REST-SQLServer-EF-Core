using FilmesAPI.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FilmesAPI.Src.Models
{
   [Table("SITE", Schema = "Filme")]
   public class Site
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      [Required(ErrorMessage = "nome é obrigatório")]
      [StringLength(35, ErrorMessage = "Título deve ter no máximo 35 caracteres")]
      public string nome { get; set; }

      [Required(ErrorMessage = "url é obrigatório")]
      [StringLength(255, ErrorMessage = "url deve ter no máximo 255 caracteres")]
      public string url { get; set; }

      [Required(ErrorMessage = "filmeId é obrigatório")]
      public int? filmeId { get; set; }

      [JsonIgnore]
      public virtual Filme Filmes { get; set; }
   }
}
