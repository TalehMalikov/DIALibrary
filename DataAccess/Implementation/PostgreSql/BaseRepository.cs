namespace Library.DataAccess.Implementation.PostgreSql
{
    public abstract class BaseRepository
    {
        protected readonly string connectionString;
        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
