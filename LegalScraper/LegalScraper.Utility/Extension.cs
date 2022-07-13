using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LegalScraper.Utility
{
    public static class Extension
    {
        public static string RemoveAcentos(this string texto)
        {
            string Acentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < Acentos.Length; i++)
            {
                texto = texto.Replace(Acentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }


        /// <summary>
        /// Gera um número de CNJ FICTÍCIO somente para evitar chave UNIQUE duplicada na base de dados quando consumida API de Update 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GeraNumeroPadraoCNJ(this string text)
        {
            string[] grupos = { "7", "-", "2", ".", "4", ".", "1", ".", "2", ".", "4"} ;
            Random grupoNum = new Random();
            string numCNJ = string.Empty;

            for (int i = 0; i < grupos.Length; i++)
            {
                int n;
                var isNumeric = int.TryParse(grupos[i], out n);
                if (isNumeric)
                {
                    for (int j = 0; j < n; j++)
                    {
                        numCNJ += grupoNum.Next(1, 9).ToString();
                    }
                }
                else
                    numCNJ += grupos[i];
            }
            
            return numCNJ;
        }
    }
}
