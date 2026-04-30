using System.Text.Json.Serialization;
namespace Models;

public class Bank
{
    public string Name = "OurBank";
    
    public Bank()
    {
    }

    public override string ToString()
    {
        return $"{Name}";
    }
}

public class OtherBank
{
    public string Name;
    private List<User> BankUsers;
    
    public OtherBank()
    {
        BankUsers = new List<User>();
    }

    public OtherBank(string name)
    {
        Name = name;
        BankUsers = new List<User>();
    }

    public override string ToString()
    {
        return $"{Name} users: {users.Count}";
    }
    public List<User> users
    {
        get => BankUsers;
    }

    public void add_user(User user)
    {
        this.BankUsers.Add(user);
    }

    public User get_user_by_acc_no(string acc_no)
    {
        // Console.WriteLine($"{this._bank_users.ToString()}");
        foreach(var user in this.BankUsers)
        {
            if (user.Account == acc_no)
            {
                return user;
            }
        }
        return null;
    }
}