using MediatR;

namespace Modules.Products.Application.Commands.ChangeProductWeight
{
    public class ChangeProductWeightCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public decimal WeightPerUnit { get; set; }        
    }
}
