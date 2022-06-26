using EF_Core_Postgre.Src.Services;
using FilmesAPI.Models;
using FilmesAPI.Src.Models;
using FilmesAPI.Src.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FilmesAPI.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class SiteController
   {
      public Contexto Fcontexto { get; set; }
      private object FObjRetorno { get; set; }

      public SiteController()
      {
         Fcontexto = new Contexto();
      }

      [HttpGet]
      public IActionResult GetSites()
      {
         try
         {
            var sites = Fcontexto.SITE.ToList();

            if (sites.Count > 0)
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
      public IActionResult GetSiteById(int id)
      {
         try
         {
            var site = Fcontexto.SITE.FirstOrDefault(site => site.Id == id);

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
      public IActionResult AddSite([FromBody] Site Site)
      {
         try
         {
            string url = Site.url;

            var site = Fcontexto.SITE.FirstOrDefault(site => site.url == url);

            if (site == null)
            {
               Fcontexto.SITE.Add(Site);
               Fcontexto.SaveChanges();

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
         catch
         {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
         }
      }

      [HttpDelete]
      public IActionResult DeleteSiteById([FromQuery] int id)
      {
         try
         {
            var site = Fcontexto.SITE.FirstOrDefault(site => site.Id == id);

            if (site != null)
            {
               Fcontexto.SITE.Remove(site);
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
