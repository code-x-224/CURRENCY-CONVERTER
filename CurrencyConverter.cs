using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;


namespace Currencyconverter_trial
{
    internal class Program
    {
        public enum Currency
        {
            USD = 1,
            KES,
            GBP,
            EUR,
            JPY,
            CNY,
            CHF,
            SGD,
            ZAR,
            INR,
            AED,
            ALL,
            EGP,
            UGX,
            TZS,
            WST,
            ZMW,
            RUB,
            SAR,
            SBD,
            MXN,
            MVR,
            GEL,
            HKD,
            HUF,
            ETB,
            BZD,
            ANG,
            NGN,
            MYR
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("WELCOME TO CODE_X CURRENCY CONVERTER");
            Console.ResetColor();
            Console.WriteLine("You must be online for the converter to work\n");

            while (true)
            {
                Console.Write("Choose your ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("CURRENT ");
                Console.ResetColor();
                Console.WriteLine("currency from the ones listed below");
                DisplayCurrencyOptions();

                Currency currentCurrency = GetCurrencyInput("current");
                Console.Clear();

                Console.Write("Choose your");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" TARGET ");
                Console.ResetColor();
                Console.WriteLine("currency from the ones listed below");
                DisplayCurrencyOptions();

                Currency targetCurrency = GetCurrencyInput("target");

                double inputCurrency = GetAmountInput();

                double exchangeRate = GetExchangeRate(currentCurrency.ToString(), targetCurrency.ToString());

                double convertedAmount = inputCurrency * exchangeRate;

                Console.Write($"Converted {inputCurrency} ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write($"{currentCurrency} ");
                Console.ResetColor();
                Console.Write($" to {convertedAmount} ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"{targetCurrency}");
                Console.ResetColor();

                try
                {
                    int userChoice;
                    do
                    {
                        Console.WriteLine("Do you want to perform another conversion?");
                        Console.WriteLine("1. Yes");
                        Console.WriteLine("2. No");

                        if (int.TryParse(Console.ReadLine(), out userChoice) && (userChoice == 1 || userChoice == 2))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter 1 for 'Yes' or 2 for 'No'.");
                        }
                    } while (true);

                    if (userChoice != 1)
                    {
                        Console.WriteLine("Thank you. Come again");
                        break; // Exit the loop if the user enters anything other than "Yes"
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer (1 or 2).");
                }

            }
        }
        static void DisplayCurrencyOptions()
        {
            Console.WriteLine(@"
1. US Dollar                  11. UAE Dirham                    21. Mexican Peso  
2. Kenyan shilling            12. Albanian Lek                  22. Maldivian Rufiyaa     
3. Great  Britain Pound       13. Egyptian Pound                23. Georgian Lari
4. Euro                       14. Ugandan Shilling              24. Hong Kong Dollar
5. Japanese Yen               15. Tanzanian Shilling            25. Hungarian Forint 
6. Chinise Yuan               16. Samoan Tala                   26. Ethiopian Birr
7. Swiss Franc                17. Zambian Kwacha                27. Belize Dollar
8. Singapore Dollar           18. Russian Ruble                 28. Netherlands Antillian Guilder
9. South African Rand         19. Saudi Riyal                   29. Nigerian Naira
10. Indian Rupee              20. Solomon Island Dollar         30. Malaysian Ringgit
");
            Console.WriteLine();
        }

        static Currency GetCurrencyInput(string type)
        {
            while (true)
            {
                Console.WriteLine($"Please input the {type} currency (1-30)");

                try
                {
                    int currencyCode = Convert.ToInt32(Console.ReadLine());

                    if (Enum.IsDefined(typeof(Currency), currencyCode))
                    {
                        return (Currency)currencyCode;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid currency code.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                }
            }
        }

        static double GetAmountInput()
        {
            while (true)
            {
                Console.WriteLine("Please input the amount you wish to be converted");

                try
                {
                    return Convert.ToDouble(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }

        public static double GetExchangeRate(string currentCurrency, string targetCurrency)
        {
            try
            {
                string apiKey = "64becc9fa315de618e4a5cea";
                currentCurrency = currentCurrency.ToUpper();
                targetCurrency = targetCurrency.ToUpper();

                string url = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{currentCurrency}";

                string json;
                using (var client = new WebClient())
                {
                    json = client.DownloadString(url);
                }

                var data = JObject.Parse(json);
                var rate = (double)data["conversion_rates"][targetCurrency];
                return rate;
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error fetching exchange rate: {ex.Message}");
                //Console.WriteLine("Check your internet connection and try again");
                // Handle the exception or return a default value
                return 0.0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                // Handle the exception or return a default value
                return 0.0;
            }

        }

    }
}