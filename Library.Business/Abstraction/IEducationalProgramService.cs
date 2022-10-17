using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Business.Abstraction
{
    public interface IEducationalProgramService : IBaseService<EducationalProgram>
    {
        Result Add(EducationalProgramDto value);
        Result Update(EducationalProgramDto value);
        DataResult<List<EducationalProgram>> GetAllBySpecialtyId(int specialtyId);
    }
}
