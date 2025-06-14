﻿using HotelBookingPlatform.Application.Core.Abstracts.RoomClassManagementService;
namespace HotelBookingPlatform.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoomClassController : ControllerBase
{
    private readonly ILogger<RoomClassController> _logger;
    private readonly IRoomClassService _roomClassService;
    private readonly IRoomManagementService _roomManagementService;
    private readonly IAmenityManagementService _amenityManagementService;
    private readonly IImageService _imageService;
    private readonly IResponseHandler _responseHandler;
    public RoomClassController(
        IRoomClassService roomClassService,
        IRoomManagementService roomManagementService,
        IAmenityManagementService amenityManagementService,
        IImageService imageService,
        IResponseHandler responseHandler,
        ILogger<RoomClassController> logger)
    {
        _roomClassService = roomClassService;
        _roomManagementService = roomManagementService;
        _amenityManagementService = amenityManagementService;
        _imageService = imageService;
        _responseHandler = responseHandler;
        _logger = logger;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateRoomClass([FromBody] RoomClassRequestDto request)
    {
        if (!ModelState.IsValid)
            return _responseHandler.BadRequest("Invalid request data.");

        var createdRoomClass = await _roomClassService.CreateRoomClass(request);
        return _responseHandler.Created(createdRoomClass, "Room class created successfully.");
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all room classes", Description = "Retrieve all room class types")]
    public async Task<IActionResult> GetAllRoomClasses()
    {
        try
        {
            var roomClasses = await _roomClassService.GetAllRoomClassesAsync();
            return _responseHandler.Success(roomClasses);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving room classes: {ex.Message}", "error");
            return _responseHandler.NotFound("An error occured while retrieving room classes.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomClassResponseDto>> GetRoomClass(int id)
    {
        var roomClass = await _roomClassService.GetRoomClassById(id);
        return Ok(roomClass);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateRoomClass(int id, RoomClassRequestDto request)
    {
        var updatedRoomClass = await _roomClassService.UpdateRoomClass(id, request);
        return _responseHandler.Success(updatedRoomClass, "Room class updated successfully.");

    }

    [HttpPost("{roomClassId}/addamenity")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddAmenityToRoomClass(int roomClassId, [FromBody] AmenityCreateDto request)
    {
        var addedAmenity = await _amenityManagementService.AddAmenityToRoomClassAsync(roomClassId, request);
        return _responseHandler.Created(addedAmenity, "Amenity added successfully to the room class.");
    }

    [HttpDelete("{roomClassId}/amenities/{amenityId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAmenityFromRoomClass(int roomClassId, int amenityId)
    {
        await _amenityManagementService.DeleteAmenityFromRoomClassAsync(roomClassId, amenityId);
        return _responseHandler.NoContent("Room class deleted successfully.");
    }

    [HttpGet("{roomClassId}/amenities")]
    public async Task<IActionResult> GetAmenitiesByRoomClassId(int roomClassId)
    {
        var amenities = await _amenityManagementService.GetAmenitiesByRoomClassIdAsync(roomClassId);
        return _responseHandler.Success(amenities, "Amenities retrieved successfully for the room class.");
    }

    [HttpPost("{roomClassId}/rooms")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(Summary = "Add a new room to a specific room class",
                     Description = "Adds a new room to the room class identified by the specified roomClassId. The request body should include the room's number and any other necessary details."
        )]
    public async Task<IActionResult> AddRoomToRoomClass(int roomClassId, [FromBody] RoomCreateRequest request)
    {
        var roomDto = await _roomManagementService.AddRoomToRoomClassAsync(roomClassId, request);
        return _responseHandler.Created(roomDto, "Room added successfully.");
    }

    [HttpGet("{roomClassId}/rooms")]
    [SwaggerOperation(
    Summary = "Get all rooms for a specific room class",
    Description = "Retrieves a list of all rooms associated with the room class identified by the specified roomClassId."
)]
    public async Task<IActionResult> GetRoomsByRoomClassId(int roomClassId)
    {
        var rooms = await _roomManagementService.GetRoomsByRoomClassIdAsync(roomClassId);
        return _responseHandler.Success(rooms, "Rooms retrieved successfully for the room class.");
    }

    [HttpDelete("{roomClassId}/rooms/{roomId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Delete a specific room from a room class",
        Description = "Deletes a specific room from the room class identified by the specified roomClassId. If the room or room class is not found, returns a 404 Not Found response. On successful deletion, returns a 204 No Content response."
    )]
    public async Task<IActionResult> DeleteRoomFromRoomClass(int roomClassId, int roomId)
    {
        await _roomManagementService.DeleteRoomFromRoomClassAsync(roomClassId, roomId);
        return _responseHandler.NoContent("Room deleted successfully.");
    }

    [HttpPost("{roomClassId}/upload-image")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(Summary = "Upload an image for a specific room class.")]
    public async Task<IActionResult> UploadRoomClassImage(int roomClassId, IFormFile file)
    {
        if (file.Length == 0)
            return _responseHandler.BadRequest("No file uploaded.");

        var folderPath = $"roomClasses/{roomClassId}";
        var imageType = "roomClass";
        var uploadResult = await _imageService.UploadImageAsync(file,imageType, roomClassId);

        return _responseHandler.Success(new { Url = uploadResult.SecureUrl.ToString(), PublicId = uploadResult.PublicId }, "Image uploaded successfully for the hotel.");
    }

    [HttpDelete("{roomClassId}/delete-image/{publicId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(Summary = "Delete an image from a specific room class.")]
    public async Task<IActionResult> DeleteRoomClassImage(int roomClassId, string publicId)
    {
        var deletionResult = await _imageService.DeleteImageAsync(publicId);
        return _responseHandler.Success("Image deleted successfully.");
    }

    [HttpGet("{roomClassId}/images")]
    [SwaggerOperation(Summary = "Retrieve all images associated with a specific room class.")]
    public async Task<IActionResult> GetImagesForRoomClass(int roomClassId)
    {
        var allImages = await _imageService.GetImagesByTypeAsync("roomClass");

        var roomClassImages = allImages.Where(img => img.EntityId == roomClassId);

        if (!roomClassImages.Any())
            return _responseHandler.NotFound("No images found for the specified room class.");

        return _responseHandler.Success(roomClassImages, "Images retrieved successfully for the room class.");
    }
}


