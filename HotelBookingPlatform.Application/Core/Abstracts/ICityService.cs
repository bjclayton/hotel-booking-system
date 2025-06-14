﻿namespace HotelBookingPlatform.Application.Core.Abstracts;
public interface ICityService
{
    Task<IEnumerable<CityResponseDto>> GetCities(string cityName, string description, int pageSize, int pageNumber);
    Task<CityWithHotelsResponseDto> GetCity(int id, bool includeHotels);
    Task<CityResponseDto> UpdateCity(int id, CityCreateRequest request);
    Task DeleteAsync(int id);
    Task<CityResponseDto> AddCityAsync(CityCreateRequest request);
    Task<IEnumerable<CityResponseDto>> GetTopVisitedCitiesAsync(int topCount);
    Task<IEnumerable<HotelResponseDto>> GetHotelsForCityAsync(int cityId);


}
