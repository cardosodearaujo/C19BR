using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Everaldo.Cardoso.C19BR.Framework.Conversion
{
    public static class FileToClass<T> where T : new()
    {
        public static List<T> Parse(string pathOfFile)
        {
            try
            {
                List<T> list = new List<T>();
                string[] file = File.ReadAllLines(pathOfFile);

                foreach (var line in file)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            var entity = new T();
                            var currentRow = line.Split('|');
                            int cont = 0;

                            var listValidAttributes = entity.GetType().GetProperties()
                                .Where(F => F.PropertyType.FullName != "Everaldo.Cardoso.C19BR.Object.Entity.Administradora"
                                && F.GetCustomAttribute(typeof(KeyAttribute)) == null
                                && F.Name != "Id_Vendedor"
                                && F.Name != "Id_Usuario_Inc"
                                && F.Name != "Id_Usuario_Alt"
                                && F.Name != "Marca"
                                && F.Name != "Bloqueado"
                                && F.Name != "Dt_Inc"
                                && F.Name != "Dt_Alt"
                                && F.Name != "Dt_Hab").ToList();

                            foreach (var currentField in currentRow)
                            {
                                try
                                {
                                    var obj = new T();
                                    var propertyInfo = obj.GetType().GetProperty(listValidAttributes[cont].Name, BindingFlags.Public | BindingFlags.Instance);
                                    if (propertyInfo != null)
                                    {
                                        if (propertyInfo.GetCustomAttribute(typeof(ForeignKeyAttribute)) != null) //Chaves estrangeira;
                                        {
                                            var objInternal = propertyInfo.GetValue(obj);
                                            var property = objInternal.GetType().GetProperty("Codigo", BindingFlags.Public | BindingFlags.Instance);                                           
                                            SetValueSubEntity(property, currentField, ref objInternal, property.PropertyType.Name);

                                            propertyInfo.SetValue(entity, objInternal);
                                        }
                                        else if (propertyInfo.PropertyType.IsGenericType) //Tipos nullaveis;
                                        {
                                            var typeNullable = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                                            var value = currentField;
                                            if (typeNullable.Name == "DateTime") value = Tools.TextoParaData(value.ToString()).ToString();
                                            var safeValue = (value == null) ? null : Convert.ChangeType(value, typeNullable);
                                            SetValueEntity(propertyInfo, safeValue, ref entity, propertyInfo.PropertyType.GetGenericArguments()[0].Name);
                                        }
                                        else //Tipos comuns
                                        {
                                            var value = currentField;
                                            if (propertyInfo.PropertyType.Name == "DateTime") value = Tools.TextoParaData(value.ToString()).ToString();
                                            SetValueEntity(propertyInfo, value, ref entity, propertyInfo.PropertyType.Name);
                                        }
                                    }
                                    cont++;
                                }
                                catch (Exception ex)
                                {
                                    //ex.RegisterLog();
                                    cont++;
                                }
                            }
                            list.Add(entity);
                        }
                    }
                    catch (Exception ex)
                    {
                        //ex.RegisterLog();
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                //ex.RegisterLog();
                return null;
            }
        }

        private static void SetValueEntity(PropertyInfo propertyInfo, object currentField, ref T entity, string propertyType)
        {
            switch (propertyType)
            {
                case "Int16":
                    if (VerifyType.IsNumeric(currentField))
                    {
                        currentField = VerifyType.ConvertProperty(propertyInfo.Name, currentField, entity.GetType());
                        propertyInfo.SetValue(entity, currentField);
                    }                        
                    break;
                case "Int32":
                    if (VerifyType.IsNumeric(currentField))
                    {
                        currentField = VerifyType.ConvertProperty(propertyInfo.Name, currentField, entity.GetType());
                        propertyInfo.SetValue(entity, currentField);
                    }                        
                    break;
                case "Int64":
                    if (VerifyType.IsNumeric(currentField))
                    {
                        currentField = VerifyType.ConvertProperty(propertyInfo.Name, currentField, entity.GetType());
                        propertyInfo.SetValue(entity, currentField);
                    }                        
                    break;
                case "Decimal":
                    if (VerifyType.IsNumeric(currentField))
                    {
                        currentField = VerifyType.ConvertProperty(propertyInfo.Name, currentField, entity.GetType());
                        propertyInfo.SetValue(entity, decimal.Parse(currentField.ToString()) / 100);
                    }                        
                    break;
                case "DateTime":
                    if (VerifyType.IsDateTime(currentField.ToString())) 
                    {
                        var date = VerifyType.ConvertProperty(propertyInfo.Name, currentField, entity.GetType());
                        if (DateTime.Parse(date.ToString()) == DateTime.MinValue)
                            propertyInfo.SetValue(entity, null);                        
                        else                        
                            propertyInfo.SetValue(entity, date);                        
                    }
                    break;
                default:
                    propertyInfo.SetValue(entity, currentField.ToString());
                    break;
            }
        }

        private static void SetValueSubEntity(PropertyInfo propertyInfo, object currentField, ref object entity, string propertyType)
        {
            switch (propertyType)
            {
                case "Int16":
                    if (VerifyType.IsNumeric(currentField))
                    {
                        currentField = VerifyType.ConvertProperty(propertyInfo.Name, currentField, entity.GetType());
                        propertyInfo.SetValue(entity, currentField);
                    }                        
                    break;
                case "Int32":
                    if (VerifyType.IsNumeric(currentField))
                    {
                        currentField = VerifyType.ConvertProperty(propertyInfo.Name, currentField, entity.GetType());
                        propertyInfo.SetValue(entity, currentField);
                    }                        
                    break;
                case "Int64":
                    if (VerifyType.IsNumeric(currentField))
                    {
                        currentField = VerifyType.ConvertProperty(propertyInfo.Name, currentField, entity.GetType());
                        propertyInfo.SetValue(entity, currentField);
                    }                        
                    break;
                case "Decimal":
                    if (VerifyType.IsNumeric(currentField))
                    {
                        currentField = VerifyType.ConvertProperty(propertyInfo.Name, currentField, entity.GetType());
                        propertyInfo.SetValue(entity, decimal.Parse(currentField.ToString()) / 100);
                    }                        
                    break;
                case "DateTime":
                    if (VerifyType.IsDateTime(currentField))
                    {
                        var date = VerifyType.ConvertProperty(propertyInfo.Name, currentField, entity.GetType());
                        if (DateTime.Parse(date.ToString()) == DateTime.MinValue)                        
                            propertyInfo.SetValue(entity, null);                        
                        else                        
                            propertyInfo.SetValue(entity, date);                        
                    }
                    break;
                default:
                    propertyInfo.SetValue(entity, currentField.ToString());
                    break;
            }
        }
    }
}
