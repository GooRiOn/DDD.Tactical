using System.Runtime.InteropServices.ComTypes;
using DevMedical.Exceptions;

namespace DevMedical.Entities;

public class HomeVisit
{    
    public Guid Id { get; }
    public DateTimeOffset VisitDateTime { get; private set; }
    public HomeVisitStatus Status { get; private set; }
    
    public string City { get; private set; }
    public string Street { get; private set; }
    public string ZipCode { get; private set; }

    private HomeVisit(Guid id)
        => Id = id;

    public HomeVisit(Guid id, DateTimeOffset visitDateTime, 
        HomeVisitStatus status, string city, 
        string street, string zipCode)
    {
        Id = id;
        VisitDateTime = visitDateTime;
        Status = status;
        City = city;
        Street = street;
        ZipCode = zipCode;
    }

    public static HomeVisit Create(Guid id, DateTimeOffset visitDateTime, 
         string city, string street, string zipCode)
    {
        var homeVisit = new HomeVisit(id)
        {
            Status = HomeVisitStatus.Pending
        };

        homeVisit.SetVisitDateTime(visitDateTime);
        homeVisit.ChangeAddress(city, street, zipCode);

        return homeVisit;
    }

    public void Reschedule(DateTimeOffset newVisitDateTime)
    {
        if (Status is HomeVisitStatus.Canceled or HomeVisitStatus.Completed)
        {
            throw new DevMedicalException("Cannot reschedule visit.");
        }

        SetVisitDateTime(newVisitDateTime);
        Status = HomeVisitStatus.Rescheduled;
    }

    public void ChangeAddress(string city, string street, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new DevMedicalException("City cannot be empty.");
        }
        if (string.IsNullOrWhiteSpace(street))
        {
            throw new DevMedicalException("Street cannot be empty.");
        }
        if (string.IsNullOrWhiteSpace(zipCode))
        {
            throw new DevMedicalException("Zipcode cannot be empty.");
        }
        
        City = city;
        Street = street;
        ZipCode = zipCode;
    }

    private void SetVisitDateTime(DateTimeOffset visitDateTime)
    {
        if (visitDateTime <= DateTimeOffset.Now)
        {
            throw new DevMedicalException("Cannot set new visit date time to past.");
        }

        VisitDateTime = visitDateTime;
    }
}