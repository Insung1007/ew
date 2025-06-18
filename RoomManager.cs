using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomManager
{
    abstract class Usage
    {
        private int usageAmount;
        public int UsageAmount
        {
            get { return usageAmount; }
            set { usageAmount = value; }
        }
        private double unitPrice;
        public double UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }
        public Usage(int usageAmount, double unitprice)
        {
            this.usageAmount = usageAmount;
            this.unitPrice = unitprice;
        }
        public abstract double CalculateCost();
        
    }
    class ElectricityUsage : Usage    {
            
        public ElectricityUsage(int usageAmount, double unitprice) : base(usageAmount, unitprice)
        {
        }
        public override double CalculateCost()
        {
            return UsageAmount * UnitPrice;
        }
    }
    class WaterUsage : Usage    {
        
        public WaterUsage(int usageAmount, double unitprice) : base(usageAmount, unitprice)
        {
        }
        public override double CalculateCost()
        {
            return UsageAmount*UnitPrice;
        }
    }
    class Room
    {
        private string number;
        public string Number
        {
            get { return number; }
        }
        private WaterUsage[] waterUsageMonths =new WaterUsage[12];
        private ElectricityUsage[] electricityUsageMonths = new ElectricityUsage[12] ;

        public Room(string roomNumber) 
        {
            number = roomNumber;
            for(int i = 0; i <= 11; i++)
            {
                waterUsageMonths[i] = new WaterUsage(0, 0.0);
                electricityUsageMonths[i] = new ElectricityUsage(0, 0.0);
            }
        }
        public void Register(int month, Usage u)
        {
            if (u is WaterUsage)
            {
                waterUsageMonths[month] = (WaterUsage)u;
            }
            if (u is ElectricityUsage)
            {
                electricityUsageMonths[month]= (ElectricityUsage)u;
            }            
        }
        public void Status(int month)
        {
                Console.WriteLine(Number + "호실 " + (month + 1) + "월 전기 사용량 : " + electricityUsageMonths[month].UsageAmount);
                Console.WriteLine(Number + "호실 " + (month + 1) + "월 전기 요금 : " + electricityUsageMonths[month].CalculateCost());
                Console.WriteLine(Number + "호실 " + (month + 1) + "월 수도 사용량 : " + waterUsageMonths[month].UsageAmount);
                Console.WriteLine(Number + "호실 " + (month + 1) + "월 수도 요금 : " + waterUsageMonths[month].CalculateCost());
            
        }
        public void AllSatus()
        {            
            for (int i = 0; i <= 11; i++)
            {
                Console.WriteLine(Number + "호실 " + (i + 1) +"월 전기 사용량 : " + electricityUsageMonths[i].UsageAmount);
                Console.WriteLine(Number + "호실 " + (i + 1) + "월 전기 요금 : " + electricityUsageMonths[i].CalculateCost());
                Console.WriteLine(Number + "호실 " + (i + 1) + "월 수도 사용량 : " + waterUsageMonths[i].UsageAmount);
                Console.WriteLine(Number + "호실 " + (i + 1) + "월 수도 요금 : " + waterUsageMonths[i].CalculateCost());
            }
        }
    }
    internal class Program
    {
        static List<Room> rooms = new List<Room>();
        static double unitElectricityPrice = 250.0;//1kWh당
        static double unitWaterPrice = 0.6;//1㎥당
        static void Main(string[] args)
        {
            //미리 호실 정보 생성
            rooms.Add(new Room("101"));
            rooms.Add(new Room("102"));
            rooms.Add(new Room("103"));
            rooms.Add(new Room("201"));
            rooms.Add(new Room("202"));
            rooms.Add(new Room("203"));
            

            while (true) {
                Console.WriteLine("\n번호를 선택해 주세요. ");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("1. 단가 입력하기");
                Console.WriteLine("2. 호실별 사용량 입력하기");
                Console.WriteLine("3. 호실별 상황 검색하기");
                Console.WriteLine("4. 종료하기");
                Console.WriteLine("-------------------------------");
                Console.Write(": ");

                var sel = Console.ReadLine().Trim();

                switch (sel) {
                    case "1":
                        EnterUnitPrice();
                        break;
                    case "2":
                        EnterUsage();
                        break;
                    case "3":
                        ShowStatus();
                        break;                    
                    case "4":
                        Console.WriteLine("안녕히 가십시요.");
                        return;
                }
            }
        }
        private static void EnterUnitPrice()
        {
            //unitElectricityPrice, unitWaterPrice 단가 변경시 사용한다.
            //1kWh당의 전기요금과
            //1㎥당의 수도요금을 입력 받는다.
            
            Console.WriteLine("현재 1kWh당 전기요금은 " + unitElectricityPrice+"원");
            Console.WriteLine("현재 1㎥당 수도요금은 " + unitWaterPrice + "원");
            Console.Write("변경하시겠습니까?(y/n) : ");
            string ans = Console.ReadLine();
            if (ans == "y")
            {
                Console.Write("변경 1kWh당 전기요금 : ");
                unitElectricityPrice = int.Parse(Console.ReadLine());
                Console.Write("변경 1㎥당 수도요금 : ");
                unitWaterPrice = double.Parse(Console.ReadLine());
                Console.WriteLine("변경된 1kWh당 전기요금은 " + unitElectricityPrice + "원");
                Console.WriteLine("변경된 1㎥당 수도요금은 " + unitWaterPrice + "원");
            }            
        }
        
        private static void EnterUsage()
        {
            //호실을 입력 받아서, 호실이 있는 경우에만
            //원하는 월(1-12)을 입력 받는다.
            //전기 사용량을 입력한다.
            //입력한 정보로 ElectricityUsage를 생성하고 Room의 Register를 이용해 등록한다.
            //수도사용량을 입력한다.
            //입력한 정보로 WaterUsage 생성하고 Room의 Register를 이용해 등록한다.

            string roomNumber;
            Console.Write("\n호실번호를 입력해 주세요 : ");
            roomNumber = Console.ReadLine();
            int month = 0;
            int eAmount = 0;
            int wAmount = 0;
            ElectricityUsage eu;
            WaterUsage wu;
            foreach (var room in rooms)
            {
                if(room.Number == roomNumber)
                {
                    Console.Write("전기와 수도 사용량 입력을 원하는 월을 입력 하세요 : ");
                    month = int.Parse(Console.ReadLine());
                    Console.Write("전기사용량을 입력하세요 : ");
                    eAmount = int.Parse(Console.ReadLine());
                    Console.Write("수도사용량을 입력하세요 : ");
                    wAmount = int.Parse(Console.ReadLine());
                    eu = new ElectricityUsage(eAmount, unitElectricityPrice);
                    room.Register(month-1, eu);
                    wu = new WaterUsage(wAmount, unitWaterPrice);
                    room.Register(month-1, wu);
                    return;
                }
            }
            Console.Write(roomNumber + "호실은 없는 호실입니다. 다시 한번 더 확인해 주시기 바랍니다. ");
        }

        private static void ShowStatus()
        {
            //호실을 입력 받아서, 호실이 있는 경우에만
            //원하는 월(1-12)이나 전체(0)를 선택해
            //해당 월만 정보를 출력하거나 전체 정보를 출력한다.            
            string roomNumber;
            Console.Write("호실번호를 입력해 주세요 : ");
            roomNumber = Console.ReadLine();
            foreach (var room in rooms)
            {
                if (room.Number == roomNumber)
                {
                    Console.Write(roomNumber + "호실의 전기요금과 수도요금을 알고 싶은 월을 입력하세요(전체는 0) : ");
                    int month = int.Parse(Console.ReadLine());
                    if (month == 0)
                        room.AllSatus();
                    else                    
                        room.Status(month - 1);

                    return;
                }
            }
            Console.Write(roomNumber + "호실은 없는 호실입니다. 다시 한번 더 확인해 주시기 바랍니다. ");
        }
    }
}
