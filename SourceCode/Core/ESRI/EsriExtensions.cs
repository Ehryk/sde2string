using System;
using System.Text;
using System.Collections.Generic;
using log4net;
using ESRI.ArcGIS.esriSystem;

namespace Core.ESRI
{
    public static class Extensions
    {
        #region Private Properties

        private static readonly log4net.ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion

        #region IEnumBSTR Extensions

        public static List<string> ToList(this IEnumBSTR pEnum)
        {
            List<string> modelNames = new List<string>();
            string modelName = pEnum.Next();

            try
            {
                while (!String.IsNullOrEmpty(modelName))
                {
                    modelNames.Add(modelName);
                    modelName = pEnum.Next();
                }
            }
            catch (Exception ex)
            {
                //Iteration Over
                _log.Warn(String.Format("Iteration of {0} threw exception: {1}", pEnum, ex.Message), ex);
            }

            return modelNames;
        }

        #endregion

        #region IObjectClass Retrieval

        #endregion

        #region IField Retrieval

        public static IField GetField(this IObjectClass pObjectClass, string pName)
        {
            if (pObjectClass is null)
                throw new ArgumentException("pObjectClass");

            int index = pObjectClass.FindField(pName);
            if (index < 0)
                return null;

            return pObjectClass.Fields.Field[index];
        }

        #endregion

        #region IPropertySet Extensions

        public static string ToPropertiesString(this IPropertySet pPropertySet, bool pBracketless = false)
        {
            if (pPropertySet == null)
                throw new ArgumentNullException("propertySet");

            string format = pBracketless ? "{0}={1};" : "[{0}]={1}";

            return pPropertySet.ToPropertiesString(format);
        }

        public static string ToPropertiesString(this IPropertySet pPropertySet, string pFormat = "{0}={1}\r\n")
        {
            if (pPropertySet == null)
                throw new ArgumentNullException("propertySet");

            StringBuilder result = new StringBuilder();

            foreach (var property in pPropertySet.ToDictionary())
            {
                result.AppendFormat(pFormat, property.Key, property.Value);
            }

            return result.ToString().Trim();
        }

        public static Dictionary<string, string> ToDictionary(this IPropertySet pPropertySet)
        {
            if (pPropertySet == null)
                throw new ArgumentNullException("propertySet");

            int propertyCount = pPropertySet.Count;
            var dictionary = new Dictionary<string, string>();

            pPropertySet.GetAllProperties(out object nameArray, out object valueArray);
            object[] names = (object[])nameArray;
            object[] values = (object[])valueArray;

            for (int i = 0; i < propertyCount; i++)
            {
                dictionary.Add(names[i].ToString(), values[i].ToString());
            }

            return dictionary;
        }

        #endregion
    }
}
