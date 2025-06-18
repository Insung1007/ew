using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SharedRiding
{
    interface IElectrical
    {
        int MaxSpeed { get; set; }
        double BatterCapacity { get; set; }
    }
    
    public abstract class SharedVehicle 
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        private bool isRent;
        public bool IsRent
        {
            get { return isRent; }
        }
        private DateTime rentDate;
        public DateTime RentDate
        {
            get { return rentDate; }
            set { rentDate = value; }
        }
        public SharedVehicle()
        {
        }
        public SharedVehicle(int number, string name)
        {
            Number = number;
            this.Name = name;
            isRent = false;
            rentDate = DateTime.Now;
        }
        public SharedVehicle(int number, DateTime rentDate)
        {
            this.number = number;
            this.rentDate = rentDate;
        }
        public bool Rent()
        {
            this.isRent = true;
            return isRent;
        }
        public bool Return()
        {
            this.isRent = false;
            return isRent;
        }
        public abstract string Status();
    }

    class Bicycle : SharedVehicle
    {
        public Bicycle() { }
        
        public Bicycle(int number, string name) : base(number, name)
        {
        }
        public override string Status()
        {
            if (IsRent == true)
                return "자전거 " + Name + "은(는) 대여중입니다. 대여시간 : " + RentDate.ToString("yyyy-MM-dd HH:mm:ss");
            else
                return "자전거 " + Name + "은(는) 반납되었습니다. 대여가능합니다. \n반납시간 : " + RentDate.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
    class ElectricBicycle : SharedVehicle, IElectrical
    {
        public int MaxSpeed { get; set; }
        public double BatterCapacity { get; set; }
        
        public ElectricBicycle(int number, string name) :base(number, name)
        {
        }
        public override string Status()
        {
            if(IsRent == true)
                return "전기 자전거 " + Name + "은(는) 대여중입니다. 대여시간 : " + RentDate.ToString("yyyy-MM-dd HH:mm:ss");
            else
                return "전기 자전거 " + Name + "은(는) 반납되었습니다. 대여가능합니다. \n반납시간 : " + RentDate.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
    class ElectricKickBoard : SharedVehicle, IElectrical
    {
        public int MaxSpeed { get; set; }
        public double BatterCapacity { get; set; }
        public ElectricKickBoard(int number, string name) : base(number, name)
        {
        }

        public override string Status()
        {
            if (IsRent == true)
                return "전동킥보드 " + Name + "은(는) 대여중입니다. 대여시간 : " + RentDate.ToString("yyyy-MM-dd HH:mm:ss");
            else
                return "전동킥보드 " + Name + "은(는) 반납되었습니다. 대여가능합니다. \n반납시간 : " + RentDate.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

    internal class Program
    {
        static List<SharedVehicle> sharedVehicles = new List<SharedVehicle>();     

        static void Main(string[] args)
        {
            sharedVehicles.Add(new Bicycle(1,"B001"));
            sharedVehicles.Add(new Bicycle(2,"B002"));
            sharedVehicles.Add(new ElectricBicycle(3,"EB001"));
            sharedVehicles.Add(new ElectricBicycle(4,"EB002"));
            sharedVehicles.Add(new ElectricKickBoard(5,"EKB001"));
            sharedVehicles.Add(new ElectricKickBoard(6,"EKB002"));



            while (true) {
                Console.WriteLine("\n-------------------------------");
                Console.WriteLine("1. 대여하기");
                Console.WriteLine("2. 반납하기");
                Console.WriteLine("3. 전체보기");                
                Console.WriteLine("4. 종료하기");
                Console.WriteLine("-------------------------------");
                Console.Write(": ");
                var sel = Console.ReadLine().Trim();

                switch (sel) {
                    case "1":
                        DoRent();
                        break;
                    case "2":
                        DoReturn();
                        break;
                    case "3":
                        View();
                        break;
                    case "4":
                        return;
                }
            }
        }

        private static void DoRent()
        {

            //대여 가능한 탈 것만 리스트로 출력한다.
            //단, 일반 자전거 / 전기 자전거 / 전동 킥보드 별로 구별하여 출력한다.
            
            Console.WriteLine("\n현재 대여 가능 목록입니다.");
            
            foreach (var vehicle in sharedVehicles)
            {
                if ((vehicle.IsRent == false))
                {
                    if(vehicle is Bicycle)
                    {
                        Console.WriteLine("일반 자전거 : " + vehicle.Number +"번 " + vehicle.Name + " 대여가능");
                    }
                    else if (vehicle is ElectricBicycle)
                    {
                        Console.WriteLine("전기 자전거 : " + vehicle.Number + "번 " + vehicle.Name + " 대여가능");
                    }
                    else if (vehicle is ElectricKickBoard)
                    {
                        Console.WriteLine("전동 킥보드 : " + vehicle.Number + "번 " + vehicle.Name + " 대여가능");
                    }
                }   
            }
            
            //대여 가능한 탈 것이 있으면
            //등록번호를 받고, Rent()를 호출하여 대여를 진행한다.
            Console.Write("몇번을 대여하시겠습니까?(취소는 0) : ");
            int number = int.Parse(Console.ReadLine());
            if (number == 0)
                return;
            foreach (var v in sharedVehicles)
            {
                if (v.Number == number)
                {
                    if(v.Rent()) 
                        Console.WriteLine(v.Number + "번 " + v.Name + "을(를) 대여, 대여시간 : " + v.RentDate);                    
                }
            }
        }

        private static void DoReturn()
        {

            //반납 가능한 탈 것만 리스트로 출력한다.
            //단, 일반 자전거 / 전기 자전거 / 전동 킥보드 별로 구별하여 출력한다.
            
            Console.WriteLine("\n현재 반납 가능 목록입니다.");

            foreach (var vehicle in sharedVehicles)
            {
                if ((vehicle.IsRent == true))
                {
                    if (vehicle is Bicycle)
                    {
                        Console.WriteLine("일반 자전거 : " + vehicle.Number + "번 " + vehicle.Name + " 반납가능");
                    }
                    else if (vehicle is ElectricBicycle)
                    {
                        Console.WriteLine("전기 자전거 : " + vehicle.Number + "번 " + vehicle.Name + " 반납가능");
                    }
                    else if (vehicle is ElectricKickBoard)
                    {
                        Console.WriteLine("전동 킥보드 : " + vehicle.Number + "번 " + vehicle.Name + " 반납여가능");
                    }
                }
            }

            //반납 가능한 탈 것이 있으면
            //등록번호를 받고, Return()를 호출하여 반납를 진행한다.
            Console.Write("몇번을 반납하시겠습니까?(취소는 0) : ");
            int number = int.Parse(Console.ReadLine());
            if (number == 0)
                return;
            foreach (var v in sharedVehicles)
            {
                if (v.Number == number)
                {
                    if (v.Return())
                        Console.WriteLine(v.Number + "번 " + v.Name + "을(를) 반납, 반납시간 : " + v.RentDate);
                }
            }
        }

        private static void View()
        {
            //모든 탈 것의 상태를 리스트로 출력한다.
            //단, 일반 자전거 / 전기 자전거 / 전동 킥보드 별로 구별하여 출력한다.
           foreach(var v in sharedVehicles)
            {
                Console.WriteLine(v.Number + "번 " + v.Status());
            }
        }
        
    }
}
