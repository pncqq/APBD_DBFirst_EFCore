using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DAL;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Repositories.Implementations;

public class TripRepository : ITripRepository
{
    private readonly MyDbContext _context;

    public TripRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<TripModel>> GetTripsSortedAsync()
    {
        return await _context.Trips
            .Select(trip => new TripModel()
            {
                IdTrip = trip.IdTrip,
                Name = trip.Name,
                Description = trip.Description,
                DateFrom = trip.DateFrom,
                DateTo = trip.DateTo,
                MaxPeople = trip.MaxPeople,
                Clients = trip.ClientTrips,
                Countries = trip.Countries
            })
            .OrderByDescending(model => model.DateFrom)
            .ToListAsync();
    }

    public async Task<DatabaseErrors> DeleteClientAsync(int idClient)
    {
        //Sprawdzenie czy klient nie ma wycieczek
        try
        {
            await _context.ClientTrips
                .Where(ct => ct.IdClient == idClient)
                .FirstAsync();

            return DatabaseErrors.ClientHasTrips;
        }
        catch (InvalidOperationException e)
        {
        }


        //Jesli nie ma - usuwamy
        try
        {
            var clientToDelete = new Client()
            {
                IdClient = idClient
            };

            _context.Clients.Attach(clientToDelete);
            _context.Clients.Remove(clientToDelete);

            await _context.SaveChangesAsync();

            return DatabaseErrors.ClientDeleted;
        }
        catch (Exception e) //nie ma klienta o podanym id
        {
            return DatabaseErrors.ClientNotPresent;
        }
    }

    public async Task<bool> AddClientToTripAsync(AddClientToTripDTO data)
    {
        var pesel = "";
        Client tempClient;
        
        //Sprawdzamy czy klient o danym PESEL istnieje
        try
        {
            tempClient = await _context.Clients
                .Where(client => client.Pesel == data.Pesel)
                .FirstAsync();

            pesel = tempClient.Pesel;
        }
        catch (Exception e)
        {
            //Jesli nie - dodaj do bazy
            await _context.Clients
                .AddAsync(new Client()
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Telephone = data.Telephone,
                    Email = data.Email,
                    Pesel = data.Pesel,
                });

            await _context.SaveChangesAsync();
        }
        
        //Sprawdzamy czy wycieczka istnieje
        try
        {
            await _context.ClientTrips
                .Where(trip => trip.IdTrip == data.IdTrip)
                .FirstAsync();
            
            //Jesli tak - dobrze
        }
        catch (Exception e) //Jesli nie - blad
        {
            return false;
        }
        
        //Sprawdzamy czy jest zapisany na dana wycieczke
        try
        {
            await _context.ClientTrips
                .Where(trip => trip.IdTrip == data.IdTrip)
                .Where(trip => pesel == data.Pesel)
                .FirstAsync();
            
            //Jesli tak - blad
            return false;
        
        }
        catch (Exception e) //Jesli nie - dodajemy
        {
            // ignored
        }

        await _context.ClientTrips
            .AddAsync(new ClientTrip()
            {
                IdTrip = data.IdTrip,
                PaymentDate = data.PaymentDate,
                RegisteredAt = DateTime.Now,
            });

        await _context.SaveChangesAsync();

        return true;
    }
}