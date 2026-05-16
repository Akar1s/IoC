using System;

namespace IoC_Containers
{
    public interface IRequestContext
    {
        Guid RequestId { get; }
    }

    public class RequestContext : IRequestContext
    {
        public Guid RequestId { get; } = Guid.NewGuid();
        public RequestContext() { }
    }
}