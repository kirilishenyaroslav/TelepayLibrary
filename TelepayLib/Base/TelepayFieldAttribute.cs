using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TelepayLib
{
    public enum FieldType
    {
        Numeric,
        Alpha,
        Alphanumeric,
        Object
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class TelepayFieldAttribute : Attribute
    {
        public TelepayFieldAttribute(int Position, int Length, FieldType Type) : this(Length, Type)
        {
            this.Position = Position;
        }

        public TelepayFieldAttribute(int Length, FieldType Type) : this(Type)
        {
            this.Length = Length;
        }

        public TelepayFieldAttribute(FieldType Type)
        {
            this.Type = Type;
        }

        public int? Length;
        public FieldType Type;
        public int? Position;
    }
}