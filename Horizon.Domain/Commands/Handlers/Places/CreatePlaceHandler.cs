using FluentValidation;
using Horizon.Domain.Commands.Inputs.Places;
using Horizon.Domain.Entities;
using Horizon.Domain.Repositories;
using Horizon.Domain.Services;
using Horizon.Domain.ValueObjects;
using Horizon.Shared.Commands;
using Horizon.Shared.Outputs;
using System.Net;

namespace Horizon.Domain.Commands.Handlers.Places;

public class CreatePlaceHandler : ICommandHandler<CreatePlaceCommand>
{
    private readonly IPlaceRepository _placeRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUploadImageService _uploadImageService;
    private readonly IValidator<Place> _placeValidator;

    public CreatePlaceHandler(
        IPlaceRepository placeRepository,
        ICityRepository cityRepository,
        IUserRepository userRepository,
        IUploadImageService uploadImageService,
        IValidator<Place> placeValidator
    )
    {
        _placeRepository = placeRepository;
        _cityRepository = cityRepository;
        _userRepository = userRepository;
        _uploadImageService = uploadImageService;
        _placeValidator = placeValidator;
    }

    public async Task<ICommandResult> Handle(CreatePlaceCommand command)
    {
        try
        {
            var imageContainer = "images/places";
            var place = await this.CreatePlaceEntity(command);
            var valid = await _placeValidator.ValidateAsync(place);

            if (!valid.IsValid)
            {
                return new CommandResult(
                    false,
                    "Verifique se todos os campos estão preenchidos corretamente",
                    (int)HttpStatusCode.BadRequest, errors: valid.ToDictionary());
            }

            imageContainer += $"/{place.Id.ToString().Substring(0,4)}_{place.Name}";
            var presentationImageUrl = await this.UploadPresentationImage(command.PresentationImageBase64, imageContainer);
            place.AddPresentationImageUrl(presentationImageUrl);
            await _placeRepository.CreateAsync(place);

            return new CommandResult(true, "O local foi criado com sucesso", (int)HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            return new CommandResult(
                false,
                "Ocorreu um erro. Por favor novamente tente mais tarde",
                (int)HttpStatusCode.InternalServerError
            );
        }
    }

    private async Task<Place> CreatePlaceEntity(CreatePlaceCommand command)
    {
        var currentUser = await _userRepository.GetByIdAsync(command.OwnerId);
        var address = await this.CreateAddressEntity(command);
        var owner = new User(
            currentUser.Id,
            new Name(currentUser.FirstName, currentUser.LastName, currentUser.NickName),
            new Email(currentUser.Email)
        );

        var place = new Place(command.Name, command.Status);
        place.AddAddress(address);
        place.AddOwner(owner);
        place.AddAutomaticOpen(command.AutomaticOpen);
        place.AddDescription(command.Description);
        place.AddType(command.Type);

        return place;
    }

    private async Task<Address> CreateAddressEntity(CreatePlaceCommand command)
    {
        var city = await _cityRepository.GetByIdAsync(command.CityId);
        var address = new Address(city);
        address.AddLatLong(new LatLong(command.Latitude, command.Longitude));
        address.AddStreet(command.Street);
        address.AddNumber(command.Number);
        address.AddZipCode(command.ZipCode);
        address.AddNeighborhood(command.Neighborhood);

        return address;
    }

    private async Task<string?> UploadPresentationImage(string base64Image, string container)
    {
        if (string.IsNullOrEmpty(base64Image)) return null;

        var imageName = "presentation_" + Guid.NewGuid().ToString();

        return await _uploadImageService.UploadBase64ImageAsync(base64Image, container, imageName);
    }
}
