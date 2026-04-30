using System.Text.Json.Serialization;

namespace Models;

public class User
{
    public Guid Id { get; init; }
    public string First;
    public string Last;
    public string Account { get; }
    public DateTime Dob;
    public string Password;

    public User () {}

    public User (string lastName, string firstName, DateTime d_o_b, string password, string account_id)
    {
        First = firstName;
        Last = lastName;
        Id = Guid.NewGuid();
        // Account account;
        // if (bank == null)
        // {
        //     account = new Account();
        //     Account = Convert.ToString(account.Id);
        // }
        // else
        // {
        //     account = new OtherBankAccount();
        //     Account = Convert.ToString(account.Id);
        //     bank.add_user(this);
        // }
        Account = account_id;
        Password = password;
        Dob = d_o_b;
    }

    // i'm to move this to utilities
    public bool check_password(string pass)
    {
        return pass == this.Password;
    }

    public override string ToString()
    {

        return $"[User] {this.Id} - {this.Name}\nAcc: {this.Account}";
    }

    public string Name
    {
        get => $"{First} {Last}";
    }

    // public Transaction Withdraw(double amount)
    // {
    //     return account.Subtract(amount);
    // }

    // public Transaction Transfer(double amount, User recepient)
    // {
    //     return account.Transfer(amount, recepient);
    // }

    // public TransactionWithOtherBank TransferToOtherBank(double amount, User receipient, OtherBank bank)
    // {
    //     return account.TransferToOtherBank(amount, receipient, bank);
    // }
    
}