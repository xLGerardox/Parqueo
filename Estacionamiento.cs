using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Estacionamiento
{
    private List<Vehiculo> ListaVehiculos;
    private int EspaciosDisponibles;

    public Estacionamiento(int espaciosDisponibles)
    {
        EspaciosDisponibles = espaciosDisponibles;
        ListaVehiculos = new List<Vehiculo>();
    }

    public void RegistrarVehiculo()
    {
        if (EspaciosDisponibles <= 0)
        {
            Console.WriteLine("No hay espacios disponibles en el estacionamiento.");
            return;
        }

        try
        {
            Console.WriteLine("Ingrese el tipo de vehículo (1- Carro, 2- Moto, 3- Camión): ");
            int tipoVehiculo = int.Parse(Console.ReadLine());

            Console.Write("Ingrese la placa del vehículo: ");
            string placa = Console.ReadLine();
            Console.Write("Ingrese la marca del vehículo: ");
            string marca = Console.ReadLine();
            Console.Write("Ingrese el modelo del vehículo: ");
            string modelo = Console.ReadLine();
            Console.Write("Ingrese el color del vehículo: ");
            string color = Console.ReadLine();
            if (Regex.IsMatch(color, @"\d"))
            {
                throw new ArgumentException("El color no puede contener números.");
            }

            switch (tipoVehiculo)
            {
                case 1:
                    Console.Write("Ingrese el número de puertas del carro: ");
                    int numeroDePuertas = int.Parse(Console.ReadLine());
                    ListaVehiculos.Add(new Carro(placa, marca, modelo, color, numeroDePuertas));
                    break;
                case 2:
                    Console.Write("Ingrese la cilindrada de la moto: ");
                    int cilindrada = int.Parse(Console.ReadLine());
                    ListaVehiculos.Add(new Moto(placa, marca, modelo, color, cilindrada));
                    break;
                case 3:
                    Console.Write("Ingrese la capacidad de carga del camión: ");
                    double capacidadCarga = double.Parse(Console.ReadLine());
                    ListaVehiculos.Add(new Camion(placa, marca, modelo, color, capacidadCarga));
                    break;
                default:
                    Console.WriteLine("Tipo de vehículo no válido.");
                    return;
            }

            EspaciosDisponibles--;
            Console.WriteLine("Vehículo registrado exitosamente.");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Error de formato: {ex.Message}. Por favor, ingrese un valor válido.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error en la entrada: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    public void RetirarVehiculo()
    {
        try
        {
            Console.Write("Ingrese la placa del vehículo a retirar: ");
            string placa = Console.ReadLine();
            Vehiculo vehiculo = ListaVehiculos.Find(v => v.Placa == placa);

            if (vehiculo == null)
            {
                Console.WriteLine("Vehículo no encontrado.");
                return;
            }

            TimeSpan tiempoEstacionado = DateTime.Now - vehiculo.HoraEntrada;
            double costoTotal = vehiculo.CalcularCostoEstacionamiento(tiempoEstacionado);

            Console.WriteLine($"Tiempo estacionado: {Math.Ceiling(tiempoEstacionado.TotalHours)} horas");
            Console.WriteLine($"Costo total a pagar: Q{costoTotal}");

            Console.WriteLine("Seleccione el método de pago (1- Efectivo, 2- Tarjeta): ");
            int metodoPago = int.Parse(Console.ReadLine());

            if (metodoPago == 1)
            {
                ProcesarPagoEfectivo(costoTotal);
            }
            else if (metodoPago == 2)
            {
                if (ProcesarPagoTarjeta(costoTotal))
                {
                    ListaVehiculos.Remove(vehiculo);
                    EspaciosDisponibles++;
                    Console.WriteLine("Vehículo retirado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Error en el procesamiento del pago con tarjeta. Intente nuevamente.");
                }
            }
            else
            {
                Console.WriteLine("Método de pago no válido.");
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Error de formato: {ex.Message}. Por favor, ingrese un valor válido.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    private void ProcesarPagoEfectivo(double costoTotal)
    {
        try
        {
            Console.Write("Ingrese el monto entregado por el cliente: ");
            double montoEntregado = double.Parse(Console.ReadLine());

            if (montoEntregado < costoTotal)
            {
                Console.WriteLine("Monto insuficiente, se requiere un monto adicional.");
                ProcesarPagoEfectivo(costoTotal);
            }
            else
            {
                double cambio = montoEntregado - costoTotal;
                Console.WriteLine($"Cambio a devolver: Q{cambio}");
                MostrarDesgloseCambio(cambio);
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Error de formato: {ex.Message}. Por favor, ingrese un valor válido.");
        }
    }

    private void MostrarDesgloseCambio(double cambio)
    {
        int[] billetes = { 200, 100, 50, 20, 10, 5, 1 };
        Console.WriteLine("Desglose del cambio:");

        foreach (int billete in billetes)
        {
            int cantidadBilletes = (int)(cambio / billete);
            if (cantidadBilletes > 0)
            {
                Console.WriteLine($"{cantidadBilletes} billete(s) de Q{billete}");
                cambio -= cantidadBilletes * billete;
            }
        }
    }

    private bool ProcesarPagoTarjeta(double costoTotal)
    {
        try
        {
            Console.Write("Ingrese el número de tarjeta (16 dígitos): ");
            string numeroTarjeta = Console.ReadLine();

            if (!Regex.IsMatch(numeroTarjeta, @"^\d{16}$"))
            {
                throw new ArgumentException("Número de tarjeta inválido.");
            }

            Console.Write("Ingrese el nombre del titular de la tarjeta: ");
            string nombreTitular = Console.ReadLine();

            Console.Write("Ingrese la fecha de vencimiento (MM/AA): ");
            string fechaVencimiento = Console.ReadLine();

            if (!Regex.IsMatch(fechaVencimiento, @"^(0[1-9]|1[0-2])\/\d{2}$"))
            {
                throw new ArgumentException("Fecha de vencimiento inválida.");
            }

            Console.Write("Ingrese el CVV (3 o 4 dígitos): ");
            string cvv = Console.ReadLine();

            if (!Regex.IsMatch(cvv, @"^\d{3,4}$"))
            {
                throw new ArgumentException("CVV inválido.");
            }

            // Simulación de la aprobación del pago
            Console.WriteLine("Procesando pago...");
            System.Threading.Thread.Sleep(2000);  // Simular tiempo de procesamiento
            Console.WriteLine("Pago aprobado exitosamente.");

            return true;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error en la entrada: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
            return false;
        }
    }

    public void MostrarVehiculosEstacionados()
    {
        if (ListaVehiculos.Count == 0)
        {
            Console.WriteLine("No hay vehículos estacionados.");
        }
        else
        {
            Console.WriteLine("Vehículos estacionados:");
            foreach (var vehiculo in ListaVehiculos)
            {
                Console.WriteLine($"Placa: {vehiculo.Placa}, Marca: {vehiculo.Marca}, Modelo: {vehiculo.Modelo}, Color: {vehiculo.Color}, Hora de entrada: {vehiculo.HoraEntrada}");
            }
        }
    }

    public void MostrarEspaciosDisponibles()
    {
        Console.WriteLine($"Espacios disponibles: {EspaciosDisponibles}");
    }
}
