bool keep_going = true;

while (keep_going)
{
    Console.WriteLine("""
    What would you like to do?
    1. Create new Account             2. Transfer
    3. Check balance                  4. Withdraw
    5. Save                           6. Close Account
    """);

    int account = Convert.ToInt32(Console.ReadLine());

    switch (account)
    {
        case 1: create_new_account();
        break;
        case 2:
        break;
        case 3:
        break;
        case 4:
        break;
        case 5:
        break;
        case 6:
        break;
        
        default:
        break;
    }

    Console.WriteLine("Would you like to do something else? \"Yes\" or \"No\"");
    string _continue = Console.ReadLine() ?? "";
    if (_continue == "no" || _continue == "NO" || _continue == "No" || _continue == "n")
    {
        keep_going = false;
    }
}

void create_new_account()
{
    Console.WriteLine("what is your first name?");
    string first_name = Console.ReadLine() ?? "";

    Console.WriteLine("What is your last name?");
    string last_name = Console.ReadLine() ?? "";

    Console.WriteLine("Date of Birth - dd/mm/yyyy ");
    string d_o_B = Console.ReadLine() ?? "";

    Console.WriteLine("Password");
    string Password = Console.ReadLine() ?? "";

    Console.WriteLine("Repeat Password");
    string RepeatPassword = Console.ReadLine() ?? "";
}