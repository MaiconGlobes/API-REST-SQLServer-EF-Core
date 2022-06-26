using System.Collections.Generic;

namespace FilmesAPI.Src.Utils
{
   public class RetornoUtils
   {
      private static RetornoUtils FInstancia { get; set; }
      private object FObjRetorno { get; set; }

      public static RetornoUtils Instancia()
      {
         if (FInstancia == null)
         {
            FInstancia = new RetornoUtils();
         }
         return FInstancia;
      }

      internal virtual object RetornoOk<T>(List<T> dados)
      {
         FObjRetorno = new
         {
            retorno = new
            {
               dados = dados
            }
         };

         return FObjRetorno;
      }

      internal virtual object RetornoOk(object dados)
      {
         FObjRetorno = new
         {
            retorno = new
            {
               dados = dados
            }
         };

         return FObjRetorno;
      }

      internal object RetornoMensagem(string msg)
      {
         FObjRetorno = new
         {
            retorno = new
            {
               msg
            }
         };

         return FObjRetorno;
      }
   }
}
