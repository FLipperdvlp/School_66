using School_66.Entities;

namespace School_66.Interface;

public interface IParentFormService
{
    Task<Parent> CreateFormForParent(Parent parent);
}