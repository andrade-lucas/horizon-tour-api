using Horizon.Domain.Enums;
using Horizon.Domain.ValueObjects;
using Horizon.Shared.Entities;
using Horizon.Shared.Messages;

namespace Horizon.Domain.Entities;

public class Place : Entity
{
    public string Name { get; private set; }
    public User? Owner { get; private set; }
    public EPlaceStatus Status { get; private set; }
    public Address? Address { get; private set; }
    public string? PresentationImageUrl { get; private set; }
    public EAutomaticOpen? AutomaticOpen { get; private set; }
    public bool? IsOpen { get; private set; }
    public string? Description { get; private set; }
    public EPlaceType? Type { get; private set; }

    public Place(string name, EPlaceStatus status, bool isOpen = false)
    {
        Name = name;
        Status = status;
        IsOpen = isOpen;
    }

    public Place(string id, string name, EPlaceStatus status, bool isOpen = false, DateTime? createdAt = null, DateTime? updatedAt = null) 
        : base(id, createdAt, updatedAt)
    {
        Name = name;
        Status = status;
        IsOpen = isOpen;
    }

    public void AddOwner(User owner) => Owner = owner;

    public void AddAddress(Address address) => Address = address;

    public void AddPresentationImageUrl(string? presentationImageUrl) => PresentationImageUrl = presentationImageUrl;

    public void AddAutomaticOpen(EAutomaticOpen automaticOpen) => AutomaticOpen = automaticOpen;

    public void AddDescription(string description) => Description = description;

    public void AddType(EPlaceType type) => Type = type;

    public bool CanPublish()
    {
        bool canPublish = false;

        if (Type != null && Type != EPlaceType.None && Status != EPlaceStatus.Published)
            canPublish = true;

        return canPublish;
    }

    public void Publish()
    {
        if (Status != EPlaceStatus.Published)
            Status = EPlaceStatus.Published;
    }

    public IDictionary<string, string> ErrorsOnPublish()
    {
        var errors = new Dictionary<string, string>();

        if (Type == null || Type == EPlaceType.None) errors.Add("Type", string.Format(Messages.InvalidField, "Tipo"));

        return errors;
    }
}
