using System.Text.Json.Serialization;

namespace Models;

public class Transaction
{
    public string Type; // outbound or inbound
    // 
    public Guid Id;
    public double Amount; // 89700393456
    public string Recipient;
    
    
    public Transaction(string type, double amount, string account)
    {
        Id = Guid.NewGuid();
        Type = type;
        Amount = amount;
        Recipient = account;
    }
}

public class TransactionWithOtherBank
{    
    public Guid Id;
    public double Amount; // 89700393456
    public string Recipient;
    public string Bank;
    
    
    public TransactionWithOtherBank(double amount, string receipient_acc_id, string bank)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        Recipient = receipient_acc_id;
        Bank = bank;
    }
}