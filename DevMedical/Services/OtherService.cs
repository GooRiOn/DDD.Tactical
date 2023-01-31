using DevMedical.Entities;

namespace DevMedical.Services;

public class OtherService
{
    public void Create(DateTimeOffset visitDateTime)
    {
        if (visitDateTime <= DateTimeOffset.Now)
        {
            //error
        }
        
        var homeVisit = new HomeVisit
        {
            Status = HomeVisitStatus.Pending
        };
        
        // other params here
    }
}