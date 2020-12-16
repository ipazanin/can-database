using System;

namespace CanDatabase.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string typeName, object id)
            : base($"{typeName} with id: {id} was not found!")
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }
    }
}
