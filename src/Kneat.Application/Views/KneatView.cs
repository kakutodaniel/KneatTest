using Kneat.Application.Contracts;
using System.Collections.Generic;

namespace Kneat.Application.Views
{
    public class KneatView : KneatBaseResponse
    {

        public List<KneatViewItem> Items { get; set; } = new List<KneatViewItem>();

    }
}
