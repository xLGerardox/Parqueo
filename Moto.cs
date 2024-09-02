using System;

public class Moto : Vehiculo
{
    public int Cilindrada { get; set; }

    public Moto(string placa, string marca, string modelo, string color, int cilindrada)
        : base(placa, marca, modelo, color)
    {
        Cilindrada = cilindrada;
    }

    public override double CalcularCostoEstacionamiento(TimeSpan tiempoEstacionado)
    {
        return Math.Ceiling(tiempoEstacionado.TotalHours) * 10;
    }
}
