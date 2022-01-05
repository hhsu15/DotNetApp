using System;
using System.Collections.Generic;
//using World;

namespace Hello
{
    class Car
    {
        public string color = "red";
        public int age;
        public string brand;

        private int num = 5;

        // constructor
        public Car(int carAge, string carBrand = "Honda")
        {
            age = carAge;
            brand = carBrand;
        }


        public void method1()
        {
            Console.WriteLine("hello");
            Console.WriteLine(num);  // access private field
        }
        public static void method2()
        {
            Console.WriteLine("this is a static method");
        }

        public string method3(string greetings)
        {
            return greetings;
        }

        public Dictionary<string, string> method4()
        {

            Dictionary<string, string> nameDict = new Dictionary<string, string>();
            nameDict.Add("Bob", "A");
            nameDict.Add("Ken", "B");
            nameDict.Add("John", "C");
            return nameDict;
        }

        public int Num
        {
            get
            {
                Console.WriteLine("Calling getter");
                return num;
            }
            set
            {
                Console.WriteLine("Calling setter");
                num = value;
            }
        }

        public string method5()
        {
            // usde "this"
            string somestring = "OK";
            this.color = "new color";
            return this.method3("~Man") + somestring;
        }

    }

    interface IVehicle
    {
        void makeSound();
    }

    class Truck : IVehicle
    // Interface
    {
        public void makeSound()
        {
            Console.WriteLine("Vrooom");
        }
    }

    class MyProgram
    {
        static void Main(string[] args)
        {
            Car myObj = new Car(carAge: 20);
            Console.WriteLine($"The color is {myObj.color} and the age is {myObj.age}");
            myObj.method1();
            Car.method2();
            // myObj.method2(); // this will raise an error
            Console.WriteLine(myObj.method3("Hi there"));

            Dictionary<string, string> myDict = myObj.method4();
            Console.WriteLine(myDict["Bob"]);
            foreach (var item in myDict)
            {
                // Console.WriteLine(item.Key);
                // Console.WriteLine(item.Value);
                Console.WriteLine($"{item.Key}:{item.Value}");
            }

            Console.WriteLine(myObj.Num);
            myObj.Num = 10;
            Console.WriteLine(myObj.Num);
            Console.WriteLine(myObj.method5());
            Console.WriteLine(myObj.color);

            Truck truck = new Truck();
            truck.makeSound();

            // World world = new SomeClass();
            // world.sayWorld();

        }


    }
}
