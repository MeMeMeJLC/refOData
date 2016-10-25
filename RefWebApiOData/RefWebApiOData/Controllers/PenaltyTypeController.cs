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
    builder.EntitySet<PenaltyType>("PenaltyType");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PenaltyTypeController : ODataController
    {
        private RefWebApiODataContext db = new RefWebApiODataContext();

        [Authorize]
        // GET: odata/PenaltyType
        [EnableQuery]
        public IQueryable<PenaltyType> GetPenaltyType()
        {
            return db.PenaltyTypes;
        }

        [Authorize]
        // GET: odata/PenaltyType(5)
        [EnableQuery]
        public SingleResult<PenaltyType> GetPenaltyType([FromODataUri] int key)
        {
            return SingleResult.Create(db.PenaltyTypes.Where(penaltyType => penaltyType.Id == key));
        }

        [Authorize]
        // PUT: odata/PenaltyType(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<PenaltyType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PenaltyType penaltyType = await db.PenaltyTypes.FindAsync(key);
            if (penaltyType == null)
            {
                return NotFound();
            }

            patch.Put(penaltyType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenaltyTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(penaltyType);
        }

        [Authorize]
        // POST: odata/PenaltyType
        public async Task<IHttpActionResult> Post(PenaltyType penaltyType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PenaltyTypes.Add(penaltyType);
            await db.SaveChangesAsync();

            return Created(penaltyType);
        }

        [Authorize]
        // PATCH: odata/PenaltyType(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<PenaltyType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PenaltyType penaltyType = await db.PenaltyTypes.FindAsync(key);
            if (penaltyType == null)
            {
                return NotFound();
            }

            patch.Patch(penaltyType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenaltyTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(penaltyType);
        }

        [Authorize]
        // DELETE: odata/PenaltyType(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            PenaltyType penaltyType = await db.PenaltyTypes.FindAsync(key);
            if (penaltyType == null)
            {
                return NotFound();
            }

            db.PenaltyTypes.Remove(penaltyType);
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

        private bool PenaltyTypeExists(int key)
        {
            return db.PenaltyTypes.Count(e => e.Id == key) > 0;
        }
    }
}
