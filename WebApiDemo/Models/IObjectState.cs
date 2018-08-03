using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiDemo.Models
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState State { get; set; }
    }

    public abstract class EntityBase : IObjectState
    {
        [NotMapped]
        public ObjectState State { get; set; }
    }

}
