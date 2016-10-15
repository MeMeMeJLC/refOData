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
    builder.EntitySet<Penalty>("Penalty");
    builder.EntitySet<GamePlayer>("GamePlayers"); 
    builder.EntitySet<PenaltyType>("PenaltyTypes"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PenaltyController : ODataController
    {
        private RefWebApiODataContext db = new RefWebApiODataContext();

        // GET: odata/Penalty
        [EnableQuery]
        public IQueryable<Penalty> GetPenalty()
        {
            return db.Penalties;
        }

        // GET: odata/Penalty(5)
        [EnableQuery]
        public SingleResult<Penalty> GetPenalty([FromODataUri] int key)
        {
            return SingleResult.Create(db.Penalties.Where(penalty => penalty.Id == key));
        }

        // PUT: odata/Penalty(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Penalty> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Penalty penalty = await db.Penalties.FindAsync(key);
            if (penalty == null)
            {
                return NotFound();
            }

            patch.Put(penalty);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenaltyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(penalty);
        }

        // POST: odata/Penalty
        public async Task<IHttpActionResult> Post(Penalty penalty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Penalties.Add(penalty);
            await db.SaveChangesAsync();

            return Created(penalty);
        }

        // PATCH: odata/Penalty(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Penalty> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Penalty penalty = await db.Penalties.FindAsync(key);
            if (penalty == null)
            {
                return NotFound();
            }

            patch.Patch(penalty);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenaltyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(penalty);
        }

        // DELETE: odata/Penalty(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Penalty penalty = await db.Penalties.FindAsync(key);
            if (penalty == null)
            {
                return NotFound();
            }

            db.Penalties.Remove(penalty);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PenaltyExists(int key)
        {
            return db.Penalties.Count(e => e.Id == key) > 0;
        }
    }
}
