using Models;
using DataStorage;
bool keep_going = true;

Bank ourbank = new Bank();

OtherBank[] banks = DataStore.Load_banks();

List<User> users = DataStore.Load_users();

List<User> other_users = DataStore.other_users(banks);

List<Account> user_accounts = DataStore.user_accounts();
List<OtherBankAccount> other_accounts = DataStore.other_bank_accounts();

foreach (Account account in user_accounts)
{
    Console.WriteLine(
        $"""
        {account.Id}
        {account.Balance}
        """
    );
}
foreach (User user in users)
{
    Console.WriteLine(
        $"""
        {user.Account}
        {user.Name}
        """
    );
}
foreach (OtherBank bank in banks)
{
    Console.WriteLine(
        $"""
        {bank.Name}
        """
    );
}


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

    int option = Convert.ToInt32(inputInt(1, 6));

    switch (option)
    {
        case 1:
            create_new_account();
            DataStore.save_users(users);
            break;
        case 2:
            transfer();
            DataStore.save_accounts(user_accounts);
            DataStore.save_other_accounts(other_accounts);
            // DataStore.save_users(users);
            // DataStore.save_other_users(other_users);
            break;
        case 3:
            check_balance();
            break;
        case 4:
            withdraw();
            DataStore.save_accounts(user_accounts);
            // DataStore.save_users(users);
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
    Console.WriteLine("You can press any key to continue.");
    string _continue = Console.ReadLine() ?? "";
    if (_continue == "no" || _continue == "NO" || _continue == "No" || _continue == "n")
    {
        Console.WriteLine("Thank you for banking with us");
        keep_going = false;
    }
}
DataStore.save_other_users(other_users);
DataStore.save_users(users);
DataStore.save_accounts(user_accounts);
DataStore.save_other_accounts(other_accounts);

string inputString(int lenght = 3)
{
    string userInput;
    do
    {
        userInput = Console.ReadLine() ?? "";
        userInput.Trim();
        if (!userInput.All(char.IsLetter) || string.IsNullOrWhiteSpace(userInput) || string.IsNullOrEmpty(userInput) || userInput.Length < lenght)
        {
            Console.WriteLine("Input cannot be empty, symbols or whitespace. Input valid letter only");
            Console.WriteLine($"Input must be at least  of lenght {lenght}");
        }
    } while (!userInput.All(char.IsLetter) || string.IsNullOrWhiteSpace(userInput) || string.IsNullOrEmpty(userInput) || userInput.Length < lenght);
    return userInput;
}

string inputInt(int min, int max)
{
    string userInput;
    do
    {
        userInput = Console.ReadLine() ?? "";
        userInput.Trim();
        if (!int.TryParse(userInput, out int res) || string.IsNullOrWhiteSpace(userInput) || string.IsNullOrEmpty(userInput) || res < min || res > max)
        {
            Console.WriteLine($"Input cannot be empty, symbols or whitespace. Input valid digits {min}-{max} only");
        }
    } while (!int.TryParse(userInput, out int result) || string.IsNullOrWhiteSpace(userInput) || string.IsNullOrEmpty(userInput) || result < min || result > max);
    return userInput;
}

string inputDigits(string prompt, int lenght = 10, int min = 0)
{
    string userInput;
    Console.WriteLine(prompt);
    long red;
    do
    {
        userInput = Console.ReadLine() ?? "";
        userInput.Trim();
        long.TryParse(userInput, out red);
        if (!userInput.All(char.IsDigit) || string.IsNullOrWhiteSpace(userInput) || string.IsNullOrEmpty(userInput) || userInput.Length < lenght || red <= min)
        {
            Console.WriteLine("why this is happening");
            Console.WriteLine(!userInput.All(char.IsDigit));
            Console.WriteLine(string.IsNullOrWhiteSpace(userInput));
            Console.WriteLine(string.IsNullOrEmpty(userInput));
            Console.WriteLine(userInput.Length < lenght);
            Console.WriteLine(red <= min);
            Console.WriteLine("End test");
            Console.WriteLine("Input cannot be letters, empty, symbols or whitespace. Input numbers having 0-9 only");
            Console.WriteLine($"Input must be at least of lenght {lenght} and not less than {min}");
            Console.WriteLine(prompt);
        }
    } while (!userInput.All(char.IsDigit) || string.IsNullOrWhiteSpace(userInput) || string.IsNullOrEmpty(userInput) || userInput.Length < lenght || red <= min);
    return userInput;
}

DateTime inputDate()
{
    string userInput;
    DateTime date;
    DateOnly today;
    do
    {
        userInput = Console.ReadLine() ?? "";
        bool dateBool = DateTime.TryParse(userInput, out date);
        today = DateOnly.FromDateTime(DateTime.Now);
        if (!dateBool || DateOnly.FromDateTime(date) > today || DateOnly.FromDateTime(date) < today.AddYears(-100))
        {
            Console.WriteLine("Enter valid date in the following format - mm/dd/yyyy");
            Console.WriteLine("Date must be at most 100 years before today.");
            Console.WriteLine("Enter a valid date");
        }
        
    } while (!DateTime.TryParse(userInput, out date) || DateOnly.FromDateTime(date) > today || DateOnly.FromDateTime(date) < today.AddYears(-100));
    return date;
}

// password to be treated later
string inputPassword(string prompt, int lenght = 3)
{
    string userInput;
    Console.WriteLine(prompt);
    do
    {
        userInput = Console.ReadLine() ?? "";
        userInput.Trim();
        if ((!userInput.Any(char.IsLetterOrDigit) && !userInput.Any(char.IsSymbol)) || string.IsNullOrEmpty(userInput) || userInput.Length < lenght)
        {
            Console.WriteLine("Input cannot be empty, whitespace. Input valid letters, numbers or symbols");
            Console.WriteLine($"Password lenght must be more than {lenght}");
            Console.WriteLine(prompt);
        }
    } while ((!userInput.Any(char.IsLetterOrDigit) && !userInput.Any(char.IsSymbol)) || string.IsNullOrEmpty(userInput) || userInput.Length < lenght);
    return userInput;
}

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
        {user.Name}
        {user.Account}
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
            {user.Name}
            {user.Account}
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

    // Console.Write("Enter the amount of money you want to save");
    double amount = Convert.ToDouble(inputDigits("Enter the amount of money you want to save", 1, 1));
    string account = current_user.Account;
    var user_account = user_accounts.FirstOrDefault(u => u.Id == account);
    user_account.Balance += amount;
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
    // Console.Write("Enter the amount of money you want to withdraw");
    double amount = Convert.ToDouble(inputDigits("Enter the amount of money you want to withdraw", 1, 1));
    string account = current_user.Account;
    var user_account = user_accounts.FirstOrDefault(u => u.Id == account);
    if (user_account.Balance < amount)
    {
        Console.WriteLine("""
        Transaction Unsuccessfull! Insufficient account balance.
        """);
        return;
    }
    user_account.Balance -= amount;
    Console.WriteLine($"""
    Take your cash >
    ${amount} withdrawn successfully!!
    """);
}

User login()
{
    // Console.WriteLine("Enter your account number: ");
    var acc_number = inputDigits("Enter your account number: "); 
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
        // Console.WriteLine("Enter your password: ");
        pass = inputPassword("Enter your password: ");

        if (user.check_password(pass) == false)
        {
            Console.WriteLine("wrong password!!");
        }
        retry_num += 1;
    } while (user.check_password(pass) == false || retry_num >= 3);
    if (retry_num >= 3)
    {
        Console.WriteLine("You've exceeded the number of retrys");
        Console.WriteLine("Try again later.");
        return null;
    }
    return user;
}

User get_user(String acc_no)
{
    // foreach (var user in users)
    // {
    //     Account acc = user.account;
    //     if (acc.id == acc_no)
    //     {
    //         return user;
    //     }
    // }
    var user = users.FirstOrDefault(u => u.Account == acc_no);
    return user;
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

    Account name: {current_user.Name}
    Account number: {current_user.Account}
    Account Balance: {(user_accounts.FirstOrDefault(u => u.Id == current_user.Account)).Balance}
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
        // Console.WriteLine(transfer_id);
        
        if (transfer_id != "1" && transfer_id != "2") 
        {
            Console.WriteLine("""
            Invalid Input!
            Enter 1 or 2.
            """);
        }
    } while (transfer_id != "1" && transfer_id != "2");


    if (transfer_id == "1")
    {
        string p = $"""
        ----------- Transfer to OurBank ---------------
        Enter recipient's account number: 
        """;
        string recipient_acc = inputDigits(p);
        User? recepient = get_user(recipient_acc);
        if (recepient == null)
        {
            Console.WriteLine("""
            Invalid Account!
            """);
            return;
        }
        // Console.Write("Enter amount to send: ");
        double amount = Convert.ToDouble(inputDigits("Enter amount to send: ", 1));
        var account = user_accounts.FirstOrDefault(u => u.Id == current_user.Account);
        if (account.Balance < amount)
        {
            Console.WriteLine(
                """
                Failed transaction: Insuficient Balance.
                """
            );
        }
        else
        {
            account.Balance -= amount;
            var receipient_acc = user_accounts.FirstOrDefault(u => recepient.Account == u.Id);
            receipient_acc.Balance += amount;
            Console.WriteLine($"""
            your new balance: {account.Balance} 

            recepient: {recepient.Name} Credited
        
            Transfer Successful
            """);
        }
        // ourbank.transfer_to_ourbank(current_user, recepient, amount, out string response);
        // if (response == "Failed: Insurficient balance")
        // {
        //     Console.WriteLine("""
        //     Failed: Insurficient balance
        //     """);
        // }
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
        string p = $"""
        Enter recipient's account number: 
        """;
        string recipient_acc = inputDigits(p);
        User? recepient = get_other_bank_user(recipient_acc, bank);
        Console.WriteLine($"""
        recipient: {recepient.Name}
        """);
        if (recepient == null)
        {
            Console.WriteLine("""
            Invalid Account!
            """);
            return;
        }
        // Console.Write("Enter amount to send:\n");
        double amount = Convert.ToDouble(inputDigits("Enter amount to send:\n", 1, 1));
        var user_account = user_accounts.FirstOrDefault(u => u.Id == current_user.Account);
        var receipient_acc = other_accounts.FirstOrDefault(u => u.Id == recipient_acc);
        if (user_account.Balance < amount)
        {
            Console.WriteLine(
                """
                Faild Transaction: Insuficient Balance
                """
            );
        }
        else
        {
            // withraw from user and add to receipient.
            user_account.Balance -= amount;
            receipient_acc.Balance += amount;
            Console.WriteLine($"""
            your new balance: {user_account.Balance} 

            recepient: {recepient.Name} Credited
        
            Transfer Successful
            """);
        }


        // ourbank.Transfer_to_other_banks(current_user, recepient, bank, amount, out string response);
        // if (response == "Failed: Insurficient balance")
        // {
        //     Console.WriteLine("""
        //     Failed: Insurficient balance
        //     """);
            
        // }
    }
}

void create_new_account()
{
    Console.WriteLine("what is your first name?");
    string first_name = inputString();

    Console.WriteLine("What is your last name?");
    string last_name = inputString();

    Console.WriteLine("Date of Birth - mm/dd/yyyy ");
    DateTime d_o_b = inputDate();

    // Console.WriteLine("Password");
    string password = inputPassword("Password");

    string repeat_password = "";
    do
    {
        // Console.WriteLine("Repeat Password");
        repeat_password = inputPassword("Repeat Password");
        if (password != repeat_password)
        {
            Console.WriteLine("Password not the same. Repeat password.");
        }
    } while (password != repeat_password);
    Account account = new Account();
    string account_id = Convert.ToString(account.Id);
    User new_user = new User(last_name, first_name, d_o_b, password, account_id);
    users.Add(new_user);
    user_accounts.Add(account);

    Console.WriteLine($"{ourbank}\n{new_user}");
}