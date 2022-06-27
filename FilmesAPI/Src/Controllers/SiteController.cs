using EF_Core_Postgre.Src.Services;
using FilmesAPI.Src.Models;
using FilmesAPI.Src.Services;
using FilmesAPI.Src.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class SiteController
   {
      private Contexto Fcontexto { get; set; }
      private object FObjRetorno { get; set; }

      public SiteController()
      {
         Fcontexto = new Contexto();
      }

      [HttpGet]
      public async Task<IActionResult> GetSites()
      {
         try
         {
            object sites = await SiteService.Instancia().GetSites(Fcontexto);

            if (sites != null)
               FObjRetorno = RetornoUtils.Instancia().RetornoOk(sites);
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
      public async Task<IActionResult> GetSiteById(int id)
      {
         try
         {
            object site = await SiteService.Instancia().GetSiteById(id, Fcontexto);

            if (site != null)
            {
               FObjRetorno = RetornoUtils.Instancia().RetornoOk(site);

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
      public async Task<IActionResult> AddSite([FromBody] Site Site)
      {
         try
         {
            object site = await SiteService.Instancia().GetSiteByUrl(Site.url, Fcontexto);

            if (site == null)
            {
               await SiteService.Instancia().AddSite(Site, Fcontexto);

               return new CreatedResult(nameof(GetSiteById), Site);
            }
            else
            {
               FObjRetorno = new
               {
                  retorno = new
                  {
                     msg = "Registro já exixte no banco de dados"
                  }
               };
            }

            return new ConflictObjectResult(FObjRetorno);
         }
         catch (Exception e)
         {
            Console.WriteLine(e.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
         }
      }

      [HttpDelete]
      public async Task<IActionResult> DeleteSiteById([FromQuery] int id)
      {
         try
         {
            object site = await SiteService.Instancia().DeleteSiteById(id, Fcontexto);

            if (site != null)
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
