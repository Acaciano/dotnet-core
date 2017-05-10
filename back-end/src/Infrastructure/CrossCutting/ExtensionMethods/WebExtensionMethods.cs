using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.CrossCutting.ExtensionMethods
{
    public static class WebExtensionMethods
    {
        #region "Public"

        public static List<T> AddDefaultItem<T>(this List<T> list, string dataValueField, string dataTextField, string dataValueValue = "0", string dataTextValue = "Selecione")
        {
            try
            {
                var instance = Activator.CreateInstance(typeof(T));
                PropertyInfo propertyInfo;
                if (instance.GetType().GetProperties().Select(x => x.Name).Contains(dataTextField))
                {
                    propertyInfo = instance.GetType().GetProperty(dataTextField);
                    propertyInfo.SetValue(instance, Convert.ChangeType(dataTextValue, propertyInfo.PropertyType),
                    null);
                }

                if (instance.GetType().GetProperties().Select(x => x.Name).Contains(dataValueField))
                {
                    propertyInfo = instance.GetType().GetProperty(dataValueField);
                    propertyInfo.SetValue(instance, Convert.ChangeType(dataValueValue, propertyInfo.PropertyType),
                    null);
                }
                list.Insert(0, (T)instance);
            }
            catch
            {
                return list;
            }
            return list;
        }

        public static string UrlFriendly(this string value)
        {
            if (value == null) return "";

            const int maxlen = 80;
            int len = value.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = value[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }

        public static string ToDayMonthYear(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        public static string OnlyNumbers(this String regex)
        {
            return regex != null ? Regex.Replace(regex, @"\D", "") : null;
        }

        public static string ToReal(this decimal? value)
        {
            return String.Format("{0:C}", value);
        }
        public static string ToReal(this decimal value)
        {
            return String.Format("{0:C}", value);
        }

        public static string ToReal(this string value)
        {
            decimal valueDecimal;
            decimal.TryParse(value, out valueDecimal);

            return String.Format("{0:C}", valueDecimal);
        }

        public static int GetSkip(this int? page, int take)
        {
            page = page == 0 ? null : page;
            var valuePage = page ?? 1;
            var skip = (valuePage * take) - take;

            return skip;
        }

        public static decimal ToRound(this decimal value, int decimals)
        {
            return Math.Round(value, 2);
        }

        public static string GeneratePassword(this int length)
        {
            string guid = Guid.NewGuid().ToString().OnlyNumbers();

            return guid.Substring(0, length);
        }

        public static string MaskCnpj(this string value)
        {
            return Convert.ToUInt64(value).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string MaskCpf(this string value)
        {
            return Convert.ToUInt64(value).ToString(@"000\.000\.000\-00");
        }

        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }

        public static bool IsCnpjValid(this string cnpj)
        {
            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            if (cnpj == "00000000000000" ||
                cnpj == "11111111111111" ||
                cnpj == "22222222222222" ||
                cnpj == "33333333333333" ||
                cnpj == "44444444444444" ||
                cnpj == "55555555555555" ||
                cnpj == "66666666666666" ||
                cnpj == "77777777777777" ||
                cnpj == "88888888888888" ||
                cnpj == "99999999999999")
                return false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                     cnpj.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                        int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);

                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));

                }

                return (CNPJOk[0] && CNPJOk[1]);

            }
            catch
            {
                return false;
            }
        }

        public static bool IsCpf(this string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            var listarCpf = new List<string>
                                {
                                    "00000000000",
                                    "11111111111",
                                    "22222222222",
                                    "33333333333",
                                    "44444444444",
                                    "55555555555",
                                    "66666666666",
                                    "88888888888",
                                    "99999999999"
                                };

            foreach (var cpfsInvalidos in listarCpf)
            {
                if (cpf.Contains(cpfsInvalidos))
                {
                    return false;
                }
            }

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool EmailIsValid(this string email)
        {

            Regex regExpEmail = new Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$");
            Match match = regExpEmail.Match(email);

            if (match.Success)
                return true;

            return false;
        }

        public static bool IsNullOrEmpty(this String value)
        {
            if (string.IsNullOrEmpty(value))
                return true;

            return false;
        }

        public static void SaveFile(this byte[] fileBytes, string path, string fileName)
        {
            try
            {
                CreateDirectory(path);

                System.IO.File.WriteAllBytes(path + fileName, fileBytes);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        private static void CreateDirectory(string caminho)
        {
            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);
        }

        public static string ToSafeString(this object obj)
        {
            return obj != null ? obj.ToString() : String.Empty;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetMethodName(MethodBase targetSite)
        {
            if (targetSite.DeclaringType != null)
                return targetSite.DeclaringType.FullName + "." + targetSite.Name;
            return null;
        }

        #endregion
    }
}