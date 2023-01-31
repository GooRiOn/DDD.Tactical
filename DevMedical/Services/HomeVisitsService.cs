using DevMedical.Entities;

namespace DevMedical.Services;

public class HomeVisitsService
{
    public void CreateAndReschedule()
    {
        var visitDateTime = DateTimeOffset.Now.AddDays(2);
        var id = Guid.NewGuid();
        var homeVisit =  HomeVisit.Create(id, visitDateTime, 
            "Warsaw", "JP2", "01-123");
        
        homeVisit.Reschedule(visitDateTime);
        // other params here
    }
}