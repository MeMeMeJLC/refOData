using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using RefWebApiOData.Models;

namespace RefWebApiOData.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using RefWebApiOData.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<GamePlayer>("GamePlayer");
    builder.EntitySet<Game>("Games"); 
    builder.EntitySet<Player>("Players"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class GamePlayerController : ODataController
    {
        private RefWebApiODataContext db = new RefWebApiODataContext();

        // GET: odata/GamePlayer
        [EnableQuery]
        public IQueryable<GamePlayer> GetGamePlayer()
        {
            return db.GamePlayers;
        }

        // GET: odata/GamePlayer(5)
        [EnableQuery]
        public SingleResult<GamePlayer> GetGamePlayer([FromODataUri] int key)
        {
            return SingleResult.Create(db.GamePlayers.Where(gamePlayer => gamePlayer.Id == key));
        }

        // PUT: odata/GamePlayer(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<GamePlayer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GamePlayer gamePlayer = await db.GamePlayers.FindAsync(key);
            if (gamePlayer == null)
            {
                return NotFound();
            }

            patch.Put(gamePlayer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamePlayerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(gamePlayer);
        }

        // POST: odata/GamePlayer
        public async Task<IHttpActionResult> Post(GamePlayer gamePlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GamePlayers.Add(gamePlayer);
            await db.SaveChangesAsync();

            return Created(gamePlayer);
        }

        // PATCH: odata/GamePlayer(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<GamePlayer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GamePlayer gamePlayer = await db.GamePlayers.FindAsync(key);
            if (gamePlayer == null)
            {
                return NotFound();
            }

            patch.Patch(gamePlayer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamePlayerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(gamePlayer);
        }

        // DELETE: odata/GamePlayer(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            GamePlayer gamePlayer = await db.GamePlayers.FindAsync(key);
            if (gamePlayer == null)
            {
                return NotFound();
            }

            db.GamePlayers.Remove(gamePlayer);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/GamePlayer(5)/Game
        [EnableQuery]
        public SingleResult<Game> GetGame([FromODataUri] int key)
        {
            return SingleResult.Create(db.GamePlayers.Where(m => m.Id == key).Select(m => m.Game));
        }

        // GET: odata/GamePlayer(5)/Player
        [EnableQuery]
        public SingleResult<Player> GetPlayer([FromODataUri] int key)
        {
            return SingleResult.Create(db.GamePlayers.Where(m => m.Id == key).Select(m => m.Player));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GamePlayerExists(int key)
        {
            return db.GamePlayers.Count(e => e.Id == key) > 0;
        }
    }
}
