using JuniorStart.DTO;
using JuniorStart.Entities;
using JuniorStart.ViewModels;

namespace JuniorStart.Factories
{
    public interface IModelFactory<TC, TM> where TC : class where TM : class
    {
        TC Create(TM model);
        TM Map(TC model);
    }
}