using FilmesAPI.Src.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmesAPI.Models
{
   [Table("FILME", Schema = "Filme")]
   public class Filme
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      [Required(ErrorMessage = "Título é obrigatório")]
      [StringLength(50, ErrorMessage = "Título deve ter no máximo 50 caracteres")]
      public string titulo { get; set; }

      [Required(ErrorMessage = "diretor é obrigatório")]
      [StringLength(25, ErrorMessage = "Título deve ter no máximo 25 caracteres")]
      public string diretor { get; set; }

      [StringLength(20, ErrorMessage = "Título deve ter no máximo 20 caracteres")]
      public string genero { get; set; }

      [Required(ErrorMessage = "Duração é obrigatório")]
      [Range(1, 180, ErrorMessage = "duracao deve estar entre 1 e 180")]
      public int? duracao { get; set; }

      [Required(ErrorMessage = "imdb é obrigatório")]
      [Range(1, 10, ErrorMessage = "imdb deve estar entre 1 e 10")]
      public float? imdb { get; set; }

      public virtual List<Site> sites { get; set; }
   }
}
