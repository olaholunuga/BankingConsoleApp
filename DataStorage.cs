using Models;
using System.Text.Json;
namespace DataStorage;

class DataStore
{
    static public OtherBank[] Load_banks()
    {
        string banks_json = File.ReadAllText("banks.json");
        OtherBank[] banks = JsonSerializer.Deserialize<OtherBank[]>(banks_json) ?? [];
        return banks;
    }

    static public List<User> Load_users()
    {
        string our_users = File.ReadAllText("our_bank_users.json");
        List<User> users = JsonSerializer.Deserialize<List<User>>(our_users) ?? [];
        return users;
    }

    static public List<User> other_users(OtherBank[] banks)
    {
        string other_users = File.ReadAllText("other_banks_users.json");
        List<User> users = JsonSerializer.Deserialize<List<User>>(other_users) ?? [];
        // banks[0].add_user(users[0]);
        // banks[0].add_user(users[1]);
        // banks[1].add_user(users[2]);
        // banks[1].add_user(users[3]);
        // banks[2].add_user(users[4]);
        // banks[2].add_user(users[5]);
        // banks[3].add_user(users[6]);
        // banks[3].add_user(users[7]);
        int i = 0;
        int j = 0;
        foreach (User user in users)
        {
            banks[j].add_user(users[i]);
            i++;
            if (i >= 2 && i % 2 == 0)
            {
                j++;
            }
        }
        return users;
    }

    static public List<Account> user_accounts()
    {
        string user_accounts = File.ReadAllText("accounts.json");
        List<Account> accounts = JsonSerializer.Deserialize<List<Account>>(user_accounts) ?? [];
        return accounts;
    }
    static public List<OtherBankAccount> other_bank_accounts()
    {
        string user_accounts = File.ReadAllText("other_bank_accounts.json");
        List<OtherBankAccount> accounts = JsonSerializer.Deserialize<List<OtherBankAccount>>(user_accounts) ?? [];
        return accounts;
    }

    static public void save_accounts(List<Account> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText("accounts.json", json);
    }

    static public void save_other_accounts(List<OtherBankAccount> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText("other_bank_accounts.json", json);
    }

    static public void save_users(List<User> users)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string users_json = JsonSerializer.Serialize(users, options);
        File.WriteAllText("our_bank_users.json", users_json);
    }

    static public void save_other_users(List<User> users)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string users_json = JsonSerializer.Serialize(users, options);
        File.WriteAllText("other_banks_users.json", users_json);
    }
}