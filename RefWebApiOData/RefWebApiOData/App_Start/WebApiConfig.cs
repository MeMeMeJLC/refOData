using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using RefWebApiOData.Models;

namespace RefWebApiOData
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Game>("Game");
            builder.EntitySet<GameTeam>("GameTeam");
            builder.EntitySet<Team>("Team");
            builder.EntitySet<Player>("Player");
            builder.EntitySet<GamePlayer>("GamePlayer");
            builder.EntitySet<Goal>("Goal");
            builder.EntitySet<PenaltyType>("PenaltyType");
            builder.EntitySet<Penalty>("Penalty");
            builder.EntitySet<Substitution>("Substitution");

            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
