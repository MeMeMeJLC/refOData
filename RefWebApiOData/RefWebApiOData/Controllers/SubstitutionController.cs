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
    builder.EntitySet<Substitution>("Substitution");
    builder.EntitySet<GamePlayer>("GamePlayers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SubstitutionController : ODataController
    {
        private RefWebApiODataContext db = new RefWebApiODataContext();

        // GET: odata/Substitution
        [EnableQuery]
        public IQueryable<Substitution> GetSubstitution()
        {
            return db.Substitutions;
        }

        // GET: odata/Substitution(5)
        [EnableQuery]
        public SingleResult<Substitution> GetSubstitution([FromODataUri] int key)
        {
            return SingleResult.Create(db.Substitutions.Where(substitution => substitution.Id == key));
        }

        // PUT: odata/Substitution(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Substitution> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Substitution substitution = await db.Substitutions.FindAsync(key);
            if (substitution == null)
            {
                return NotFound();
            }

            patch.Put(substitution);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubstitutionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(substitution);
        }

        // POST: odata/Substitution
        public async Task<IHttpActionResult> Post(Substitution substitution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Substitutions.Add(substitution);
            await db.SaveChangesAsync();

            return Created(substitution);
        }

        // PATCH: odata/Substitution(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Substitution> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Substitution substitution = await db.Substitutions.FindAsync(key);
            if (substitution == null)
            {
                return NotFound();
            }

            patch.Patch(substitution);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubstitutionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(substitution);
        }

        // DELETE: odata/Substitution(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Substitution substitution = await db.Substitutions.FindAsync(key);
            if (substitution == null)
            {
                return NotFound();
            }

            db.Substitutions.Remove(substitution);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Substitution(5)/GamePlayerGoingOffTheField
        [EnableQuery]
        public SingleResult<GamePlayer> GetGamePlayerGoingOffTheField([FromODataUri] int key)
        {
            return SingleResult.Create(db.Substitutions.Where(m => m.Id == key).Select(m => m.GamePlayerGoingOffTheField));
        }

        // GET: odata/Substitution(5)/GamePlayerGoingOnTheField
        [EnableQuery]
        public SingleResult<GamePlayer> GetGamePlayerGoingOnTheField([FromODataUri] int key)
        {
            return SingleResult.Create(db.Substitutions.Where(m => m.Id == key).Select(m => m.GamePlayerGoingOnTheField));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubstitutionExists(int key)
        {
            return db.Substitutions.Count(e => e.Id == key) > 0;
        }
    }
}
