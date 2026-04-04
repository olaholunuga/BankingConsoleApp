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

    public long _balance
    {
        get => account.balance;
    }

    public string acc_no
    {
        get => account.id;
    }

    // user methods

    public void Save(long amount)
    {
        account.Add(amount); // to return transaction
    }

    public Transaction Withdrawal(long amount)
    {
        return account.Subtract(amount);
    }

    public Transaction Transfer(long amount, Bank otherbank, Account acc_no)
    {
        return account.Transfer(amount, acc_no);
    }

}

public class Account
{
    private long _id;
    private Bank _bank;
    private static int[] prefix_list = [207, 142, 657];
    private long _balance;
    private Transaction[] _transactions;
    public Account (Bank bank_name)
    {
        int acc_prefix =prefix_list[Random.Shared.Next(prefix_list.Length)];
        long acc_suffix = Random.Shared.Next(0, 10000000);

        _id = (acc_prefix * 10000000) + acc_suffix;
        _bank = bank_name;
        _transactions = [];
    }

    public string id
    {
        get => $"{_id}";
    }

    public long balance
    {
        get => _balance;
    }

    public string bank
    {
        get => _bank.ToString();
    }

    public void Add(long amount)
    {
        Transaction transaction = new Transaction("Inbound - Saving", amount, this);
        _transactions.Append(transaction);
        _balance += amount;
    }

    public Transaction Subtract(long amount)
    {
       Transaction transaction = new Transaction("Outbound - Withdrawal", amount, this);
        _transactions.Append(transaction);
        _balance -= amount;
        return transaction;
    }

    public Transaction Transfer(long amount, Account acc_no)
    {
        Transaction transaction = new Transaction("Outbound - Transfer", amount, acc_no);
        _transactions.Append(transaction);
        _balance -= amount;
        return transaction;
    }
}

public class Transaction
{
    private string _type; // outbound or inbound
    private Guid id;
    private long _amount; // 89700393456

    private Account _recipient;
    private string bank_name;

    public Transaction(string type, long amount, Account account)
    {
        id = Guid.NewGuid();
        _type = type;
        _amount = amount;
        _recipient = account;
        bank_name = account.bank;


    }
}

public class Bank
{
    private string _name;

    public Bank(string name)
    {
        _name = name;
    }

    public override string ToString()
    {
        return $"{_name}";
    }

}