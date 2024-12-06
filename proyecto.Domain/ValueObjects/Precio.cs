namespace Proyecto.Domain.ValueObjects;

public class Precio
    {
        public decimal Valor { get; private set; }

        public Precio(decimal valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("El precio no puede ser negativo");
            }
            Valor = valor;
        }
    }