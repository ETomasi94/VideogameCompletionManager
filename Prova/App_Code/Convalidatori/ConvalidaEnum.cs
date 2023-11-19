using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

public class ConvalidaEnum
{
    static ConvalidaEnum()
    {
    }

    public static Boolean Valida(string valueName,Enum inputEnumValue)
    {
        Boolean valoreTrovato = false;
  
        if (valueName != null)
        {
            foreach(String name in Enum.GetNames(inputEnumValue.GetType()))
            {
                if(valueName.Equals(name,StringComparison.OrdinalIgnoreCase)) {  valoreTrovato = true; break;}
            }
        }  
        
        return valoreTrovato; 
    }

    public static Boolean ValidaNome(string valueName,Enum inputEnumValue)
    {
        Boolean nomeTrovato = false;

        if (valueName != null)
        {
            foreach (String name in Enum.GetNames(inputEnumValue.GetType()))
            {
                if(NormalizzaStringa(valueName).Equals(NormalizzaStringa(RicavaNome(inputEnumValue)),StringComparison.OrdinalIgnoreCase)) { nomeTrovato = true; break; }
            }
        }

        return nomeTrovato;
    }

    private static string RicavaNome(Enum value) 
    {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
    }

    private static string NormalizzaStringa(string value)
    {
        return value.Replace(" ", "").Replace("_", "").Replace("-", "").Trim();
    }
}