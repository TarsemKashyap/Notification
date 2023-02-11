using Example.Notific.Context.Contract.CQRS.Dtos;
using Example.Notific.WebApi.Contract.Resources;
using Example.Notific.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using Example.Common.WebAPI.Contract;

namespace Example.Notific.WebApi.Mappers
{
    public class EventDtoMapper
    {
        public static Event ToResource(EventDto dto, UrlHelper urlHelper = null)
        {
            Event result = new Event()
            {
                MerchantId = dto.MerchantId,
                Content = dto.EventContent,
                Received = dto.DateReceived.ToString("s") + "Z",
                Type = dto.EventType,
                Id = dto.EventId.ToString()
            };

            if (urlHelper != null)
            {
                var link = new Link()
                {
                    Href = urlHelper.Link(EventsController.RouteNameGetEventDetails, new { id = dto.EventId }),
                    Relation = "self"
                };

                result.Links.Add(link);
            }

            return result;
        }
    }
}