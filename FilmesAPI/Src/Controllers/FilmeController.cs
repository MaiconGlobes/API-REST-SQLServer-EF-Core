using EF_Core_Postgre.Src.Services;
using FilmesAPI.Models;
using FilmesAPI.Src.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FilmesAPI.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class FilmeController
   {
      public Contexto Fcontexto { get; set; }
      private object FObjRetorno { get; set; }

      public FilmeController()
      {
         Fcontexto = new Contexto();
      }

      [HttpGet]
      public IActionResult GetFilmes()
      {
         try
         {
            var filmes = Fcontexto.FILME.ToList();

            if (filmes.Count > 0)
               FObjRetorno = RetornoUtils.Instancia().RetornoOk(filmes);
            else
               FObjRetorno = RetornoUtils.Instancia().RetornoMensagem("Não há registros para listar");

            return new OkObjectResult(FObjRetorno);
         }
         catch
         {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
         }
      }

      [HttpGet("{id}")]
      public IActionResult GetFilmeById(int id)
      {
         try
         {
            var filme = Fcontexto.FILME.FirstOrDefault(filme => filme.Id == id);

            if (filme != null)
            {
               FObjRetorno = RetornoUtils.Instancia().RetornoOk(filme);

               return new OkObjectResult(FObjRetorno);
            }
            else
            {
               FObjRetorno = RetornoUtils.Instancia().RetornoMensagem("Não há registros para listar");
            }

            return new NotFoundObjectResult(FObjRetorno);
         }
         catch
         {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
         }
      }

      [HttpPost]
      public IActionResult AddFilme([FromBody] Filme Filme)
      {
         try
         {
            Fcontexto.FILME.Add(Filme);
            Fcontexto.SaveChanges();
            
            return new CreatedResult(nameof(GetFilmeById), Filme);
         }
         catch
         {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
         }
      }

      [HttpDelete]
      public IActionResult DeleteFilmeById([FromQuery] int id)
      {
         try
         {
            var filme = Fcontexto.FILME.FirstOrDefault(filme => filme.Id == id);

            if (filme != null)
            {
               Fcontexto.FILME.Remove(filme);
               Fcontexto.SaveChanges();

               return new NoContentResult();
            }
            return new NotFoundResult();
         }
         catch
         {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
         }
      }
   }
}
