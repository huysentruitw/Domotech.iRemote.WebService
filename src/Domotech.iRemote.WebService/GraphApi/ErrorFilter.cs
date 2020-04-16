using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using HotChocolate;

namespace Domotech.iRemote.WebService.GraphApi
{
    internal sealed class ErrorFilter : IErrorFilter
    {
        private static readonly Regex SnakeCasingRegex = new Regex("([A-Z])", RegexOptions.Compiled);

        public IError OnError(IError error)
        {
            if (error.Exception == null)
                return error;

            string code = GetCodeForException(error.Exception);
            IReadOnlyDictionary<string, object> data = GetExceptionData(error.Exception);

            return error
                .WithCode(code)
                .AddExtension("data", data);
        }

        private static string GetCodeForException(Exception exception)
        {
            var exceptionName = exception.GetType().Name;

            if (exceptionName.EndsWith("Exception"))
                exceptionName = exceptionName[..^9];

            return ToSnakeCasing(exceptionName);
        }

        private static IReadOnlyDictionary<string, object> GetExceptionData(Exception exception)
        {
            var data = new Dictionary<string, object>();

            Type exceptionType = exception.GetType();
            foreach (PropertyInfo propertyInfo in exceptionType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                data.Add(ToCamelCasing(propertyInfo.Name), propertyInfo.GetValue(exception));

            return data;
        }

        private static string ToSnakeCasing(string input)
            => SnakeCasingRegex.Replace(input, "_$0").TrimStart('_').ToUpper();

        private static string ToCamelCasing(string input)
            => char.ToLower(input[0]) + input.Substring(1);
    }
}
