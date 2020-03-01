namespace Kneat.Application.Contracts.External
{
    public class GetStarShipResponseItem
    {

        public string Name { get; set; }

        public string MGLT { get; set; }
        
        //hour - hours
        //day - days
        //week - weeks
        //month - months
        //year - years
        public string Consumables { get; set; }
    }
}
