public class User
{
    private Guid _id;
    private string first;
    private string last;
    private Account account;
    private DateTime _d_o_b;
    private string _password;

    public User (string lastName, string firstName, Bank bank, DateTime d_o_b, string password)
    {
        first = firstName;
        last = lastName;
        _id = Guid.NewGuid();
        account = new Account(bank);
        _d_o_b = d_o_b;
        _password = password;
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

    public Transaction Withdraw(long amount)
    {
        return account.Subtract(amount);
    }

    public Transaction Transfer(long amount, Account acc)
    {
        return account.Transfer(amount, acc);
    }

    public TransactionWithOtherBank TransferToOtherBank(long amount, Account acc, OtherBank bank)
    {
        return account.TransferToOtherBank(amount, acc, bank);
    }

}

public class Account
{
    private long _id;
    private static int[] prefix_list = [207, 142, 657];
    private long _balance;
    private Transaction[] _transactions;
    private TransactionWithOtherBank[] _transactions_with_other_bank;


    public Account (Bank bank_name)
    {
        int acc_prefix = prefix_list[Random.Shared.Next(prefix_list.Length)];
        long acc_suffix = Random.Shared.Next(0, 10000000);
        _id = (acc_prefix * 10000000) + acc_suffix;
        _transactions = [];
        _transactions_with_other_bank = [];
    }

    public string id
    {
        get => $"{_id}";
    }

    public long balance
    {
        get => _balance;
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

    public Transaction Transfer(long amount, Account acc)
    {
        Transaction transaction = new Transaction("Outbound - Transfer", amount, acc);
        _transactions.Append(transaction);
        _balance -= amount;
        return transaction;
    }

    public TransactionWithOtherBank TransferToOtherBank(long amount, Account acc, OtherBank bank)
    {
        TransactionWithOtherBank transaction = new TransactionWithOtherBank(amount, acc, bank);
        _transactions_with_other_bank.Append(transaction);
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

    public Transaction(string type, long amount, Account account)
    {
        id = Guid.NewGuid();
        _type = type;
        _amount = amount;
        _recipient = account;

    }
}

public class TransactionWithOtherBank
{    
    private Guid id;
    private long _amount; // 89700393456
    private Account _recipient;
    private OtherBank _bank;
    public TransactionWithOtherBank(long amount, Account account, OtherBank bank)
    {
        id = Guid.NewGuid();
        _amount = amount;
        _recipient = account;
        _bank = bank;
    }
}

public class Bank
{
    private static string _name = "OurBank";

    public override string ToString()
    {
        return $"{_name}";
    }

}

public class OtherBank
{
    private string _name;

    public OtherBank(string name)
    {
        _name = name;
    }

    public override string ToString()
    {
        return $"{_name}";
    }

}