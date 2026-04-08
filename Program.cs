using Models;

bool keep_going = true;

Bank bank = new Bank();

OtherBank[] banks =
{
    new OtherBank("UBA"),
    new OtherBank("Stanbic IBTC"),
    new OtherBank("Union Bank"),
    new OtherBank("Access Bank")
};

OtherBank bank1 = new OtherBank("UBA");
OtherBank bank2 = new OtherBank("Stanbic IBTC");
OtherBank bank3 = new OtherBank("Union Bank");
OtherBank bank4 = new OtherBank("Access Bank");

DateTime.TryParse("01/09/2006", out DateTime date);
DateTime.TryParse("09/17/2009", out DateTime dipo_date);

User[] users = {
    new User("olunuga", "olaoluwa", date, "olaholunuga"),
    new User("bolu", "dipo", dipo_date, "dipopo")
};

Console.WriteLine($"""
{users[0]}
{users[1]}
""");

while (keep_going)
{
    Console.WriteLine("""
    ================ OurBank =====================
                     welcome
    ==============================================

    What would you like to do?
    1. Create new Account             2. Transfer
    3. Check balance                  4. Withdraw
    5. Save
    """);

    int account = Convert.ToInt32(Console.ReadLine());

    switch (account)
    {
        case 1:
            create_new_account();
            break;
        case 2:
            transfer();
            break;
        case 3:
            break;
        case 4:
            break;
        case 5:
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
    User current_user = user;
    return current_user;
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

void transfer()
{
    User current_user = login();

    string transfer_id;
    do
    {
        Console.WriteLine("""
        1. OurBank To OurBank
        2. Transfer to other banks
        """);

        transfer_id = Console.ReadLine() ?? "";
        if (transfer_id != "1" || transfer_id != "2")
        {
            Console.WriteLine("""
            Invalid Input!
            """);
        }
    } while (transfer_id != "1" || transfer_id != "2");


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
        bank.transfer_to_ourbank(current_user, recepient, amount, out string response);
        Console.WriteLine($"""
        your new balance: {current_user.account.balance} 

        recepient: {recepient.name.ToString()} Credited
        
        Transfer Successful

        """);
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

            Console.WriteLine("Invalid Input!\nNumbers 1-4 are the only valid inputs");

        } while (bank_id != "1" || bank_id != "2" || bank_id != "3" || bank_id != "4");


        Console.Write($"""
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
        bank.Transfer_to_other_banks(current_user, recepient, banks[Convert.ToInt32(bank_id)], amount, out string response);
        Console.WriteLine($"""
        your new balance: {current_user.account.balance} 

        recepient: {recepient.name.ToString()} Credited
        
        Transfer Successful

        """);
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
    User current_user = new User(last_name, first_name, date, password);

    Console.WriteLine($"{bank.ToString()} \n {current_user.ToString()}");
}