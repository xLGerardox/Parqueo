﻿using System;
using System.Collections.Generic;

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

    public void RetirarVehiculo()
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
            ProcesarPagoTarjeta();
        }
        else
        {
            Console.WriteLine("Método de pago no válido.");
            return;
        }

        ListaVehiculos.Remove(vehiculo);
        EspaciosDisponibles++;
        Console.WriteLine("Vehículo retirado exitosamente.");
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

    private void ProcesarPagoEfectivo(double costoTotal)
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

    private void ProcesarPagoTarjeta()
    {
        Console.Write("Ingrese el número de tarjeta (16 dígitos): ");
        string numeroTarjeta = Console.ReadLine();
        Console.Write("Ingrese el nombre del titular de la tarjeta: ");
        string nombreTitular = Console.ReadLine();
        Console.Write("Ingrese la fecha de vencimiento (MM/AA): ");
        string fechaVencimiento = Console.ReadLine();
        Console.Write("Ingrese el CVV (3 o 4 dígitos): ");
        string cvv = Console.ReadLine();

        // validación del pago acá...(NI IDEA COMO SACARLO) 
        // PD. Pensa gerardooooooo

        Console.WriteLine("Pago procesado exitosamente.");
    }
}
