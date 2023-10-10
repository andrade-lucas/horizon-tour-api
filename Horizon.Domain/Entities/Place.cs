﻿using Horizon.Domain.Enums;
using Horizon.Domain.ValueObjects;
using Horizon.Shared.Entities;

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

    public void AddOwner(User owner) => Owner = owner;

    public void AddAddress(Address address) => Address = address;

    public void AddPresentationImageUrl(string? presentationImageUrl) => PresentationImageUrl = presentationImageUrl;

    public void AddAutomaticOpen(EAutomaticOpen automaticOpen) => AutomaticOpen = automaticOpen;

    public void AddDescription(string description) => Description = description;

    public void AddType(EPlaceType type) => Type = type;
}
