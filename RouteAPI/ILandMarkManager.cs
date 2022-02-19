using GrapgDS;
using RouteAPI.Entities;

namespace RouteAPI
{
    public interface ILandMarkManager
    {
        Landmark RegisterLandMark(string name);

        Landmark RemoveALandMark(string name);

    }
}