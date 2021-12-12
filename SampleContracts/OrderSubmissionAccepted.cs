using System;
namespace SampleContracts
{
    public interface OrderSubmissionAccepted
    {
        Guid OrderId { get; }
        DateTime Timestamp { get; }
        string CustomerNumber { get; }
    }
}
