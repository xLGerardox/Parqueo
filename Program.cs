using System;

public class Programa
{
    static void Main(string[] args)
    {
        Estacionamiento estacionamiento = new Estacionamiento(20);
        int opcion = 0;

        do
        {
            try
            {
                Console.Clear();
                Console.WriteLine("SISTEMA DE GESTIÓN DE ESTACIONAMIENTO");
                Console.WriteLine("1. Registrar un nuevo vehículo");
                Console.WriteLine("2. Retirar un vehículo");
                Console.WriteLine("3. Ver vehículos estacionados");
                Console.WriteLine("4. Ver espacios disponibles");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        estacionamiento.RegistrarVehiculo();
                        break;
                    case 2:
                        estacionamiento.RetirarVehiculo();
                        break;
                    case 3:
                        estacionamiento.MostrarVehiculosEstacionados();
                        break;
                    case 4:
                        estacionamiento.MostrarEspaciosDisponibles();
                        break;
                    case 5:
                        Console.WriteLine("Gracias por usar el sistema.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }

                if (opcion != 5)
                {
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error de formato: {ex.Message}. Por favor, ingrese un número válido.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
            }

        } while (opcion != 5);
    }
}
