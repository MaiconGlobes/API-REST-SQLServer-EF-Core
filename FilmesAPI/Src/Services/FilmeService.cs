using EF_Core_Postgre.Src.Services;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmesAPI.Src.Services
{
   public class FilmeService
   {
      private static FilmeService FInstancia { get; set; }

      public static FilmeService Instancia()
      {
         if (FInstancia == null)
         {
            FInstancia = new FilmeService();
         }

         return FInstancia;
      }

      public async Task<List<Filme>> GetFilmes(Contexto Acontexto)
      {
         return await Acontexto.FILME.ToListAsync();
      }
      
      public async Task<object> GetFilmeById(int Aid, Contexto Acontexto)
      {
         return await Acontexto.FILME.FirstOrDefaultAsync(filme => filme.Id == Aid);
      }

      public async Task<object> AddFilme(Filme AFilme, Contexto Acontexto)
      {
         Acontexto.FILME.Add(AFilme);
         return await Acontexto.SaveChangesAsync();
      }

      public async Task<object> DeleteFilmeById(int Aid, Contexto Acontexto)
      {
         var filme = await Acontexto.FILME.FirstOrDefaultAsync(filme => filme.Id == Aid);

         if (filme != null)
         {
            Acontexto.FILME.Remove(filme);
            return await Acontexto.SaveChangesAsync();
         }

         return null;
      }
   }
}
