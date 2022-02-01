using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Util
{
    public static class CriptLogin
    {
        public static string Encriptar(string valor)
        {
            return Encriptar(valor, false);
        }

        public static string Encriptar(string valor, bool converterMaiuscula)
        {
            if (converterMaiuscula)
                valor = valor.ToUpper();


            int tamanho = valor.Length;
            char[] str = new char[tamanho];


            int idx = 0;
            for (int i = tamanho - 1; i >= 0; i--)
            {
                str[idx] = Encriptar((int)valor[i], i + 1);
                idx++;
            }


            return new string(str);
        }

        private static char Encriptar(int codigoASCII, int posicao)
        {
            int num = 120 - codigoASCII + ((posicao * 2) + 8);


            if ((num == 34) || (num == 39) || (num == 94) || (num == 96))
                num = num + 1;


            return (char)num;
        }

        public static string Desencriptar(string valor)
        {
            valor = ReverseString(valor);
            int tamanho = valor.Length;
            char[] str = new char[tamanho];


            for (int i = 0; i < tamanho; i++)
            {
                str[i] = Desencriptar((int)valor[i], i + 1);
            }


            return new string(str);
        }

        private static char Desencriptar(int codigoASCII, int posicao)
        {
            if ((codigoASCII == 35) || (codigoASCII == 40) || (codigoASCII == 95) || (codigoASCII == 97))
                codigoASCII = codigoASCII - 1;


            int num = 120 - codigoASCII + ((posicao * 2) + 8);


            return (char)num;
        }

        private static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }



}
