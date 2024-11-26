using CRUD_Operations.Controllers;

namespace CRUD_Operations.Authorization
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public class CheckPermassionAttribute : Attribute
    {
        public CheckPermassionAttribute(Permission permission)
        {
            Permission = permission;
        }

        public Permission Permission { get; }
    }
}
