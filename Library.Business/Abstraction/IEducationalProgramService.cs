using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IEducationalProgramService : IBaseService<EducationalProgram>
    {
        DataResult<List<EducationalProgram>> GetAllBySpecialtyId(int specialtyId);
    }
}
