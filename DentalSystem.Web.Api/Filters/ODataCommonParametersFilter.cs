using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.OData.Query;

namespace DentalSystem.Web.Api.Filters
{
    /// <summary>
    /// OData Common Parameters Filter.
    /// </summary>
    public class ODataCommonParametersFilter : IOperationFilter
    {
        /// <summary>
        /// Applies OData Common Parameters to OData end points.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null && descriptor.MethodInfo.GetCustomAttributes(typeof(EnableQueryAttribute), true).Any())
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "$filter",
                    In = ParameterLocation.Query,
                    Description = "The $filter system query option allows to filter the collection of resources that are addressed by the request URL.",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "$expand",
                    In = ParameterLocation.Query,
                    Description = "The $expand system query option specifies the related resources or media streams to be included in line with retrieved resources.",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "$select",
                    In = ParameterLocation.Query,
                    Description = "The $select system query option allows to request a specific set of properties for each entity or complex type.",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "$orderby",
                    In = ParameterLocation.Query,
                    Description = "The $orderby system query option allows to request resources in a particular order.",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "$top",
                    In = ParameterLocation.Query,
                    Description = "The $top system query option requests the number of items in the queried collection to be included in the result. ",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "$skip",
                    In = ParameterLocation.Query,
                    Description = "The $skip query option requests the number of items in the queried collection that are to be skipped and not included in the result.",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "$count",
                    In = ParameterLocation.Query,
                    Description = "The $count system query option allows to request a count of the matching resources included with the resources in the response.",
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "boolean"
                    }
                });
            }
        }
    }
}