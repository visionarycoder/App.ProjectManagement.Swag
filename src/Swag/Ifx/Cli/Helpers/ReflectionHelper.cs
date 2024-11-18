using System.Reflection;
using System.Text;

namespace Swag.Ifx.Cli.Helpers;

public static class ReflectionHelper
{

    public static string Display<T>(this T instance, int indentDepth = 0)
    {

        var indent1 = ConsoleHelper.GetIndent(indentDepth++);
        var indent2 = ConsoleHelper.GetIndent(indentDepth++);
        var propertyInfos = GetPropertyInfos(typeof(T));
        var propertyValues = DisplayImp(instance, indentDepth, propertyInfos);
        var results = $"{indent1}Type='{instance.GetName()}';{Environment.NewLine}{indent2}{propertyValues}";

        return results;

    }

    private static string DisplayImp<T>(T instance, int indentDepth, IEnumerable<PropertyInfo> propertyInfos)
    {

        var builder = new StringBuilder();
        foreach (var propertyInfo in propertyInfos)
        {
            var msg = propertyInfo.PropertyType.IsClass
                ? propertyInfo.GetValue(instance).Display(indentDepth: indentDepth++)
                : $"{propertyInfo.Name}='{propertyInfo.GetValue(instance)}'; ";
            builder.Append($"{propertyInfo.Name}='{propertyInfo.GetValue(instance)}'; ");
        }

        return builder.ToString();

    }

    private static string GetName<T>(this T _) => typeof(T).FullName ?? typeof(T).Name;

    private static IEnumerable<PropertyInfo> GetPropertyInfos(Type type)
    {
        return type.GetProperties();
    }

    private static PropertyInfo[] GetPropertyInfos(this Type type, string[] propertyNames)
    {
        return GetPropertyInfos(type).Where(i => propertyNames.Contains(i.Name)).ToArray();
    }

}