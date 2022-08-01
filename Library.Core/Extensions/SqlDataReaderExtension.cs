using Npgsql;

namespace Library.Core.Extensions
{
    public static class NpgsqlDataReaderExtension
    {

        public static T Get<T>(this NpgsqlDataReader reader, string columnName)
        {
            object val = null;

            val = reader[columnName];

            T result = default;

            if (val != DBNull.Value && val != null) 
                result = (T) val;

            return result;
        }

    }
}