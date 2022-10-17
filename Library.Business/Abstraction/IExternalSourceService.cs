using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IExternalSourceService : IBaseService<ExternalSource>
    {
        Result Add(ExternalSource value);
        Result Update(ExternalSource value);
    }
}
