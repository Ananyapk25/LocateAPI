﻿using AutoMapper;
using LocationNinja.Common.Persistence;
using LocationNinja.Features.IpLocation.Domain;
using LocationNinja.Features.IpLocation.Models;
using Microsoft.EntityFrameworkCore;

namespace LocationNinja.Features.IpLocation;

public class IpLocationService(ILocationAPI locationAPI,
                               IMapper mapper,
                               LocationNinjaDbContext locationNinjaDbContext) : IIpLocationService
{
    public async Task<IpLocationResponse> GetLocation(string ip, CancellationToken cancellationToken = default)
    {
        Location? locationFromDB = await FetchLocationFromDB(ip, cancellationToken);

        if (locationFromDB is not null)
        {
            return mapper.Map<IpLocationResponse>(locationFromDB);
        }

        IpLocationApiResponse locationFromAPI = await FetchLocationFromApi(ip, cancellationToken);

        return mapper.Map<IpLocationResponse>(locationFromAPI);
    }

    public async Task<IpLocationDetailedResponse> GetDetailedLocation(string ip, CancellationToken cancellationToken = default)
    {
        Location? locationFromDB = await FetchLocationFromDB(ip, cancellationToken);

        if (locationFromDB is not null)
        {
            return mapper.Map<IpLocationDetailedResponse>(locationFromDB);
        }

        IpLocationApiResponse locationFromAPI = await FetchLocationFromApi(ip, cancellationToken);

        return mapper.Map<IpLocationDetailedResponse>(locationFromAPI);
    }

    private async Task<Location?> FetchLocationFromDB(string ip, CancellationToken cancellationToken)
    {
        return await locationNinjaDbContext.Locations.FirstOrDefaultAsync(l => l.Ip == ip, cancellationToken);
    }

    private async Task<IpLocationApiResponse> FetchLocationFromApi(string ip, CancellationToken cancellationToken)
    {
        var locationFromAPI = await locationAPI.GetAsync(ip, cancellationToken);

        var locationToPersist = mapper.Map<Location>(locationFromAPI);
        locationToPersist.Ip = ip;

        await locationNinjaDbContext.Locations.AddAsync(locationToPersist, cancellationToken);
        await locationNinjaDbContext.SaveChangesAsync(cancellationToken);

        return locationFromAPI;
    }
}