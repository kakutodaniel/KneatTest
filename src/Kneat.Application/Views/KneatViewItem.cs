using System.Diagnostics;

namespace Kneat.Application.Views
{
    [DebuggerDisplay("Name = {Name} - NumberStops = {NumberStops}")]
    public class KneatViewItem
    {

        public string Name { get; set; }

        public string NumberStops { get; set; }

    }
}
