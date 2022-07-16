namespace Library.Core.DataAccess.Implementation.PostgreSql
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
