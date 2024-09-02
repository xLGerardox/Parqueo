using System;

public class Carro : Vehiculo
{
    public int NumeroDePuertas { get; set; }

    public Carro(string placa, string marca, string modelo, string color, int numeroDePuertas)
        : base(placa, marca, modelo, color)
    {
        NumeroDePuertas = numeroDePuertas;
    }

    public override double CalcularCostoEstacionamiento(TimeSpan tiempoEstacionado)
    {
        return Math.Ceiling(tiempoEstacionado.TotalHours) * 15;
    }
}
