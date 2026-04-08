using System.Security.Cryptography.X509Certificates;
using Models;

bool keep_going = true;
Bank bank = new Bank();
DateTime.TryParse("01/09/2006", out DateTime date);
DateTime.TryParse("09/17/2009", out DateTime dipo_date);
User[] users = {
    new User("olunuga", "olaoluwa", bank, date, "olaholunuga"),
    new User("bolu", "dipo", bank, dipo_date, "dipopo")
};



Console.WriteLine($"""
{users[0]}
{users[1]}
""");

while (keep_going)
{
    Console.WriteLine("""
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
        keep_going = false;
    }
}

User login()
{
    Console.WriteLine("Enter your account number: ");
    var acc_number = Console.ReadLine() ?? ""; 
    User? user = get_user_and_account(acc_number, out Account? acc);
    if (user == null)
    {
        Console.WriteLine("Account number does not exist");
        return user;
    }
    User current_user = user;
    return current_user;
}

User get_user_and_account(String acc_no, out Account? acc)
{
    foreach (var user in users)
    {
        acc = user.account;
        if (acc.id == acc_no)
        {
            return user;
        }
    }
    acc = null;
    return null;
}

void transfer()
{
    User current_user = login();
    Console.WriteLine("""
    1. OurBank To OurBank
    2. Transfer to other banks
    """);
    string transfer_id;
    do
    {
        transfer_id = Console.ReadLine() ?? "";

    } while (transfer_id == "");
    if (transfer_id == "1")
    {
        Console.WriteLine("Enter recipient's account number: ");
        string recipient_acc = Console.ReadLine() ?? "";
        User? recepient = get_user_and_account(recipient_acc, out Account? acc);
        Console.WriteLine("how much do you want to send");
        double amount = Convert.ToDouble(Console.ReadLine());
        bank.transfer_to_ourbank(current_user, recepient, amount, out string response);
        Console.WriteLine($"""
        you: {current_user} your balance: {current_user.account.balance} 

        recepient: {recepient} balance: {recepient.account.balance}
        
        {response}
        """);
    } else if (transfer_id == "2")
    {
        
    } else
    {
        
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
    User current_user = new User(last_name, first_name, bank, date, password);

    Console.WriteLine($"{bank.ToString()} \n {current_user.ToString()}");
}