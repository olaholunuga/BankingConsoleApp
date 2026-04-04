using System.Data.Common;

public class User
{
    private Guid _id;
    private string first;
    private string last;
    private Account account;

    public User (string lastName, string firstName, Bank bank_name)
    {
        first = firstName;
        last = lastName;
        _id = Guid.NewGuid();
        account = new Account(bank_name);
    }

    public string id
    {
        get => _id.ToString("D");
    }

    public string name
    {
        get => $"{first} {last}";
    }

    public string acc_no
    {
        get => account.id;
    }

}

public class Account
{
    private long _id;
    private Bank bank;
    private static int[] prefix_list = [207, 142, 657];

    public Account (Bank bank_name)
    {
        int acc_prefix =prefix_list[Random.Shared.Next(prefix_list.Length)];
        long acc_suffix = Random.Shared.Next(0, 10000000);

        _id = (acc_prefix * 10000000) + acc_suffix;
        bank = bank_name;
    }

    public string id
    {
        get => $"{_id}";
    }
}

public class Transaction
{
    private string _type; // outbound or inbound
    private Guid id;
    private long _amount; // 89700393456

    public Transaction(string type, long amount)
    {
        id = Guid.NewGuid();
        _type = type;
        _amount = amount;
    }
}

public class Bank
{
    private string _name;

    public Bank(string name)
    {
        _name = name;
    }

}