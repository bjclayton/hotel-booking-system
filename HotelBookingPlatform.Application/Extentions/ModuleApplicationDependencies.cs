﻿using HotelBookingPlatform.Application.Core.Abstracts.HotelManagementService;
using HotelBookingPlatform.Application.Core.Abstracts.IBookingManagementService;
using HotelBookingPlatform.Application.Core.Abstracts.IHotelManagementService;
using HotelBookingPlatform.Application.Core.Abstracts.RoomClassManagementService;
using HotelBookingPlatform.Application.Core.Implementations.BookingManagementService;
using HotelBookingPlatform.Application.Core.Implementations.HotelManagementService;
using HotelBookingPlatform.Application.Core.Implementations.RoomClassManagementService;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace HotelBookingPlatform.Application.Extentions;

public static class ModuleApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddValidatorsFromAssemblyContaining<OwnerValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();

        services.AddScoped<IPriceCalculationService, PriceCalculationService>();
        services.AddScoped<IConfirmationNumberGeneratorService, ConfirmationNumberGeneratorService>();
        services.AddScoped<ICityHotelService, CityHotelService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IAmenityService, AmenityService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IHotelAmenitiesService, HotelAmenitiesService>();
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<IInvoiceRecordService, InvoiceRecordService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IRoomClassService, RoomClassService>();
        services.AddScoped<IRoomManagementService, RoomManagementService>();
        services.AddScoped<IAmenityManagementService, AmenityManagementService>();
        services.AddScoped<IHotelManagementService, HotelManagementService>();
        services.AddScoped<IHotelSearchService, HotelSearchService>();
        services.AddScoped<IHotelAmenityService, HotelAmenityService>();
        services.AddScoped<IHotelReviewService, HotelReviewService>();
        services.AddScoped<IHotelRoomService, HotelRoomService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IValidator<HotelCreateRequest>, HotelCreateRequestValidator>();

        return services;
    }
}

