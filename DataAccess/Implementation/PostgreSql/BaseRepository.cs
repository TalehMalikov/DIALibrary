namespace Library.DataAccess.Implementation.PostgreSql
{
    public class BaseRepository
    {
        protected readonly string connectionString;
        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
