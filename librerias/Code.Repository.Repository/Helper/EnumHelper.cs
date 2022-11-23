using System;

namespace Code.Repository.RepositoryBL.Helper
{
    public static class EnumHelper
    {
        public static T GetEnumValue<T>( string str) where T : struct, IConvertible
        {
            var result = (T)System.Enum.Parse(typeof(T), str);

            return (T)result;
        }

        public static T GetEnumValue<T>( int intValue) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }

            return (T)Enum.ToObject(enumType, intValue);
        }
    }
}
