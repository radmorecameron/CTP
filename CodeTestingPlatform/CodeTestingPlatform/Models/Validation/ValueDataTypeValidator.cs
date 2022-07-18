using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeTestingPlatform.Models.Validation {
    public class ValueDataTypeValidator {
        //Checks if value matches its datatype and returns true/false
        public static bool CheckParamDataType(string value, string dataType) {
            switch (dataType.ToLower()) {
                //case "string":
                //    if (!Regex.IsMatch(value, "/^[a-z ]+/i"))
                //        return false;
                //    break;
                // Not sure to check for string because value is already of type string
                case "int":
                    if (value != null) {
                        if (!int.TryParse(value, out int _))
                            return false;
                    }
                    break;
                case "bool":
                case "boolean":
                    if (!bool.TryParse(value, out _))
                        return false;
                    break;
                case "char":
                    if (!char.TryParse(value, out _))
                        return false;
                    break;
                case "number":
                case "double":
                    if (!double.TryParse(value, out _))
                        return false;
                    break;
                case "long":
                    if (!long.TryParse(value, out _))
                        return false;
                    break;
                case "byte":
                case "bytes":
                    if (!byte.TryParse(value, out _))
                        return false;
                    break;
                case "short":
                    if (!short.TryParse(value, out _))
                        return false;
                    break;
                case "float":
                    if (!float.TryParse(value, out _))
                        return false;
                    break;
                case "datetime":
                    if (!DateTime.TryParse(value, out _))
                        return false;
                    break;
                case "decimal":
                    if (!decimal.TryParse(value, out _))
                        return false;
                    break;
                case "int[]":
                    if (!(value.StartsWith('[') && value.EndsWith(']'))) // temp solution for array, just checks if value contains '[' and ']'
                        return false;
                    break;
                case "string[]":
                    if (!(value.StartsWith('[') && value.EndsWith(']')))
                        return false;
                    break;
                case "list":
                    if (value != null) {
                        if (!Regex.IsMatch(value, @"^\[(((\d|-\d)|(('|"")[a-zA-z\d\-\d]*('|"")))*((?:, (\d|-\d)+)|((?:, ('|"")[a-zA-z\d\-\d]*('|""))))*)]$"))
                            return false;
                    }
                    break;
                case "tuple":
                    if (value != null) {
                        if (!Regex.IsMatch(value, @"^\((((\d)|(('|"")[a-zA-z  \d]+('|"")))+((?:, \d+)|((?:, ('|"")[a-zA-z  \d]+('|""))))*)\)$"))
                            return false;
                    }

                    break;
                case "set":
                    if (value != null) {
                        if (!Regex.IsMatch(value, @"^\{(((\d)|(('|"")[a-zA-z  \d]+('|"")))+((?:, \d+)|((?:, ('|"")[a-zA-z  \d]+('|""))))*)\}$"))
                            return false;
                    }

                    break;
                case "dict":
                    if (value != null) {
                        if (!Regex.IsMatch(value, @"^\{(((\d)|(('|"")[a-zA-z \d]+('|"")))(:| : | :|: )((\d)|(('|"")[a-zA-z \d]+('|"")))+((?:, ((\d)|(('|"")[a-zA-z \d]+('|"")))(:| : | :|: )((\d)|(('|"")[a-zA-z \d]+('|"")))))*)\}$"))
                            return false;
                    }

                    break;

                //case "complex":
                //    if (!complex.TryParse(value, out _))
                //        return false;
                //    break; 
                // Not sure how to check for complex data type

                default:
                    break;
            }
            return true;
        }
    }
}
