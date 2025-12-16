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



void GetColor()
{
        if (letters[randomize] == 'X')
    {
        Console.ForegroundColor = ConsoleColor.Green;
    }
        if (letters[randomize]  == 'Y')
    {
        Console.ForegroundColor = ConsoleColor.Blue;
    }
        if (letters[randomize]  == 'Z')
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    } 
    
}

void PrintAndFind()
{
    //GetRandomChar();
    sleepTime = 500;
    char[,] matrix = new char[size, size];
    for (row = 0; row < size; row++)
    {
        for (col = 0; col < size; col++)
        {
            randomize = rnd.Next(letters.Length);
            matrix[row,col] = letters[randomize];
            Thread.Sleep(sleepTime);
            GetColor();
            Console.Write("|" + matrix[row,col] + "|");
        }
        Console.WriteLine();

    }
    
    for (int row = 0; row < size; row++)
    {
        for (int col = 0; col < size; col++)
        {
            //Console.WriteLine($"{matrix[row,col]} is in row {row}, col {col}");

            if (matrix[row,0] == matrix[row,1] && matrix[row,0] == matrix[row,2] || matrix[0,col] == matrix[1,col] && matrix[0,col] == matrix[2,col])
            {
                account += price;
                totalWinnings += price;
                Console.ReadKey();
                VictoryScreen();
            }
            else
            {
                Console.WriteLine("No win this time");
                totalLosses += bet;
                Console.ReadKey();
            }
        }
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