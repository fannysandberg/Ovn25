using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ovn25
{
    class Program
    {
        public static List<Person> contactList = new List<Person>();

        static void Main(string[] args)
        {
            string[] names = File.ReadAllLines(@"C:\Projects\Ovn24\Ovn24\names.csv", Encoding.Default);

            for (int i = 0; i < names.Length; i++)
            {
                string[] data = names[i].Split(';');

                Person aPerson = new Person();
                aPerson.name = data[0];
                aPerson.nameDay = DateTime.Parse(data[1]);

                bool nameFound = false;

                //Console.WriteLine($"Namn: {aPerson.Name}, Namnsdag: {aPerson.NameDay}");

                for (int x = 0; x < contactList.Count; x++)
                {
                    if (contactList.ElementAt(x).name == aPerson.name)
                    {
                        nameFound = true;
                        break;
                    }
                }
                if (!nameFound) // betyder if nameFound == false;
                {
                    contactList.Add(aPerson);
                }

            }

            foreach (var n in contactList.FindAll(x => x.name.StartsWith("And")))
            {
                Console.WriteLine(n.name);
            }

            foreach (var n in contactList.FindAll(x => x.nameDay.Equals(DateTime.Parse("2015-07-23 00:00:00"))))
            {
                Console.WriteLine($"De namn som har namnsdag 23 juli är: {n.name}");
            }

            foreach (var n in contactList.FindAll(x => x.name.StartsWith("P")).Where(d => d.nameDay.Month == 4))
            {
                Console.WriteLine($"De som börjar på p och har namnsdag i april är: {n.name}");
            }


            //FindContact();
            //FindNameDay();
            //CountNames();
            NameDayCounter();
            Quartal();

        }

        static void FindContact()
        {

            bool wrongInput = true;
            string search = string.Empty;

            do
            {
                Console.WriteLine("Skriv in minst en bokstav för att söka i listan!");

                try
                {
                    search = Console.ReadLine().ToUpper();
                    wrongInput = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Inga matchningar finns!");
                }


            } while (wrongInput);



            //var userSearch =
            //    from n in contactList
            //    where n.name.StartsWith(search)
            //    select n;

            Console.WriteLine("Sök på ett namn i databasen: ");
            search = Console.ReadLine();

            foreach (var n in contactList.FindAll(x => x.name == search))
            {
                Console.WriteLine("Din sökning fick följande träffar:");
                Console.WriteLine($"{n.name}");
                //Finns ingen respons om man inte får någon träff
            }

            //if (search.Length > 0 && contactList.FindAll(x => x.name.StartsWith(search)) == 0)
            //{
            //    Console.WriteLine("Din sökning gav inga träffar.");
            //}

            //else
            //{
            //    Console.WriteLine("Din sökning fick följande träffar:");

            //    foreach (var n in userSearch)
            //    {
            //        Console.WriteLine($"{n.name}");

            //    }
            //}
        }

        static void FindNameDay()
        {
            bool wrongInput = true;
            string date = string.Empty;

            do
            {
                Console.WriteLine("Skriv in ett datum i följande format: (YYYY-MM-DD)");

                try
                {
                    date = Console.ReadLine();
                    wrongInput = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Vänligen ange en korrekt inmatning!");
                }


            } while (wrongInput);

            foreach (var n in contactList.FindAll(x => Convert.ToString(x.nameDay) == date))
            {
                Console.WriteLine("Följande namn har namnsdag detta datum: ");
                Console.WriteLine($"{n.name}");

            }

        }

        static void CountNames()
        {
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ".ToCharArray();

            foreach (char letter in alphabet)
            {
                var letterCount = contactList.FindAll(n => n.name.StartsWith(letter.ToString()));

                Console.WriteLine($"{letter}: {letterCount.Count()}");

            }
        }

        static void NameDayCounter()
        {

            for (int num = 1; num < 13; num++)
            {
                var currentMonth = contactList.FindAll(x => Convert.ToString(x.nameDay.Month) == Convert.ToString(num));
                Console.WriteLine($"I {NumberToMonth(num)} har {currentMonth.Count} personer namnsdag!");


            }


        }

        private static string NumberToMonth(int num)
        {
            switch (num)
            {
                case 1:
                    return "januari";

                case 2:
                    return "februari";
                case 3:
                    return "mars";

                case 4:
                    return "april";

                case 5:
                    return "maj";

                case 6:
                    return "juni";

                case 7:
                    return "juli";

                case 8:
                    return "augusti";

                case 9:
                    return "september";

                case 10:
                    return "oktober";

                case 11:
                    return "november";

                case 12:
                    return "december";

                default:
                    return "Error";



            }
        }

        static void Quartal()
        {
            //varje kvartal
            for (int i = 1; i <= 12; i+=3)
            {
                var newList =
                    from n in contactList
                    where n.nameDay.Month.Equals(i) || n.nameDay.Month.Equals(i + 1) || n.nameDay.Month.Equals(i + 2)
                    select n;

                List<Person> month = newList.ToList();

                Console.WriteLine(month[0].nameDay.ToString("MMM") + ":" + month.Count() + " personer har namnsdag detta kvartal.");
                //"MMM" converts to 3 letter name of month
            }
         

        }
    }

}
