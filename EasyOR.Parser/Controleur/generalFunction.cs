using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyOR.Parser.Controleur
{
    /// <summary>
    /// Fonctions générales de l'application OR
    /// By JOHNATAN GEORGES
    /// Licence MIT
    /// </summary>
    public class generalFunction
    {

        /// <summary>
        /// Recupère tout les nombres contenu dans un texte
        /// </summary>
        /// <param name="text">Texte à traiter</param>
        /// <returns>Liste contenant les nombres du texte</returns>
        public static List<long> getNumberFromText(string text)
        {
            List<long> numberList = new List<long>();
            string intWork = string.Empty;
            foreach (char c in text)
            {
                // si c'est un nombre ou un point,  on le traite
                if ((Char.IsNumber(c)) || (c == '.'))
                {
                    if (Char.IsNumber(c))
                        intWork += c;
                }
                else
                {
                    if (!string.IsNullOrEmpty(intWork))
                    {
                        numberList.Add(Convert.ToInt64(intWork));
                        intWork = string.Empty;
                    }
                }
            }
            return numberList;
        }

        public static string getReadableLong(long value)
        {
            string valueReadable = Convert.ToString(value);


            return string.Empty;
        }
    }
}
