using System.Collections.Generic;

namespace ValveMultitool.Models.Formats
{
    public interface IElement
    {
        IList<IElement> Children { get; }
    }
}
