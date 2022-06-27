using EF_Core_Postgre.Src.Services;
using FilmesAPI.Models;
using FilmesAPI.Src.Services;
using FilmesAPI.Src.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class FilmeController
   {
      private Contexto Fcontexto { get; set; }
      private object FObjRetorno { get; set; }

      public FilmeController()
      {
         Fcontexto = new Contexto();
      }

      [HttpGet]
      public async Task<IActionResult> GetFilmes()
      {
         try
         {
            List<Filme> filmes = await FilmeService.Instancia().GetFilmes(Fcontexto);

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
      public async Task<IActionResult> GetFilmeById(int id)
      {
         try
         {
            var filme = await FilmeService.Instancia().GetFilmeById(id, Fcontexto);

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
      public async Task<IActionResult> AddFilme([FromBody] Filme Filme)
      {
         try
         {
            await FilmeService.Instancia().AddFilme(Filme, Fcontexto);

            Console.WriteLine(Filme.titulo);
            return new CreatedResult(nameof(GetFilmeById), Filme);
         }
         catch (Exception e)
         {
            Console.WriteLine(e.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
         }
      }

      [HttpDelete]
      public async Task<IActionResult> DeleteFilmeById([FromQuery] int id)
      {
         try
         {
            var filme = await FilmeService.Instancia().DeleteFilmeById(id, Fcontexto);

            if (filme != null)
            {
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
