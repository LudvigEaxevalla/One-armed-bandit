using System.Reflection;

Random rnd = new Random();

char[] chars = {'X', 'Y', 'Z'};

int randomize = rnd.Next(0,3);
int sleepTime = 500;

int account = 0;
int bet = 0;
int price = 0;
int deposit = 0;
int totalLosses = 0;
int totalWinnings = 0;
int totalBetting = 0;
bool playing = false;


void Gamble()
{
    int first;
    int second;
    int third;

    Console.Clear();
    sleepTime = 500;
    Console.ForegroundColor = ConsoleColor.Green;
    Thread.Sleep(sleepTime);
    Console.WriteLine("WITH " + bet + " DOLLARS ON THE LINE\n\n");
    randomize = rnd.Next(0,3);
    first = randomize;
    Console.Write("|" + chars[randomize]+"|");
    Thread.Sleep(sleepTime);

    randomize = rnd.Next(0,3);
    second = randomize;
    if (second == first)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        sleepTime = 1000;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        totalLosses += bet;
    }
    Console.Write("|" + chars[randomize] + "|");
    Thread.Sleep(sleepTime);

    randomize = rnd.Next(0,3);
    third = randomize;
    if (third != second)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        totalLosses += bet;

    }
    Console.Write("|" + chars[randomize]+ "|");
    Console.ResetColor();
    if (second == first && third != second)
    {
        Console.WriteLine("So close!");
    }
    Console.WriteLine("\n\n...");
    Console.ReadKey();

    if (first == second && first == third)
    {
        account += price;
        totalWinnings += price;
        VictoryScreen();
    }

}


void VictoryScreen()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("You Won!!");
    Console.WriteLine("You betted " + bet + " and won the same ammount!");
    Console.WriteLine("Adding " + price + " to your account");
    Console.ResetColor();
    Console.ReadKey();
}

void OutOfMoney()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("You're out of money! Better luck next time");
    Console.WriteLine("You betted $" + totalBetting);
    Console.WriteLine("You won $" + totalWinnings + " in total");
    Console.WriteLine("You lost $" + totalLosses + " in total");
    playing = false;
    Console.ReadKey();
}

    void BettingScreen()
    {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Current balance: " + account + "\n");
    Console.WriteLine("How much would you like to bet?");
    bet = int.Parse(Console.ReadLine()!);
    totalBetting += bet;

    if (bet > account || bet < 1)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        if (bet > account)
        {
            Console.WriteLine("You can not bet more than whats in your account");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" (" + account + ") ");
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (bet < 1)
        {
            Console.WriteLine("You must bet something");
        }
        Console.WriteLine("Try again..");
        Console.ResetColor();
        Console.ReadKey();
        bet = 0;
    }

    else
    {
        account -= bet;
        price = bet*2;
        Gamble();
    }     
    }

void CashoutScreen()
{
    Console.Clear();
    Console.ResetColor();
    Console.WriteLine("Thank you for playing!");
    Console.WriteLine("You betted $" + totalBetting);
    Console.WriteLine("You won $" + totalWinnings + " in total");
    Console.WriteLine("You lost $" + totalLosses + " in total");
    Console.WriteLine("You cashed out with $" + account + "! Come back anytime");
    playing = false;
    Console.ReadKey();

}

Console.Clear();
Console.WriteLine(@"
Welcome to One Armed Bandit!

To start, decide how much money you want to deposit into your 
account. You may not deposit less than 10 or more than 1000.

");

while(deposit <= 0)
{
    Console.WriteLine("How mouch would you like to deposit");
    deposit = int.Parse(Console.ReadLine()!);

    if (deposit >= 10 && deposit <= 1000)
    {
        account += deposit;
        playing = true;
    }
    else
    {
        Console.WriteLine("Invalid ammount... Please try again");
        deposit = 0;
    }

}


while (playing)
{

    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Current balance: " + account + "\n");
    Console.ResetColor();

    Console.WriteLine(@"The rules of the game are simple - 
    1. Choose how much you want to bet
    2. If you get three of the same symbol, you win double the ammount
    3. Cash out anytime you like
    ");

    int option = 0;
    Console.WriteLine("\nEnter (1) to play\nEnter (2) to cash out");
    option = int.Parse(Console.ReadLine()!);

    switch (option)
    {
        case 1:
            BettingScreen();
        break;
        case 2:
            CashoutScreen();
        break;
        default:
            Console.WriteLine("Please choose 1 or 2");
            Console.ReadKey();
        break;
    }

    if (account <= 0)
    {
        OutOfMoney();
    }

}