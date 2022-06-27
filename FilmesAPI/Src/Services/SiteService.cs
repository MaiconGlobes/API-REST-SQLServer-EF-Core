using EF_Core_Postgre.Src.Services;
using FilmesAPI.Src.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmesAPI.Src.Services
{
   public class SiteService
   {
      private static SiteService FInstancia { get; set; }

      public static SiteService Instancia()
      {
         if (FInstancia == null)
         {
            FInstancia = new SiteService();
         }

         return FInstancia;
      }

      public async Task<List<Site>> GetSites(Contexto Acontexto)
      {
         return await Acontexto.SITE.ToListAsync();
      }

      public async Task<object> GetSiteById(int Aid, Contexto Acontexto)
      {
         return await Acontexto.SITE.FirstOrDefaultAsync(site => site.Id == Aid);
      }

      public async Task<object> GetSiteByUrl(string AUrl, Contexto Acontexto)
      {
         return await Acontexto.SITE.FirstOrDefaultAsync(site => site.url == AUrl);
      }

      public async Task<object> AddSite(Site ASite, Contexto Acontexto)
      {
         Acontexto.SITE.Add(ASite);
         return await Acontexto.SaveChangesAsync();
      }

      public async Task<object> DeleteSiteById(int Aid, Contexto Acontexto)
      {
         var site = await Acontexto.SITE.FirstOrDefaultAsync(Site => Site.Id == Aid);

         if (site != null)
         {
            Acontexto.SITE.Remove(site);
            return await Acontexto.SaveChangesAsync();
         }

         return null;
      }
   }
}
