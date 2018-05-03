using System;

namespace Sau.Mackey
{
    /// <summary>
    /// A database entity for the Mackey application
    /// </summary>
    public interface IMackeyEntity
    {
        Guid DbId { get; set; }
    }
}
