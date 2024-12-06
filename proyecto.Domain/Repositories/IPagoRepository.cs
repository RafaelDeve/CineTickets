namespace Proyecto.Domain.Repositories
{
    using Proyecto.Domain.Entities;
    public interface IPagoRepository
    {
        Pago? ObtenerPagoPorId(int pagoId);
        void AgregarPago(Pago pago);
    }
}

