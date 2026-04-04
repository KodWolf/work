using System;
using System.Collections.Generic;

// ================== FACADE ==================

public class RoomBookingSystem
{
    private Dictionary<int, bool> rooms = new Dictionary<int, bool>();

    public RoomBookingSystem()
    {
        for (int i = 100; i <= 120; i++)
            rooms[i] = true;
    }

    public bool CheckAvailability(int roomNumber)
    {
        return rooms.ContainsKey(roomNumber) && rooms[roomNumber];
    }

    public bool BookRoom(int roomNumber)
    {
        if (CheckAvailability(roomNumber))
        {
            rooms[roomNumber] = false;
            Console.WriteLine("Room " + roomNumber + " booked.");
            return true;
        }
        Console.WriteLine("Room " + roomNumber + " is not available.");
        return false;
    }

    public bool CancelBooking(int roomNumber)
    {
        if (rooms.ContainsKey(roomNumber) && !rooms[roomNumber])
        {
            rooms[roomNumber] = true;
            Console.WriteLine("Booking cancelled for room " + roomNumber);
            return true;
        }
        Console.WriteLine("No booking found for room " + roomNumber);
        return false;
    }
}

public class RestaurantSystem
{
    public void BookTable(int people, DateTime time)
    {
        Console.WriteLine("Table booked for " + people + " at " + time);
    }

    public void OrderFood(string dish)
    {
        Console.WriteLine("Food ordered: " + dish);
    }

    public void OrderTaxi(string address)
    {
        Console.WriteLine("Taxi ordered to: " + address);
    }
}

public class EventManagementSystem
{
    private List<string> halls = new List<string>()
    {
        "Hall A", "Hall B", "Ballroom"
    };

    public bool BookHall(string hall)
    {
        if (halls.Contains(hall))
        {
            halls.Remove(hall);
            Console.WriteLine("Hall booked: " + hall);
            return true;
        }
        Console.WriteLine("Hall not available: " + hall);
        return false;
    }

    public void OrderEquipment(string eq)
    {
        Console.WriteLine("Equipment ordered: " + eq);
    }
}

public class CleaningService
{
    public void ScheduleCleaning(int room, DateTime time)
    {
        Console.WriteLine("Cleaning scheduled for room " + room + " at " + time);
    }

    public void CleanNow(int room)
    {
        Console.WriteLine("Cleaning done for room " + room);
    }
}

// ===== FACADE =====

public class HotelFacade
{
    private RoomBookingSystem rooms = new RoomBookingSystem();
    private RestaurantSystem restaurant = new RestaurantSystem();
    private EventManagementSystem events = new EventManagementSystem();
    private CleaningService cleaning = new CleaningService();

    public void BookRoomWithServices(int room, string food, DateTime cleanTime)
    {
        Console.WriteLine("\n--- Booking Room ---");

        if (rooms.BookRoom(room))
        {
            restaurant.OrderFood(food);
            cleaning.ScheduleCleaning(room, cleanTime);
        }
    }

    public void OrganizeEvent(string hall, string equipment, int[] roomList)
    {
        Console.WriteLine("\n--- Event ---");

        if (events.BookHall(hall))
        {
            events.OrderEquipment(equipment);

            for (int i = 0; i < roomList.Length; i++)
            {
                rooms.BookRoom(roomList[i]);
            }
        }
    }

    public void BookTableWithTaxi(int people, DateTime time, string address)
    {
        Console.WriteLine("\n--- Restaurant ---");

        restaurant.BookTable(people, time);
        restaurant.OrderTaxi(address);
    }

    public void CancelRoom(int room)
    {
        Console.WriteLine("\n--- Cancel ---");
        rooms.CancelBooking(room);
    }

    public void RequestCleaning(int room)
    {
        Console.WriteLine("\n--- Cleaning ---");
        cleaning.CleanNow(room);
    }
}

// ================== COMPOSITE ==================

public abstract class OrganizationComponent
{
    protected string name;

    public OrganizationComponent(string name)
    {
        this.name = name;
    }

    public abstract decimal GetBudget();
    public abstract int GetEmployees();
    public abstract void Show(int level);
}

// ===== EMPLOYEE =====

public class Employee : OrganizationComponent
{
    private string position;
    private decimal salary;
    private bool contractor;

    public Employee(string name, string position, decimal salary, bool contractor = false)
        : base(name)
    {
        this.position = position;
        this.salary = salary;
        this.contractor = contractor;
    }

    public void ChangeSalary(decimal newSalary)
    {
        salary = newSalary;
    }

    public override decimal GetBudget()
    {
        if (contractor) return 0;
        return salary;
    }

    public override int GetEmployees()
    {
        return 1;
    }

    public override void Show(int level)
    {
        Console.WriteLine(new string('-', level) + name + " (" + position + ") $" + salary);
    }
}

// ===== DEPARTMENT =====

public class Department : OrganizationComponent
{
    private List<OrganizationComponent> list = new List<OrganizationComponent>();

    public Department(string name) : base(name) { }

    public void Add(OrganizationComponent c)
    {
        list.Add(c);
    }

    public override decimal GetBudget()
    {
        decimal sum = 0;

        for (int i = 0; i < list.Count; i++)
        {
            sum += list[i].GetBudget();
        }

        return sum;
    }

    public override int GetEmployees()
    {
        int count = 0;

        for (int i = 0; i < list.Count; i++)
        {
            count += list[i].GetEmployees();
        }

        return count;
    }

    public override void Show(int level)
    {
        Console.WriteLine(new string('-', level) + name);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].Show(level + 2);
        }
    }
}

// ================== MAIN ==================

class Program
{
    static void Main()
    {
        HotelFacade hotel = new HotelFacade();

        hotel.BookRoomWithServices(101, "Pizza", DateTime.Now.AddHours(2));
        hotel.BookTableWithTaxi(3, DateTime.Now.AddHours(1), "Almaty center");
        hotel.OrganizeEvent("Hall A", "Projector", new int[] { 102, 103 });
        hotel.CancelRoom(101);
        hotel.RequestCleaning(102);

        Console.WriteLine("\n=== COMPANY ===");

        Employee e1 = new Employee("John", "Dev", 70000);
        Employee e2 = new Employee("Anna", "Manager", 90000);
        Employee e3 = new Employee("Mike", "Designer", 50000, true);

        Department dev = new Department("Development");
        dev.Add(e1);

        Department company = new Department("Company");
        company.Add(dev);
        company.Add(e2);
        company.Add(e3);

        company.Show(0);

        Console.WriteLine("Budget: " + company.GetBudget());
        Console.WriteLine("Employees: " + company.GetEmployees());

        Console.ReadLine();
    }
}