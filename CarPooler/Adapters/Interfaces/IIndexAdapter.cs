using CarPooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPooler.Adapters.Interfaces
{
    public interface IIndexAdapter
    {
        List<DriverJourneyViewModel> GetDestinations(string depCity,string destination);
    }
}
