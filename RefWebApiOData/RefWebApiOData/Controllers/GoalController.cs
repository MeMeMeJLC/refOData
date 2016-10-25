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
    builder.EntitySet<Goal>("Goal");
    builder.EntitySet<GamePlayer>("GamePlayers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class GoalController : ODataController
    {
        private RefWebApiODataContext db = new RefWebApiODataContext();

        [Authorize]
        // GET: odata/Goal
        [EnableQuery]
        public IQueryable<Goal> GetGoal()
        {
            return db.Goals;
        }

        [Authorize]
        // GET: odata/Goal(5)
        [EnableQuery]
        public SingleResult<Goal> GetGoal([FromODataUri] int key)
        {
            return SingleResult.Create(db.Goals.Where(goal => goal.Id == key));
        }

        [Authorize]
        // PUT: odata/Goal(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Goal> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Goal goal = await db.Goals.FindAsync(key);
            if (goal == null)
            {
                return NotFound();
            }

            patch.Put(goal);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(goal);
        }

        [Authorize]
        // POST: odata/Goal
        public async Task<IHttpActionResult> Post(Goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Goals.Add(goal);
            await db.SaveChangesAsync();

            return Created(goal);
        }

        [Authorize]
        // PATCH: odata/Goal(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Goal> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Goal goal = await db.Goals.FindAsync(key);
            if (goal == null)
            {
                return NotFound();
            }

            patch.Patch(goal);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(goal);
        }

        [Authorize]
        // DELETE: odata/Goal(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Goal goal = await db.Goals.FindAsync(key);
            if (goal == null)
            {
                return NotFound();
            }

            db.Goals.Remove(goal);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize]
        // GET: odata/Goal(5)/GamePlayer
        [EnableQuery]
        public SingleResult<GamePlayer> GetGamePlayer([FromODataUri] int key)
        {
            return SingleResult.Create(db.Goals.Where(m => m.Id == key).Select(m => m.GamePlayer));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoalExists(int key)
        {
            return db.Goals.Count(e => e.Id == key) > 0;
        }
    }
}
