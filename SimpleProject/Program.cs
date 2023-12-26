using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using CsvHelper;

namespace SimpleProject
{
    public class Person 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public string Gender { get; set; }
    }

    class Program
    {
        const string FilePath = "dataset.csv";
        const int RowsNumber = 1000;
        public static void CreateDataset()
        {
            Random random = new Random();
            string[] menNames = { "Benny", "David", "Dvir", "Joe", "Jony" };
            string[] womenNames = { "Tehila", "Shira", "Rut", "Naomi" };
            string[] lastNames = { "Lev", "Dvir", "Tal", "Pal", "David"};
            string[] gender = { "Male", "Female"};
            List<Person> records = new List<Person>();

            for (int i = 0; i < RowsNumber; i++)
            {
                int genderNum = random.Next(2);
                Person person = new Person()
                {
                    FirstName = genderNum==0 ? menNames[random.Next(menNames.Length)]
                : womenNames[random.Next(womenNames.Length)],
                    LastName = lastNames[random.Next(lastNames.Length)],
                    Age = random.Next(18, 71),
                    Weight = random.Next(87, 330),
                    Gender = gender[genderNum]
                };
                records.Add(person);
            }
            try
            {
                using (var writer = new StreamWriter(FilePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static List<Person> ReadData()
        {
            List<Person> records = new List<Person>();
            try
            {
                using (var reader = new StreamReader(FilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<Person>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return records;
        }
        static void Main(string[] args)
        {
           CreateDataset();
           List<Person> data=ReadData();
           // average age of all people
            double averageAges = data.Any()?data.Average(person => person.Age):0;
            //total number of people weighing between 120lbs and 140lbs
            int WeightingInRange = data.Where(person => person.Weight >= 120 && person.Weight <= 140).Count();
            //average age of the people weighing between 120lbs and 140lbs
            double averageAgesInRange = WeightingInRange>0 ? data.Where(person => person.Weight >= 120 && person.Weight <= 140).Average(person => person.Age) : 0;
            

            Console.WriteLine($"Average age of all people: {averageAges}");
            Console.WriteLine($"Total number of people weighing between 120lbs and 140lbs: {WeightingInRange}");
            Console.WriteLine($"Average age of people weighing between 120lbs and 140lbs: {averageAgesInRange}");
        }
    }
}
    

