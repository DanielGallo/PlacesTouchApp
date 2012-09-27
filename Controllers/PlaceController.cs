using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Places.Controllers
{
    public class PlaceController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET api/Place
        public IEnumerable<Place> GetPlaces()
        {
            return db.Places.AsEnumerable();
        }

        // GET api/Place/5
        public Place GetPlace(int id)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return place;
        }

        // PUT api/Place/5
        public HttpResponseMessage PutPlace(int id, Place place)
        {
            if (ModelState.IsValid && id == place.id)
            {
                db.Entry(place).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Place
        public HttpResponseMessage PostPlace(Place place)
        {
            if (ModelState.IsValid)
            {
                db.Places.Add(place);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, place);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = place.id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Place/5
        public HttpResponseMessage DeletePlace(int id)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Places.Remove(place);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, place);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}