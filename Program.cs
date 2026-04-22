using Models;
using DataStorage;
using System.Net;
bool keep_going = true;

Bank ourbank = new Bank();

OtherBank[] banks = DataStore.Load_banks();

List<User> users = DataStore.Load_users();

List<User> other_users = DataStore.other_users(banks);


while (keep_going)
{
    Console.WriteLine("""
    ================ OurBank =====================
                     welcome
    ==============================================

    What would you like to do?
    1. Create new Account             2. Transfer
    3. Check balance                  4. Withdraw
    5. Save                           6. List Accounts
    """);

    int account = Convert.ToInt32(Console.ReadLine());

    switch (account)
    {
        case 1:
            create_new_account();
            DataStore.save_users(users);
            break;
        case 2:
            transfer();
            DataStore.save_users(users);
            DataStore.save_other_users(other_users);
            break;
        case 3:
            check_balance();
            break;
        case 4:
            withdraw();
            DataStore.save_users(users);
            break;
        case 5:
            save();
            DataStore.save_users(users);
            break;
        case 6:
            list_accounts();
            break;
        
        default:
        break;
    }

    Console.WriteLine("Would you like to do something else? \"Yes\" or \"No\"");
    string _continue = Console.ReadLine() ?? "";
    if (_continue == "no" || _continue == "NO" || _continue == "No" || _continue == "n")
    {
        Console.WriteLine("Thank you for banking with us");
        keep_going = false;
    }
}
DataStore.save_other_users(other_users);
DataStore.save_users(users);

void list_accounts()
{
    User current_user = login();

    if (current_user == null)
    {
        return;
    }
    Console.WriteLine("""
    ----------- OurBank Accounts ------------

    """);
    foreach (var user in users)
    {
        Console.WriteLine($"""
        {user.name}
        {user.account.id}
        ----------------------------------
        """);
    }

    Console.WriteLine("""
    ----------- Other Bank's Accounts ------------
    """);
    foreach (var bank in banks)
    {
        Console.WriteLine($"""
        ---------- {bank} Accounts ---------

        """);
        foreach (var user in bank.users)
        {
            Console.WriteLine($"""
            {user.name}
            {user.account.id}
            -----------------------------------

            """);
        }
    }
}

void save()
{
    User current_user = login();

    if (current_user == null)
    {
        return;
    }

    Console.Write("Enter the amount of money you want to save");
    double amount = Convert.ToDouble(Console.ReadLine());
    current_user.Save(amount);
    Console.WriteLine($"""
    ${amount} saved successfully
    """);

}

void withdraw()
{
    User current_user = login();

    if (current_user == null)
    {
        return;
    }
    Console.Write("Enter the amount of money you want to withdraw");
    double amount = Convert.ToDouble(Console.ReadLine());
    current_user.Withdraw(amount);
    Console.WriteLine($"""
    Take your cash >
    ${amount} withdrawn successfully!!
    """);
}

User login()
{
    Console.WriteLine("Enter your account number: ");
    var acc_number = Console.ReadLine() ?? ""; 
    User? user = get_user(acc_number);
    if (user == null)
    {
        Console.WriteLine("Account number does not exist");
        return user;
    }

    string pass;
    int retry_num = 0;
    do
    {
        Console.WriteLine("Enter your password: ");
        pass = Console.ReadLine() ?? "";

        if (user.check_password(pass) == false)
        {
            Console.WriteLine("wrong password!!");
        }
        retry_num += 1;
    } while (user.check_password(pass) == false || retry_num >= 3);
    if (retry_num >= 3)
    {
        Console.WriteLine("You've exceeded the number of retrys");
    }
    return user;
}

User get_user(String acc_no)
{
    foreach (var user in users)
    {
        Account acc = user.account;
        if (acc.id == acc_no)
        {
            return user;
        }
    }
    return null;
}

User get_other_bank_user(string acc_no, OtherBank bank)
{
    User user = bank.get_user_by_acc_no(acc_no);
    return user;
}


void check_balance()
{
    User current_user = login();

    if (current_user == null)
    {
        return;
    }

    Console.WriteLine($"""
    Here is your Account status:

    Account name: {current_user.name}
    Account number: {current_user.account.id}
    Account Balance: {current_user.account.balance}
    """);
}

void transfer()
{
    User current_user = login();

    if (current_user == null)
    {
        return;
    }

    string transfer_id;
    do
    {
        Console.WriteLine("""
        1. OurBank To OurBank
        2. Transfer to other banks
        """);

        transfer_id = Console.ReadLine() ?? "";
        Console.WriteLine(transfer_id);
        
        if (transfer_id != "1" && transfer_id != "2") 
        {
            Console.WriteLine("""
            Invalid Input!
            """);
        }
    } while (transfer_id != "1" && transfer_id != "2");


    if (transfer_id == "1")
    {
        Console.Write($"""
        ----------- Transfer to OurBank ---------------
        Enter recipient's account number: 
        """);
        string recipient_acc = Console.ReadLine() ?? "";
        User? recepient = get_user(recipient_acc);
        if (recepient == null)
        {
            Console.WriteLine("""
            Invalid Account!
            """);
            return;
        }
        Console.Write("Enter amount to send: ");
        double amount = Convert.ToDouble(Console.ReadLine());
        ourbank.transfer_to_ourbank(current_user, recepient, amount, out string response);
        if (response == "Failed: Insurficient balance")
        {
            Console.WriteLine("""
            Failed: Insurficient balance
            """);
        }
        else
        {
            Console.WriteLine($"""
            your new balance: {current_user.account.balance} 

            recepient: {recepient.name.ToString()} Credited
        
            Transfer Successful
            """);
        }
    }
    else
    {
        Console.WriteLine("---------------- Transfer To Other Banks ---------------");
        string bank_id;
        do
        {
            Console.WriteLine("""
            Choose recepient's bank
            1. UBA                          2. Stanbic IBTC
            3. Union Bank                   4. Access Bank
            """);

            bank_id = Console.ReadLine() ?? "";
            
            if (bank_id != "1" && bank_id != "2" && bank_id != "3" && bank_id != "4") 
            {
                Console.WriteLine("Invalid Input!\nNumbers 1-4 are the only valid inputs");
            }

        } while (bank_id != "1" && bank_id != "2" && bank_id != "3" && bank_id != "4");

        OtherBank bank = banks[Convert.ToInt32(bank_id) - 1];
        Console.Write($"""
        Enter recipient's account number: 
        """);
        string recipient_acc = Console.ReadLine() ?? "";
        User? recepient = get_other_bank_user(recipient_acc, bank);
        Console.WriteLine($"""
        recipient: {recepient.name}
        """);
        if (recepient == null)
        {
            Console.WriteLine("""
            Invalid Account!
            """);
            return;
        }
        Console.Write("Enter amount to send:\n");
        double amount = Convert.ToDouble(Console.ReadLine());
        ourbank.Transfer_to_other_banks(current_user, recepient, bank, amount, out string response);
        if (response == "Failed: Insurficient balance")
        {
            Console.WriteLine("""
            Failed: Insurficient balance
            """);
            
        }
        else
        {
            Console.WriteLine($"""
            your new balance: {current_user.account.balance} 

            recepient: {recepient.name.ToString()} Credited
        
            Transfer Successful
            """);
        }
    }
}

void create_new_account()
{
    Console.WriteLine("what is your first name?");
    string first_name = Console.ReadLine() ?? "";

    Console.WriteLine("What is your last name?");
    string last_name = Console.ReadLine() ?? "";

    Console.WriteLine("Date of Birth - mm/dd/yyyy ");
    string d_o_b = Console.ReadLine() ?? "";

    Console.WriteLine("Password");
    string password = Console.ReadLine() ?? "";

    string repeat_password = "";
    do
    {
        Console.WriteLine("Repeat Password");
        repeat_password = Console.ReadLine() ?? "";
        if (password != repeat_password)
        {
            Console.WriteLine("Password not the same. Repeat password.");
        }
    } while (password != repeat_password);
    DateTime.TryParse(d_o_b, out DateTime date);
    User new_user = new User(last_name, first_name, date, password);
    users.Add(new_user);

    Console.WriteLine($"{ourbank.ToString()}\n{new_user.ToString()}");
}