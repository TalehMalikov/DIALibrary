using Library.Core.Domain.Enums;
using Library.DataAccess.Abstraction;
using Library.DataAccess.Factories;
using Library.Entities.Concrete;

string connectionString = "postgres://taleh:Taleh642477@95.86.133.98:5432/DIALibrary";
string connectionString2 = "Server=95.86.133.98;Port=5432;Database=DIALibrary;User Id=taleh;Password=Taleh642477;";

IUnitOfWork unitOfWork = DbFactory.Create(ServerType.Postgre, connectionString2);

Console.OutputEncoding = System.Text.Encoding.UTF8;

Account account = new Account()
{
    Id = 5,
    User = new User()
    {
        Id = 2
    },
    AccountName = "Ahmadov",
    PasswordHash = "1111",
    Email = "eehmedov17@gmail.com",
    IsDeleted = false,
    LastModified = DateTime.Now
};
unitOfWork.AccountRepository.Update(account);

foreach (var item in unitOfWork.AccountRepository.GetAll())
{
    Console.WriteLine(item.Id);
    Console.WriteLine(item.AccountName);
    Console.WriteLine(item.PasswordHash);
    Console.WriteLine(item.Email);
    Console.WriteLine(item.User.FirstName);
    Console.WriteLine(item.User.LastName);
    Console.WriteLine(item.User.FatherName);
    Console.WriteLine(item.User.BirthDate);
    Console.WriteLine(item.User.Gender);
}