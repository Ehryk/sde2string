﻿using System;
using System.Text;
using Core.Extensions;

namespace Core.Encodings
{
    public class Encodings
    {
        public static Encoding GetEncoding(string pEncoding = "Default", bool pLittleEndian = true)
        {
            switch (pEncoding.ToAlphanumeric().ToUpper())
            {
                case "UTF8":
                case "8":
                    return new UTF8Encoding(false, true);

                case "UTF7":
                case "7":
                    return new UTF7Encoding();

                case "ASCII":
                    return new ASCIIEncoding();

                case "UNICODE":
                case "UTF16":
                case "16":
                    return new UnicodeEncoding(!pLittleEndian, false, true);

                case "UTF32":
                case "32":
                    return new UTF32Encoding(!pLittleEndian, false, true);

                case "SYSTEMDEFAULT":
                case "SYSTEM":
                case "SYS":
                case "DEFAULT":
                    return Encoding.Default;

                default:
                    throw new NotSupportedException(String.Format("Unsupported Encoding ({0})", pEncoding));
            }
        }
    }
}
