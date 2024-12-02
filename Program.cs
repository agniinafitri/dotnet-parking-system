using System;
using System.Collections.Generic;

class Program
{
    static List<ParkingLot> parkingLots = new List<ParkingLot>();
    static int totalSlots;

    static void Main(string[] args)
    {
        Console.WriteLine("Selamat datang di Sistem Parkir!");
        while (true)
        {
            Console.WriteLine("\nMasukkan perintah:");
            string? command = Console.ReadLine();
            if (string.IsNullOrEmpty(command)) continue;

            string[] inputs = command.Split(' ');
            string action = inputs[0].ToLower();

            switch (action)
            {
                case "create_parking_lot":
                    CreateParkingLot(Convert.ToInt32(inputs[1]));
                    break;
                case "park":
                    ParkVehicle(inputs[1], inputs[2], inputs[3]);
                    break;
                case "leave":
                    LeaveParking(Convert.ToInt32(inputs[1]));
                    break;
                case "status":
                    ShowStatus();
                    break;
                case "type_of_vehicles":
                    CountVehiclesByType(inputs[1]);
                    break;
                case "registration_numbers_for_vehicles_with_colour":
                    VehiclesByColour(inputs[1]);
                    break;
                case "slot_number_for_registration_number":
                    SlotByRegistration(inputs[1]);
                    break;
                case "exit":
                    Console.WriteLine("Terima kasih telah menggunakan sistem parkir!");
                    return;
                default:
                    Console.WriteLine("Perintah tidak dikenali!");
                    break;
            }
        }
    }

    static void CreateParkingLot(int slots)
    {
        totalSlots = slots;
        parkingLots.Clear();
        for (int i = 0; i < slots; i++)
        {
            parkingLots.Add(new ParkingLot { SlotNumber = i + 1, IsOccupied = false, Vehicle = null });
        }
        Console.WriteLine($"Created a parking lot with {slots} slots");
    }

    static void ParkVehicle(string regNumber, string color, string type)
    {
        for (int i = 0; i < parkingLots.Count; i++)
        {
            if (!parkingLots[i].IsOccupied)
            {
                parkingLots[i].Vehicle = new Vehicle { RegistrationNumber = regNumber, Colour = color, Type = type };
                parkingLots[i].IsOccupied = true;
                Console.WriteLine($"Allocated slot number: {parkingLots[i].SlotNumber}");
                return;
            }
        }
        Console.WriteLine("Sorry, parking lot is full");
    }

    static void LeaveParking(int slotNumber)
    {
        if (slotNumber <= totalSlots && parkingLots[slotNumber - 1].IsOccupied)
        {
            parkingLots[slotNumber - 1].IsOccupied = false;
            parkingLots[slotNumber - 1].Vehicle = null;
            Console.WriteLine($"Slot number {slotNumber} is free");
        }
        else
        {
            Console.WriteLine("Slot tersebut kosong atau tidak valid!");
        }
    }

    static void ShowStatus()
    {
        Console.WriteLine("Slot\tNo.\tType\tRegistration No\tColour");
        foreach (var lot in parkingLots)
        {
            if (lot.IsOccupied && lot.Vehicle != null)
            {
                Console.WriteLine($"{lot.SlotNumber}\t{lot.Vehicle.RegistrationNumber}\t{lot.Vehicle.Type}\t{lot.Vehicle.Colour}");
            }
        }
    }

    static void CountVehiclesByType(string type)
    {
        int count = 0;
        foreach (var lot in parkingLots)
        {
            if (lot.IsOccupied && lot.Vehicle?.Type.Equals(type, StringComparison.OrdinalIgnoreCase) == true)
            {
                count++;
            }
        }
        Console.WriteLine(count);
    }

    static void VehiclesByColour(string color)
    {
        List<string> vehicles = new List<string>();
        foreach (var lot in parkingLots)
        {
            if (lot.IsOccupied && lot.Vehicle?.Colour.Equals(color, StringComparison.OrdinalIgnoreCase) == true)
            {
                vehicles.Add(lot.Vehicle.RegistrationNumber);
            }
        }
        Console.WriteLine(string.Join(", ", vehicles));
    }

    static void SlotByRegistration(string regNumber)
    {
        foreach (var lot in parkingLots)
        {
            if (lot.IsOccupied && lot.Vehicle?.RegistrationNumber.Equals(regNumber, StringComparison.OrdinalIgnoreCase) == true)
            {
                Console.WriteLine(lot.SlotNumber);
                return;
            }
        }
        Console.WriteLine("Not found");
    }
}

class ParkingLot
{
    public int SlotNumber { get; set; }
    public bool IsOccupied { get; set; }
    public Vehicle? Vehicle { get; set; } // Nullable untuk menghindari error
}

class Vehicle
{
    public string RegistrationNumber { get; set; } = string.Empty; // Default value
    public string Colour { get; set; } = string.Empty;            // Default value
    public string Type { get; set; } = string.Empty;              // Default value
}
