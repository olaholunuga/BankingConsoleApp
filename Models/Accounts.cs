using System.Text.Json.Serialization;

namespace Models;
public class Account
{
    public string Id;
    public static int[] PrefixList = [207, 142, 657];
    public double Balance;
    public List<string> Transactions;
    public List<string> TransactionsWithOtherBank;
    public Account ()
    {
        int acc_prefix = PrefixList[Random.Shared.Next(PrefixList.Length)];
        long acc_suffix = Random.Shared.Next(0, 10000000);
        Id = Convert.ToString((acc_prefix * 10000000L) + acc_suffix);
        Transactions = new List<string>();
        TransactionsWithOtherBank = new List<string>();
        Balance = 00.00;
    }

    public override string ToString()
    {
        return $"[Account] {this.Id} {this.Balance}";
    }

    public void Add(double amount)
    {
        Transaction transaction = new Transaction("Inbound - Saving", amount, this.Id);
        Transactions.Add(Convert.ToString(transaction.Id));
        Balance += amount;
    }

    public Transaction Subtract(double amount)
    {
       Transaction transaction = new Transaction("Outbound - Withdrawal", amount, this.Id);
        Transactions.Add(Convert.ToString(transaction.Id));
        Balance -= amount;
        return transaction;
    }

    public Transaction Transfer(double amount,  User recepient)
    {
        Transaction transaction = new Transaction("Outbound - Transfer", amount, recepient.Account);
        Transactions.Add(Convert.ToString(transaction.Id));
        Balance -= amount;
        return transaction;
    }

    public TransactionWithOtherBank TransferToOtherBank(double amount, User receipient, OtherBank bank)
    {
        TransactionWithOtherBank transaction = new TransactionWithOtherBank(amount, receipient.Account, bank.Name);
        Balance -= amount;
        Transactions.Add(Convert.ToString(transaction.Id));
        return transaction;
    }
    
}

public class OtherBankAccount() : Account()
{
}