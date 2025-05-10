using System;
using UnityEngine;
public class CollectionValueAttribute : PropertyAttribute
{
    private Type m_EnumType;

    public Type EnumType
    {
        get
        {
            return m_EnumType;
        }
    }

    public CollectionValueAttribute( Type i_EnumType )
    {
        m_EnumType = i_EnumType;
    }
}
public class EnumAttribute : PropertyAttribute
{
    public System.Type m_Enum;
    public bool m_DisplayFilter;
    public string m_Remove;
    public EnumAttribute( System.Type i_Enum, bool i_DisplayFilter = false, string i_Remove = "" )
    {
        m_Enum = i_Enum;
        m_DisplayFilter = i_DisplayFilter;
        m_Remove = i_Remove;
    }
}