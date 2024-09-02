using System;

public class Camion : Vehiculo
{
    public double CapacidadCarga { get; set; }

    public Camion(string placa, string marca, string modelo, string color, double capacidadCarga)
        : base(placa, marca, modelo, color)
    {
        CapacidadCarga = capacidadCarga;
    }

    public override double CalcularCostoEstacionamiento(TimeSpan tiempoEstacionado)
    {
        return Math.Ceiling(tiempoEstacionado.TotalHours) * 20;
    }
}
