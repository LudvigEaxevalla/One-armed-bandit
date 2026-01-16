using System.Data;
using System.Globalization;
using System.Numerics;
using System.Reflection;

Random rnd = new Random();

char[] letters = {'X', 'Y', 'Z'};

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
bool won = false;

int size = 3;
int row = 0;
int col = 0;
char[,] matrix = new char[size, size];





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
    Console.Write("|" + letters[randomize]+"|");

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
    }
    Console.Write("|" + letters[randomize] + "|");
    Thread.Sleep(sleepTime);

    randomize = rnd.Next(0,3);
    third = randomize;
    if (third != second)
    {
        Console.ForegroundColor = ConsoleColor.Red;

    }
    Console.Write("|" + letters[randomize]+ "|");
    Console.ResetColor();
    if (second == first && third != second)
    {
        Console.WriteLine("So close!");
    }
    Console.WriteLine("\n\n...");

    if (first == second && first == third)
    {
        account += price;
        totalWinnings += price;
        VictoryScreen();
    }
    else
    {
        totalLosses += bet;
        Console.ReadKey();
    }

} 



void GetColor(char c)
{
    Console.ForegroundColor = c switch
    {
        'X' => ConsoleColor.Green,
        'Y' => ConsoleColor.Blue,
        'Z' => ConsoleColor.Yellow,
        _ => ConsoleColor.White
    };
        Console.BackgroundColor = c switch
    {
        'X' => ConsoleColor.Red,
        'Y' => ConsoleColor.Yellow,
        'Z' => ConsoleColor.DarkBlue
    };
}

bool CheckWin()
{
    // Rows & columns
    for (int i = 0; i < size; i++)
    {
        if (matrix[i, 0] == matrix[i, 1] && matrix[i, 0] == matrix[i, 2])
            return true;

        if (matrix[0, i] == matrix[1, i] && matrix[0, i] == matrix[2, i])
            return true;
    }

    return false;
}

void PrintAndFind()
{
    //GetRandomChar();
    sleepTime = 500;
    for (int row = 0; row < size; row++)
    {
        for (int col = 0; col < size; col++)
        {
            char symbol = letters[rnd.Next(letters.Length)];
            matrix[row, col] = symbol;

            GetColor(symbol);
            Console.Write($"|{symbol}|");
            Thread.Sleep(sleepTime);
        }
        Console.WriteLine();
    }

    Console.ResetColor();
    Console.WriteLine("\n...");

    if (CheckWin())
    {
        account += price;
        totalWinnings += price;
        VictoryScreen();
    }
    else
    {
        Console.WriteLine("No win this time");
        totalLosses += bet;
        Console.ReadKey();
    }

}

 void BonusGamble()
{
    Console.Clear();
    sleepTime = 500;
    Console.ForegroundColor = ConsoleColor.Green;
    Thread.Sleep(sleepTime);
    Console.WriteLine("WITH " + bet + " DOLLARS ON THE LINE\n\n");
    PrintAndFind();

    /*for (int i = 1; i < 10; i++)
    {

        Thread.Sleep(sleepTime);
       /* GetRandomChar();
        PrintChar();
        if (i == 3 || i == 6)
        {
            Console.WriteLine("");
        }
    //Console.ReadKey();
    //FindPos();


    }  */
    
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
        totalBetting += bet;
        account -= bet;
        price = bet*2;
        //Gamble();
        BonusGamble();
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
    Console.WriteLine("How mouch would you like to deposit (Minimum 10, Maximum 1000)");

    while (!int.TryParse(Console.ReadLine(), out deposit) || deposit < 10 || deposit > 1000)
    {
        Console.WriteLine("Invalid amount. Try again.");
    }

    account = deposit;
    playing = true;

}


while (playing)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Balance: {account}");
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(@"
    
Rules:
1. Bet
2. Win when you get three (X, Y or Z) in a row, horizontaly or verticly
    
Cash out any time
    ");
    Console.ResetColor();
    Console.WriteLine("\nWhat do you wanna do?");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("(1) Play");
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("(2) Cash out");

    if (!int.TryParse(Console.ReadLine(), out int option))
        continue;

    if (option == 1)
        BettingScreen();
    else if (option == 2)
        CashoutScreen();

    if (account <= 0)
    {
        Console.WriteLine("You're out of money!");
        break;
    }
}