using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class FavoriteManager(CoachAssistentDbContext context, IMapper _mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper) : BaseAuthenticatedManager(context, _mapper, configuration, authenticationWrapper)
    {
        public async Task SetFavorite(Guid shareableId)
        {
            Favorite? favorite = await dbContext.Favorites
                .FirstOrDefaultAsync(f => f.UserId.Equals(authenticationWrapper.UserId) && f.ShareableId.Equals(shareableId));

            if (favorite is null)
            {
                favorite = new Favorite
                {
                    UserId = authenticationWrapper.UserId,
                    ShareableId = shareableId
                };
                await dbContext.Favorites.AddAsync(favorite);
            }
            else
            {
                dbContext.Favorites.Remove(favorite);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
