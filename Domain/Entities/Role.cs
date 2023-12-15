namespace Domain.Entities;
public class Role : BaseEntity
{
    public string Description {get;set;}
    public ICollection<User> Users {get;set;} = new HashSet<User>();
    public ICollection<UserRole> UsersRoles {get;set;}
}