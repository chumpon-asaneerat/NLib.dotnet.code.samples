﻿#region Using

using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLib.Reflection;

#endregion

namespace NLib
{
    #region ObjectPropertiesToString

    /// <summary>
    /// ObjectPropertiesToString Extnsion Methods
    /// </summary>
    public static class ObjectPropertiesToString
    {
        #region Static Variable

        private static Dictionary<Type, List<PropertyInfo>> Caches = new Dictionary<Type, List<PropertyInfo>>();

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets Properties of target type.
        /// </summary>
        /// <param name="targetType">The Target type.</param>
        /// <returns>Returns all property that has attribute ExcelColumnAttribute.</returns>
        public static List<PropertyInfo> GetProperties(Type targetType)
        {
            if (null == targetType)
                return null;
            if (!Caches.ContainsKey(targetType))
            {
                var properties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
                Caches.Add(targetType, properties);
            }
            return Caches[targetType];
        }
        /// <summary>
        /// Avaliable supports type for dump property.
        /// </summary>
        public static readonly List<Type> SupportTypes = new List<Type>(new Type[] 
        {
            typeof(string),
            typeof(bool), typeof(bool?),
            typeof(short), typeof(int), typeof(long),
            typeof(short?), typeof(int?), typeof(long?),
            typeof(ushort), typeof(uint), typeof(ulong),
            typeof(ushort?), typeof(uint?), typeof(ulong?),
            typeof(decimal), typeof(float), typeof(double),
            typeof(decimal?), typeof(float?), typeof(double?),
            typeof(DateTime), typeof(DateTime?),
            typeof(TimeSpan), typeof(TimeSpan?),
            typeof(Guid), typeof(Guid?)
        }); 
        /// <summary>
        /// Checks is supports type.
        /// </summary>
        /// <param name="type">Target type.</param>
        /// <returns>Returns true if type is supports to debug dump.</returns>
        public static bool IsSupports(Type type)
        {
            return SupportTypes.Contains(type);
        }
        /// <summary>
        /// Gets Object Debug String.
        /// </summary>
        /// <param name="value">The object instance.</param>
        /// <returns>Returns string that contains all proeprty and its value.</returns>
        public static string DebugString(this object value)
        {
            string result = string.Empty;
            if (null == value) result = "instance is null.";
            else
            {
                Type type = value.GetType();
                var props = GetProperties(type);
                if (null != props && props.Count > 0)
                {
                    foreach (var prop in props)
                    {
                        if (null == prop) continue;
                        // skip all if match below type.
                        if (prop.PropertyType is IEnumerable) continue;
                        if (prop.PropertyType.IsArray) continue;
                        if (prop.PropertyType == typeof(System.Windows.Threading.Dispatcher)) continue;
                        if (!IsSupports(prop.PropertyType)) continue;

                        string propName = prop.Name;
                        try
                        {
                            object val = DynamicAccess.Get(value, propName);
                            if (null != val)
                            {
                                result += propName + ": " + val.ToString() + Environment.NewLine;
                            }
                            else
                            {
                                result += propName + ": " + "(null)" + Environment.NewLine;
                            }
                        }
                        catch (Exception)
                        {
                            result += propName + ": " + "(value error)" + Environment.NewLine;
                        }
                    }
                }
                else
                {
                    result = "object has no property.";
                }
            }
            return result.Trim();
        }

        #endregion
    }


    #endregion
}
