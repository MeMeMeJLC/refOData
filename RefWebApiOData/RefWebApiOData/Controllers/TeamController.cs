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
    builder.EntitySet<Team>("Team");
    builder.EntitySet<Player>("Players"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TeamController : ODataController
    {
        private RefWebApiODataContext db = new RefWebApiODataContext();

        // GET: odata/Team
        [EnableQuery]
        public IQueryable<Team> GetTeam()
        {
            return db.Teams;
        }

        // GET: odata/Team(5)
        [EnableQuery]
        public SingleResult<Team> GetTeam([FromODataUri] int key)
        {
            return SingleResult.Create(db.Teams.Where(team => team.Id == key));
        }

        // PUT: odata/Team(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Team> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Team team = await db.Teams.FindAsync(key);
            if (team == null)
            {
                return NotFound();
            }

            patch.Put(team);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(team);
        }

        // POST: odata/Team
        public async Task<IHttpActionResult> Post(Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(team);
            await db.SaveChangesAsync();

            return Created(team);
        }

        // PATCH: odata/Team(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Team> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Team team = await db.Teams.FindAsync(key);
            if (team == null)
            {
                return NotFound();
            }

            patch.Patch(team);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(team);
        }

        // DELETE: odata/Team(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Team team = await db.Teams.FindAsync(key);
            if (team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(team);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Team(5)/Players
        [EnableQuery]
        public IQueryable<Player> GetPlayers([FromODataUri] int key)
        {
            return db.Teams.Where(m => m.Id == key).SelectMany(m => m.Players);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int key)
        {
            return db.Teams.Count(e => e.Id == key) > 0;
        }
    }
}
