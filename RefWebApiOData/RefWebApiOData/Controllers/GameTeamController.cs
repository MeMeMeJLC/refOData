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
    builder.EntitySet<GameTeam>("GameTeam");
    builder.EntitySet<Game>("Games"); 
    builder.EntitySet<Team>("Teams"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class GameTeamController : ODataController
    {
        private RefWebApiODataContext db = new RefWebApiODataContext();

        // GET: odata/GameTeam
        [EnableQuery]
        public IQueryable<GameTeam> GetGameTeam()
        {
            return db.GameTeams;
        }

        // GET: odata/GameTeam(5)
        [EnableQuery]
        public SingleResult<GameTeam> GetGameTeam([FromODataUri] int key)
        {
            return SingleResult.Create(db.GameTeams.Where(gameTeam => gameTeam.Id == key));
        }

        // PUT: odata/GameTeam(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<GameTeam> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GameTeam gameTeam = await db.GameTeams.FindAsync(key);
            if (gameTeam == null)
            {
                return NotFound();
            }

            patch.Put(gameTeam);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameTeamExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(gameTeam);
        }

        // POST: odata/GameTeam
        public async Task<IHttpActionResult> Post(GameTeam gameTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GameTeams.Add(gameTeam);
            await db.SaveChangesAsync();

            return Created(gameTeam);
        }

        // PATCH: odata/GameTeam(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<GameTeam> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            GameTeam gameTeam = await db.GameTeams.FindAsync(key);
            if (gameTeam == null)
            {
                return NotFound();
            }

            patch.Patch(gameTeam);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameTeamExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(gameTeam);
        }

        // DELETE: odata/GameTeam(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            GameTeam gameTeam = await db.GameTeams.FindAsync(key);
            if (gameTeam == null)
            {
                return NotFound();
            }

            db.GameTeams.Remove(gameTeam);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/GameTeam(5)/Game
        [EnableQuery]
        public SingleResult<Game> GetGame([FromODataUri] int key)
        {
            return SingleResult.Create(db.GameTeams.Where(m => m.Id == key).Select(m => m.Game));
        }

        // GET: odata/GameTeam(5)/Team
        [EnableQuery]
        public SingleResult<Team> GetTeam([FromODataUri] int key)
        {
            return SingleResult.Create(db.GameTeams.Where(m => m.Id == key).Select(m => m.Team));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameTeamExists(int key)
        {
            return db.GameTeams.Count(e => e.Id == key) > 0;
        }
    }
}
