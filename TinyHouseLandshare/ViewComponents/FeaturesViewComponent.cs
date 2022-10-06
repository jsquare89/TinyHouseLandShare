using Microsoft.AspNetCore.Mvc;
using TinyHouseLandshare.Models;
using TinyHouseLandshare.Services;
using TinyHouseLandshare.ViewModels;

namespace TinyHouseLandshare.ViewComponents;

public class FeaturesViewComponent : ViewComponent
{

    public FeaturesViewComponent()
    {
    }

    public IViewComponentResult Invoke(bool waterConnection,
                                       bool electricalConnection,
                                       bool wifiConnection,
                                       bool parking,
                                       bool petFriendly,
                                       bool childFriendly,
                                       bool isPrivate,
                                       bool NoSmoking)
    {
        FeaturesComponentViewModel featuresComponentVM = new();
        featuresComponentVM.WaterConnection = waterConnection;
        featuresComponentVM.ElectricalConnection = electricalConnection;
        featuresComponentVM.WifiConnection = wifiConnection;
        featuresComponentVM.Parking = parking;
        featuresComponentVM.PetFriendly = petFriendly;
        featuresComponentVM.ChildFriendly = childFriendly;
        featuresComponentVM.Private = isPrivate;
        featuresComponentVM.NoSmoking = NoSmoking;

        return View(featuresComponentVM);
    }
}