using Library.Core.Domain.Enums;
using Library.DataAccess.Abstraction;
using Library.DataAccess.Factories;
using Library.Entities.Concrete;

string connectionString = "postgres://taleh:Taleh642477@95.86.133.98:5432/DIALibrary";
//string connectionString2 = "Server=95.86.133.98;Port=5432;Database=DIALibrary;User Id=ehmed;Password=Ehmed642477;";
string connectionString2 = "Server=192.168.1.189;Port=5432;Database=DIALibrary;User Id=ehmed;Password=Ehmed642477;";

IUnitOfWork unitOfWork = DbFactory.Create(ServerType.Postgre, connectionString2);

bool result = unitOfWork.SpecialtyRepository.Add(new Speciality
{
    Name = "Dövlət və bələdiyyə idarəetməsi",
    Faculty = new Faculty
    {
        Id = 1,
        Name = "İnzibati idarəetmə"
    }
});
//bool result = unitOfWork.SpecialtyRepository.Delete(7);
if (result) Console.WriteLine("Process completed successfully");
 
Console.OutputEncoding = System.Text.Encoding.UTF8;
//var item = unitOfWork.SpecialtyRepository.Get(3);
//Console.WriteLine("Speciality Id: " + item.Id);
//Console.WriteLine("Name of Speciality: " + item.Name);
//Console.WriteLine("Name of Faculty: " + item.Faculty.Name);
//Console.WriteLine();


foreach (var item in unitOfWork.SpecialtyRepository.GetAll())
{
    Console.WriteLine("Speciality Id: " + item.Id);
    Console.WriteLine("Name of Speciality: " + item.Name);
    Console.WriteLine("Name of Faculty: " + item.Faculty.Name);
    Console.WriteLine();
}
Console.ReadLine();



