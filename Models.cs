using System.Text.Json.Serialization;
namespace Models;

public class User
{
	[JsonInclude]
    private Guid _id;
    [JsonInclude]
    private string first;
    [JsonInclude]
    private string last;
    [JsonInclude]
    private Account _account;
    [JsonInclude]
    private DateTime _d_o_b;
    [JsonInclude]
    private string _password;

    public Account account
    {
        get => _account;
    }
    
    public User ()
    {
    }

    public User (string lastName, string firstName, DateTime d_o_b, string password, OtherBank bank = null)
    {
        first = firstName;
        last = lastName;
        _id = Guid.NewGuid();
        if (bank == null)
        {
            _account = new Account();
        }
        else
        {
            _account = new OtherBankAccount();
            bank.add_user(this);
        }
        _password = password;
        _d_o_b = d_o_b;
    }

    public bool check_password(string pass)
    {
        return pass == this._password;
    }

    public override string ToString()
    {

        return $"[User] {this.id} - {this.name}\nAcc: {this._account.id}";
    }

    public string id
    {
        get => _id.ToString("D");
    }

    public string name
    {
        get => $"{first} {last}";
    }

    public double _balance
    {
        get => _account.balance;
    }

    // user methods

    public void Save(double amount)
    {
        account.Add(amount); // to return transaction
    }

    public Transaction Withdraw(double amount)
    {
        return account.Subtract(amount);
    }

    public Transaction Transfer(double amount, User recepient)
    {
        return account.Transfer(amount, recepient);
    }

    public TransactionWithOtherBank TransferToOtherBank(double amount, User receipient, OtherBank bank)
    {
        return account.TransferToOtherBank(amount, receipient, bank);
    }

}

public class Account
{
	[JsonInclude]
    private long _id;
    [JsonInclude]
    private static int[] prefix_list = [207, 142, 657];
    [JsonInclude]
    private double _balance;
    [JsonInclude]
    private Transaction[] _transactions;
    [JsonInclude]
    private TransactionWithOtherBank[] _transactions_with_other_bank;
    

    public Account ()
    {
        int acc_prefix = prefix_list[Random.Shared.Next(prefix_list.Length)];
        long acc_suffix = Random.Shared.Next(0, 10000000);
        _id = (acc_prefix * 10000000L) + acc_suffix;
        _transactions = [];
        _transactions_with_other_bank = [];
        _balance = 00.00;
    }

    public string id
    {
        get => $"{_id}";
    }

    public double balance
    {
        get => _balance;
    }

    public override string ToString()
    {
        return $"[Account] {this.id} {this.balance}";
    }

    public void Add(double amount)
    {
        Transaction transaction = new Transaction("Inbound - Saving", amount, this);
        _transactions.Append(transaction);
        _balance += amount;
    }

    public Transaction Subtract(double amount)
    {
       Transaction transaction = new Transaction("Outbound - Withdrawal", amount, this);
        _transactions.Append(transaction);
        _balance -= amount;
        return transaction;
    }

    public Transaction Transfer(double amount,  User recepient)
    {
        Transaction transaction = new Transaction("Outbound - Transfer", amount, recepient.account);
        _transactions.Append(transaction);
        _balance -= amount;
        return transaction;
    }

    public TransactionWithOtherBank TransferToOtherBank(double amount, User receipient, OtherBank bank)
    {
        TransactionWithOtherBank transaction = new TransactionWithOtherBank(amount, receipient, bank);
        _balance -= amount;
        _transactions_with_other_bank.Append(transaction);
        return transaction;
    }
}

public class OtherBankAccount : Account
{
	[JsonInclude]
    private long _id;
    [JsonInclude]
    private static int[] prefix_list = [207, 142, 657];
    [JsonInclude]
    private double _balance;
    [JsonInclude]
    private Transaction[] _transactions;
    
    
    public OtherBankAccount ()
    {
        int acc_prefix = prefix_list[Random.Shared.Next(prefix_list.Length)];
        long acc_suffix = Random.Shared.Next(0, 10000000);
        _id = (acc_prefix * 10000000L) + acc_suffix;
        _transactions = [];
        _balance = 30.00;
    }
}

public class Transaction
{
	[JsonInclude]
    private string _type; // outbound or inbound
    [JsonInclude]
    private Guid id;
    [JsonInclude]
    private double _amount; // 89700393456
    [JsonInclude]
    private Account _recipient;
    
    
    public Transaction(string type, double amount, Account account)
    {
        id = Guid.NewGuid();
        _type = type;
        _amount = amount;
        _recipient = account;

    }
}

public class TransactionWithOtherBank
{    
	[JsonInclude]
    private Guid id;
    [JsonInclude]
    private double _amount; // 89700393456
    [JsonInclude]
    private User _recipient;
    [JsonInclude]
    private OtherBank _bank;
    
    
    public TransactionWithOtherBank(double amount, User receipient, OtherBank bank)
    {
        id = Guid.NewGuid();
        _amount = amount;
        _recipient = receipient;
        _bank = bank;
    }
}

public class Bank
{
    private string _name = "OurBank";
    
    public Bank()
    {
    }

    public override string ToString()
    {
        return $"{_name}";
    }

    public void transfer_to_ourbank(User current_user, User recepient, double amount, out string response)
    {
        if (amount > current_user.account.balance)
        {
           response = "Failed: Insurficient balance";
           return;
        }
        current_user.Transfer(amount, recepient);
        recepient.Save(amount);
        response = "SUCCESS";
    }

    public void Transfer_to_other_banks(User current_user, User recepient, OtherBank bank, double amount, out string response)
    {
        if (amount > current_user.account.balance)
        {
           response = "Failed: Insurficient balance"; 
           return;
        }
        current_user.TransferToOtherBank(amount, recepient, bank);
        bank.receive_from_other_banks(current_user, recepient, amount, out string res);
        response = res;
    }
}

public class OtherBank
{
    [JsonInclude]
    private string _name;
    [JsonInclude]
    private List<User> _bank_users;
    
    public OtherBank()
    {
        _bank_users = new List<User>();
    }

    public OtherBank(string name)
    {
        _name = name;
        _bank_users = new List<User>();
    }

    public override string ToString()
    {
        return $"{_name}";
    }

    public void receive_from_other_banks(User sender, User recepient, double amount, out string response)
    {
        recepient.Save(amount);
        response = "SUCCESS";
    }

    public void add_user(User user)
    {
        this._bank_users.Add(user);
    }

    public User get_user_by_acc_no(string acc_no)
    {
        // Console.WriteLine($"{this._bank_users.ToString()}");
        foreach(var user in this._bank_users)
        {
            if (user.account.id == acc_no)
            {
                return user;
            }
        }
        return null;
    }

    public List<User> users
    {
        get => _bank_users;
    }

}